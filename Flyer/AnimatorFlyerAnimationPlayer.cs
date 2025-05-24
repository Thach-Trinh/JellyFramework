using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyFramework.FlyerSystem
{
    [CreateAssetMenu(menuName = "FlyerAnimationPlayer/Animator")]
    public class AnimatorFlyerAnimationPlayer : FlyerAnimationPlayer
    {
        [SerializeField] private float duration;
        public override IEnumerator PlayAnim(FlyerObject flyerObject)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}


