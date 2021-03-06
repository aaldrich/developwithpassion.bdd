using System;
using Rhino.Mocks;

namespace developwithpassion.bdd.mocking.rhino
{
    public class VoidMethodCallOccurance<T>
    {
        Action<T> action;
        private T mock;

        public VoidMethodCallOccurance(T mock, Action<T> action)
        {
            this.mock = mock;
            this.action = action;
            mock.AssertWasCalled(action, y => y.Repeat.AtLeastOnce());
        }

        public void times(int number_of_times_the_method_should_have_been_called)
        {
            mock.AssertWasCalled(action, y => y.Repeat.Times(number_of_times_the_method_should_have_been_called));
        }

        public void only_once()
        {
            times(1);
        }
    }
}