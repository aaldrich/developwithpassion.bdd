using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public interface Observations<SUT> {
        IDictionary<Type, object> dependencies { get; set; }
        Exception exception_thrown_while_the_sut_performed_its_work { get; set; }
        Action behaviour_performed_in_because { get; set; }
        SUT sut { get; set; }
        Func<SUT> factory { get; set; }
        Exception exception_thrown_by_the_sut { get; }
        void run_action<DelegateType>();
        void tear_down();
        void reset();
        void doing(Action because_behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
        InterfaceType container_dependency<InterfaceType>() where InterfaceType : class;
        InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class;
        object an_item_of(Type type);
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        void prepare_to_make_an_observation();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
        ChangeValueInPipeline change(Expression<Func<object>> static_expression);
    }
}