using UnityEngine;

namespace JellyFramework
{
    [System.Serializable]
    public class ElementWeight<T>
    {
        public T element;
        public float weight;
        public ElementWeight(T element, float weight)
        {
            this.element = element;
            this.weight = weight;
        }
    }
}

