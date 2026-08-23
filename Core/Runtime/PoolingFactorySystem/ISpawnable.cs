using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.PoolingFactorySystem
{
    public interface ISpawnable
    {
        Action release { get; set; }
        void OnSpawned();
        void OnReleased();
    }
}

