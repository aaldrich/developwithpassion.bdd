using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.containers;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using Rhino.Mocks;

namespace developwithpassion.bdd.core
{
    public class ObservationContext<SUT> : Observations<SUT>
    {
        public IDictionary<Type, object> dependencies { get; set; }
        public Exception exception_thrown_while_the_sut_performed_its_work { get; set; }
        public Action behaviour_performed_in_because { get; set;}
        public SUT sut { get; set; }
        public Func<SUT> factory { get; set; }
        object raw_test;
        readonly IList<PipelineBehaviour> context_pipeline;

        public ObservationContext(object raw_test, IList<PipelineBehaviour> behaviours)
        {
            this.raw_test = raw_test;
            context_pipeline = behaviours;
        }

        Command build_command_chain<DelegateType>()
        {
            var actions = new Stack<Command>();
            var current_class = raw_test.GetType();

            while (current_class.is_a_concern_type())
            {
                actions.Push(new DelegateFieldInvocation(typeof (DelegateType), this, current_class));
                current_class = current_class.BaseType;
            }

            return actions.as_command_chain();
        }

        public void run_action<DelegateType>()
        {
            build_command_chain<DelegateType>().run();
        }

        public void tear_down() {
            run_action<after_each_observation>();
            context_pipeline.each(item => item.finish());
            dependencies.Clear();
        }

        public void reset()
        {
            context_pipeline.Clear();

            add_pipeline_behaviour(() => {}, UnitTestContainer.tear_down);
            behaviour_performed_in_because = null;
            exception_thrown_while_the_sut_performed_its_work = null;
            dependencies = new Dictionary<Type, object>();
            prepare_to_make_an_observation();
        }

        public void doing(Action because_behaviour)
        {
            behaviour_performed_in_because = because_behaviour;
        }

        public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            doing(() => behaviour().force_traversal());
        }

        public Exception exception_thrown_by_the_sut
        {
            get { return exception_thrown_while_the_sut_performed_its_work ?? (exception_thrown_while_the_sut_performed_its_work = get_exception_throw_by(behaviour_performed_in_because)); }
            set { exception_thrown_while_the_sut_performed_its_work = value; }
        }

        Exception get_exception_throw_by(Action because_behaviour)
        {
            return because_behaviour.get_exception();
        }

        public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return container_dependency(an<InterfaceType>());
        }

        public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            UnitTestContainer.add_implementation_of(instance);
            return instance;
        }

        public object an_item_of(Type type)
        {
            return MockRepository.GenerateStub(type, new object[0]);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return MockRepository.GenerateStub<InterfaceType>();
        }

        public void prepare_to_make_an_observation()
        {
            run_action<context>();
            context_pipeline.each(item => item.start());
            sut = factory();
            run_action<after_the_sut_has_been_created>();
            run_action<because>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            context_pipeline.Add(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            add_pipeline_behaviour(new PipelineBehaviour(context, teardown));
        }

        public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return new ChangeValueInPipeline(add_pipeline_behaviour, static_expression);
        }
    }
}