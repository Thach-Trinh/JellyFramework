using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using JellyFramework.ExtensionMethod;
using System.Security.Cryptography;

namespace JellyFramework.Tween
{
    public class TweenHelper : MonoBehaviour
    {
        private const float EPSILON = 0.00001f;

        public static IEnumerator ChangeFloatValueWithTimeIE(float startValue, float endValue, float duration, AnimationCurve curve,
            Action<float, float> action, Func<bool> pauseCondition)
            => ChangeValueWithTimerIE(startValue, endValue, duration, curve, Mathf.Lerp, action, pauseCondition);

        public static IEnumerator ChangeVectorValueWithTimeIE(Vector3 startValue, Vector3 endValue, float duration, AnimationCurve curve,
            Action<float, Vector3> action, Func<bool> pauseCondition)
            => ChangeValueWithTimerIE(startValue, endValue, duration, curve, Vector3.Lerp, action, pauseCondition);

        public static IEnumerator ChangeQuaternionValueWithTimeIE(Quaternion startValue, Quaternion endValue, float duration, AnimationCurve curve,
            Action<float, Quaternion> action, Func<bool> pauseCondition)
            => ChangeValueWithTimerIE(startValue, endValue, duration, curve, Quaternion.Lerp, action, pauseCondition);


        public static async UniTask ChangeFloatValueWithTimeAsync(float startValue, float endValue, float duration, AnimationCurve curve,
            Action<float, float> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueWithTimeAsync(startValue, endValue, duration, curve, Mathf.Lerp, action, pauseCondition, source);

        public static async UniTask ChangeVectorValueWithTimeAsync(Vector3 startValue, Vector3 endValue, float duration, AnimationCurve curve,
            Action<float, Vector3> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueWithTimeAsync(startValue, endValue, duration, curve, Vector3.Lerp, action, pauseCondition, source);


        public static async UniTask ChangeQuaternionValueWithTimeAsync(Quaternion startValue, Quaternion endValue, float duration, AnimationCurve curve,
            Action<float, Quaternion> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueWithTimeAsync(startValue, endValue, duration, curve, Quaternion.Lerp, action, pauseCondition, source);



        private static IEnumerator ChangeValueWithTimerIE<T>(T startValue, T endValue, float duration, AnimationCurve curve,
            Func<T, T, float, T> lerp, Action<float, T> action, Func<bool> pauseCondition)
        {
            float elapsedTime = 0;
            action?.Invoke(0, startValue);
            while (elapsedTime <= duration)
            {
                yield return null;
                UpdateValueByTime(startValue, endValue, duration, curve, lerp, action, pauseCondition, ref elapsedTime);
            }
        }

        private static async UniTask ChangeValueWithTimeAsync<T>(T startValue, T endValue, float duration, AnimationCurve curve,
            Func<T, T, float, T> lerp, Action<float, T> action, Func<bool> pauseCondition, CancellationTokenSource source)
        {
            float elapsedTime = 0;
            action?.Invoke(0, startValue);
            while (elapsedTime <= duration)
            {
                await UniTask.Yield(source.Token);
                UpdateValueByTime(startValue, endValue, duration, curve, lerp, action, pauseCondition, ref elapsedTime);
            }
        }

        private static void UpdateValueByTime<T>(T startValue, T endValue, float duration, AnimationCurve curve,
            Func<T, T, float, T> lerp, Action<float, T> action, Func<bool> pauseCondition, ref float elapsedTime)
        {
            if (pauseCondition == null || !pauseCondition())
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > duration)
                    elapsedTime = duration;
                float normalizedTime = elapsedTime / duration;
                float easedValue = (curve != null ? curve.Evaluate(normalizedTime) : normalizedTime);
                T curValue = lerp(startValue, endValue, easedValue);
                action?.Invoke(normalizedTime, curValue);
            }
        }


        private static IEnumerator ChangeValueWithSpeedIE<T>(T startValue, T endValue, float speed, Func<T, T, float> getDistance, Func<T, T, float, T> moveTowards, Action<T> action)
        {
            T curValue = startValue;
            action?.Invoke(curValue);
            while (getDistance(curValue, endValue) > EPSILON)
            {
                yield return null;
                curValue = moveTowards(curValue, endValue, speed * Time.deltaTime);
                action?.Invoke(curValue);
            }
        }



        private static async UniTask ChangeValueBySpeedWithTask<T>(T startValue, T endValue, float speed, Func<T, T, float> getDistance, Func<T, T, float, T> moveTowards, Action<T> action,
            CancellationTokenSource source, Func<bool> pauseCondition = null)
        {
            T curValue = startValue;
            while (getDistance(curValue, endValue) > EPSILON)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    curValue = moveTowards(curValue, endValue, speed * Time.deltaTime);
                    action?.Invoke(curValue);
                }
                await UniTask.Yield(source.Token);
            }
        }

        private static float GetDistance(float a, float b) => Mathf.Abs(a - b);

        public static async UniTask ChangeFloatValueBySpeedWithTask(float startValue, float endValue, float speed, Action<float> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueBySpeedWithTask(startValue, endValue, speed, GetDistance, Mathf.MoveTowards, action, source, pauseCondition);

        public static IEnumerator ChangeFloatValueBySpeedWithEnumerator(float startValue, float endValue, float speed, Action<float> action)
            => ChangeValueWithSpeedIE(startValue, endValue, speed, GetDistance, Mathf.MoveTowards, action);

        public static IEnumerator ChangeQuaternionValueBySpeedWithEnumerator(Quaternion startValue, Quaternion endValue, float speed, Action<Quaternion> action)
            => ChangeValueWithSpeedIE(startValue, endValue, speed, Quaternion.Angle, Quaternion.RotateTowards, action);


    }
}
