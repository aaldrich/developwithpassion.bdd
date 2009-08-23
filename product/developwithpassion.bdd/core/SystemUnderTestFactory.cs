namespace developwithpassion.bdd.core
{
    public interface SystemUnderTestFactory
    {
        Contract create<Contract,Class>(); 
    }
}