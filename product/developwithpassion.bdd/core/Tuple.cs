namespace developwithpassion.bdd.core
{
    public class Tuple<T,U>
    {
        public T first { get; private set; }
        public U second { get; private set; }

        public Tuple(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    }
}