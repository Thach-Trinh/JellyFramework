using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.PoolingFactorySystem
{
    public interface ISpawnable<T>
    {
        Action release { get; set; }
        int PoolId { get; set; }
        void OnSpawned();
        void OnReleased();
    }

    public interface ISpawnableWithId<T1, T2> : ISpawnable<T1> where T2 : Enum
    {
        T2 Type { get; }
    }
}

