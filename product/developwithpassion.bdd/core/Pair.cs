namespace developwithpassion.bdd.core
{
    public class Pair<T,U>
    {
        public T first { get; private set; }
        public U second { get; private set; }

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    }
}