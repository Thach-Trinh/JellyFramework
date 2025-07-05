using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace JellyFramework.PoolingFactorySystem
{
    [Serializable]
    public class PoolingFactoryWithId<T1, T2> : PoolingFactory<T1>
    where T1 : MonoBehaviour, ISpawnable<T1>
    where T2 : Enum
    {
        [SerializeField] private T2 type;
        public PoolingFactoryWithId(T1 prefab) : base(prefab) { }
        public T2 Type => type;
    }

    public class PoolingFactory<T> where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] protected T prefab;
        protected Queue<T> pool = new Queue<T>();
        protected string name;
        protected int instanceCount;
        public int InstanceCount => instanceCount;
        public int PoolCount => pool != null ? pool.Count : 0;

        public PoolingFactory(T prefab)
        {
            this.prefab = prefab;
            Init();
        }

        public void Init()
        {
            pool = new Queue<T>();
            name = prefab.name;
            instanceCount = 0;
        }

        public T Spawn(Transform parent)
        {
            if (pool.Count > 0)
            {
                T availableObj = pool.Dequeue();
                availableObj.gameObject.SetActive(true);
                availableObj.transform.SetParent(parent);
                availableObj.OnSpawned();
                return availableObj;
            }
            T newObj = Object.Instantiate(prefab, parent);
            newObj.PoolId = instanceCount;
            newObj.release = () => Return(newObj);
            newObj.OnSpawned();
            instanceCount++;
            return newObj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.OnReleased();
            pool.Enqueue(obj);
        }
    }

}





