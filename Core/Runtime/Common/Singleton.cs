using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyFramework
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;
        private void Awake() => CustomAwake();

        protected virtual void CustomAwake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }


    }
}

