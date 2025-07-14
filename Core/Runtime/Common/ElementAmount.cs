namespace JellyFramework
{
    [System.Serializable]
    public class ElementAmount<T>
    {
        public T element;
        public int amount;

        public ElementAmount(T element, int amount)
        {
            this.element = element;
            this.amount = amount;
        }
    }
}
