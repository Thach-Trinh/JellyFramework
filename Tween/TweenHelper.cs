using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using JellyFramework.ExtensionMethod;

namespace JellyFramework.Tween
{
    public class TweenHelper : MonoBehaviour
    {
        private const float EPSILON = 0.00001f;
        private CancellationTokenSource mainSource;
        private CancellationToken destroyToken;
        private CancellationTokenSource linkedSource;

        private void Awake()
        {
            mainSource = new CancellationTokenSource();
            destroyToken = this.GetCancellationTokenOnDestroy();
            linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
        }

        public async UniTask MoveAlongPath(Transform trans, List<Vector3> path, float moveSpeed, bool smoothRotate = false, float rotateSpeed = 0, Func<bool> pauseCondition = null)
        {
            int curPointIndex = 0;
            trans.LookToTargetOnXZPlane(path[curPointIndex]);
            while (path.Count > 0)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    Vector3 targetPoint = path[0];
                    if (smoothRotate)
                        trans.RotateToward(targetPoint, rotateSpeed * Time.deltaTime);
                    trans.position = Vector3.MoveTowards(trans.position, targetPoint, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(trans.position, targetPoint) <= EPSILON)
                    {
                        path.RemoveAt(0);
                        if (path.Count > 0 && !smoothRotate)
                            trans.LookToTargetOnXZPlane(path[0]);
                    }
                }
                await UniTask.Yield(linkedSource.Token);
            }
        }

        public async UniTask WaitToMoveAlongPath(Transform trans, float delay, List<Vector3> path, float speed, Func<bool> pauseCondition = null)
        {
            await Delay(delay, pauseCondition);
            await MoveAlongPath(trans, path, speed, pauseCondition: pauseCondition);
        }

        public async UniTask MoveToTarget(Transform trans, Vector3 target, float speed, bool lookToTarget = false, Func<bool> pauseCondition = null, CancellationTokenSource additionalSource = null)
        {
            CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
            if (additionalSource != null)
                linkedSource = CancellationTokenSource.CreateLinkedTokenSource(linkedSource.Token, additionalSource.Token);
            if (lookToTarget)
                trans.LookToTargetOnXZPlane(target);
            while (Vector3.Distance(trans.position, target) > EPSILON)
            {
                if (pauseCondition == null || !pauseCondition())
                    trans.position = Vector3.MoveTowards(trans.position, target, speed * Time.deltaTime);
                await UniTask.Yield(linkedSource.Token);
            }
            trans.position = target;
        }

        public async UniTask RotateToTarget(Transform trans, Vector3 target, float speed, Func<bool> pauseCondition = null, CancellationTokenSource additionalSource = null)
        {
            CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
            if (additionalSource != null)
                linkedSource = CancellationTokenSource.CreateLinkedTokenSource(linkedSource.Token, additionalSource.Token);
            while (Vector3.Distance(trans.position, target) > EPSILON)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    //trans.position = Vector3.MoveTowards(trans.position, target, speed * Time.deltaTime);
                    //Mathf.MoveTowards
                }
                await UniTask.Yield(linkedSource.Token);
            }
            trans.position = target;
        }



        public async UniTask RotateBySpeed(Transform trans, float speed, Quaternion targetRotation, Func<bool> pauseCondition = null, CancellationTokenSource additionalSource = null)
        {
            CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
            if (additionalSource != null)
                linkedSource = CancellationTokenSource.CreateLinkedTokenSource(linkedSource.Token, additionalSource.Token);
            float angle = Quaternion.Angle(trans.rotation, targetRotation);
            while (angle > 5f)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    trans.rotation = Quaternion.Slerp(trans.rotation, targetRotation, speed * Time.deltaTime); //Quaternion.RotateTowards(trans.rotation, targetRotation, degreeAngle);
                    angle = Quaternion.Angle(trans.rotation, targetRotation);
                }
                await UniTask.Yield(linkedSource.Token);
            }
            trans.rotation = targetRotation;
        }


        public async UniTask Slerp(Transform trans, Vector3 target, float centerOffset, float duration, Func<bool> pauseCondition = null)
        {
            CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
            Vector3 startPos = trans.position;
            Vector3 center = (startPos + target) / 2f - centerOffset * Vector3.up;
            Vector3 relA = startPos - center;
            Vector3 relB = target - center;
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > duration)
                        elapsedTime = duration;
                    trans.position = Vector3.Slerp(relA, relB, elapsedTime / duration) + center;
                }
                await UniTask.Yield(linkedSource.Token);
            }
        }



        public async UniTask ExecuteCommands(int amount, Action<int> command, float delay, Func<bool> pauseCondition = null)
        {
            for (int i = 0; i < amount; i++)
            {
                command?.Invoke(i);
                await Delay(delay, pauseCondition);
            }
        }

        public async UniTask Delay(float delay, Func<bool> pauseCondition = null)
        {
            //CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(gameplaySource.Token, destroyToken);
            float elepsedTime = 0;
            while (elepsedTime < delay)
            {
                if (pauseCondition == null || !pauseCondition())
                    elepsedTime += Time.deltaTime;
                await UniTask.Yield(linkedSource.Token);
            }
        }

        public async UniTask WaitUntil(Func<bool> predicate)
        {
            CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
            await UniTask.WaitUntil(predicate, cancellationToken: linkedSource.Token);
        }



        private static IEnumerator ChangeValueByTimerWithEnumerator<T>(T startValue, T endValue, float duration, AnimationCurve curve,
            Func<T, T, float, T> lerp, Action<float, T> action)
        {
            curve ??= AnimationCurve.Linear(0, 0, 1, 1);
            float elapsedTime = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = elapsedTime / duration;
                float easedValue = curve.Evaluate(normalizedTime);
                T curValue = lerp(startValue, endValue, easedValue);
                action?.Invoke(normalizedTime, curValue);
                yield return null;
            }
        }

        private static IEnumerator ChangeValueBySpeedWithEnumerator<T>(T startValue, T endValue, float speed, Func<T, T, float> getDistance, Func<T, T, float, T> moveTowards, Action<T> action)
        {
            T curValue = startValue;
            while (getDistance(curValue, endValue) > EPSILON)
            {
                curValue = moveTowards(curValue, endValue, speed * Time.deltaTime);
                action?.Invoke(curValue);
                yield return null;
            }
        }

        private static async UniTask ChangeValueByTimeWithTask<T>(T startValue, T endValue, float duration, Func<T, T, float, T> lerp, Action<float, T> action,
            CancellationTokenSource source, Func<bool> pauseCondition = null)
        {
            float elapsedTime = 0;
            while (elapsedTime <= duration)
            {
                if (pauseCondition == null || !pauseCondition())
                {
                    elapsedTime += Time.deltaTime;
                    float ratio = elapsedTime / duration;
                    T curValue = lerp(startValue, endValue, ratio);
                    action?.Invoke(ratio ,curValue);
                }
                await UniTask.Yield(source.Token);
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


        public static async UniTask ChangeFloatValueByTimeWithTask(float startValue, float endValue, float duration, Action<float, float> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueByTimeWithTask(startValue, endValue, duration, Mathf.Lerp, action, source, pauseCondition);

        public static async UniTask ChangeVectorValueByTimeWithTask(Vector3 startValue, Vector3 endValue, float duration, Action<float, Vector3> action, CancellationTokenSource source, Func<bool> pauseCondition = null)
            => await ChangeValueByTimeWithTask(startValue, endValue, duration, Vector3.Lerp, action, source, pauseCondition);



        public static IEnumerator ChangeFloatValueByTimeWithEnumerator(float startValue, float endValue, float duration, AnimationCurve curve, Action<float, float> action)
            => ChangeValueByTimerWithEnumerator(startValue, endValue, duration, curve, Mathf.Lerp, action);

        public static IEnumerator ChangeVectorValueByTimeWithEnumerator(Vector3 startValue, Vector3 endValue, float duration, AnimationCurve curve, Action<float, Vector3> action)
            => ChangeValueByTimerWithEnumerator(startValue, endValue, duration, curve, Vector3.Lerp, action);

        public static IEnumerator ChangeFloatValueBySpeedWithEnumerator(float startValue, float endValue, float speed, Action<float> action)
            => ChangeValueBySpeedWithEnumerator(startValue, endValue, speed, GetDistance, Mathf.MoveTowards, action);

        public static IEnumerator ChangeQuaternionValueBySpeedWithEnumerator(Quaternion startValue, Quaternion endValue, float speed, Action<Quaternion> action)
            => ChangeValueBySpeedWithEnumerator(startValue, endValue, speed, Quaternion.Angle, Quaternion.RotateTowards, action);
            

        public void CancelAllTween()
        {
            mainSource.Cancel();
            mainSource = new CancellationTokenSource();
            linkedSource = CancellationTokenSource.CreateLinkedTokenSource(mainSource.Token, destroyToken);
        }
    }
}
