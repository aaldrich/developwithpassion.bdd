namespace developwithpassion.bdd.core.commands
{
    public interface ParameterizedCommand<T>
    {
        void run_against(T item);
    }
}