using System;
using System.Collections.Generic;


namespace JellyFramework.PoolingFactorySystem
{
    public class PoolingFactory<T>
    {
        private Queue<T> pool = new Queue<T>();
        private Func<T> createInstance;

        public PoolingFactory(Func<T> createInstance)
        {
            pool = new Queue<T>();
            this.createInstance = createInstance;
        }


        public T GetInstance()
        {
            if (pool.Count > 0)
                return pool.Dequeue();
            return createInstance();
        }

        public void Return(T obj) => pool.Enqueue(obj);
    }

    public class SpawneblePoolingFactory<T> where T : ISpawnable
    {
        private Queue<T> pool = new Queue<T>();
        private Func<T> createInstance;

        public SpawneblePoolingFactory(Func<T> createInstance)
        {
            pool = new Queue<T>();
            this.createInstance = createInstance;
        }

        public bool TryGetPooledInstance(out T instance)
        {
            if (pool.Count > 0)
            {
                instance = pool.Dequeue();
                instance.OnSpawned();
                return true;
            }
            instance = default;
            return false;
        }

        public T CreateNewInstance()
        {
            T newObj = createInstance();
            newObj.release = () => Return(newObj);
            newObj.OnSpawned();
            return newObj;
        }


        public T GetInstance()
        {
            if (TryGetPooledInstance(out T instance))
                return instance;
            return CreateNewInstance();
        }

        public void Return(T obj)
        {
            obj.OnReleased();
            pool.Enqueue(obj);
        }
    }
}





