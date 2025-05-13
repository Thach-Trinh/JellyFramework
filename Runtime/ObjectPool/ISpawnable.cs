using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.ObjectPool
{
    public interface ISpawnable<T>
    {
        Action returnCallback1 { get; set; }
        Action<T> returnCallback2 { get; set; }
        int PoolId { get; set; }

        void OnSpawned();
        void OnDespawned();
    }

    public interface ISpawnableWithId<T1, T2> : ISpawnable<T1> where T2 : Enum
    {
        T2 Type { get; }
    }
}

