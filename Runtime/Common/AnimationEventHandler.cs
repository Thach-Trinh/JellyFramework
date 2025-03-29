using System;
using UnityEngine;

namespace JellyFramework
{
    public class AnimationEventHandler<T> : MonoBehaviour where T : Enum
    {
        public Action<T> onEventTrigger;
        public void OnEventTrigger(T value) => onEventTrigger?.Invoke(value);
    }
}