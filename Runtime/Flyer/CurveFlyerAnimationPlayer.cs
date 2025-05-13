using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.FlyerSystem
{
    [CreateAssetMenu(menuName = "FlyerAnimationPlayer/Curve")]
    public class CurveFlyerAnimationPlayer : FlyerAnimationPlayer
    {
        [SerializeField] private float offset;
        [SerializeField] private float duration = 1f;
        [SerializeField] private AnimationCurve curve;

        public override IEnumerator PlayAnim(FlyerObject flyerObject)
        {
            void OnUpdate(float normalizedTime, Vector3 curValue)
            {
                flyerObject.SetAnchoredPosition(curValue);
                flyerObject.Img.SetAlpha(1 - normalizedTime);
            }
            Vector2 target = flyerObject.RectTransform.anchoredPosition + Vector2.up * offset;
            yield return TweenHelper.ChangeVectorValueByTimeWithEnumerator(flyerObject.RectTransform.anchoredPosition, target, duration, curve, OnUpdate);
        }
    }
}

