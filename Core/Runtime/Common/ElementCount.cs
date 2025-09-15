namespace JellyFramework
{
    [System.Serializable]
    public class ElementCount<T>
    {
        public T element;
        public int amount;

        public ElementCount(T element, int amount)
        {
            this.element = element;
            this.amount = amount;
        }
    }
}
