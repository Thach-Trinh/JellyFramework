using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyFramework.FlyerSystem
{
    public abstract class FlyerAnimationPlayer : ScriptableObject
    {
        [SerializeField] private FlyerAnimationStyle style;
        public FlyerAnimationStyle Style => style;
        public abstract IEnumerator PlayAnim(FlyerObject flyerObject);
    }
}
