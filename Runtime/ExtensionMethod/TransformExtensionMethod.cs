using UnityEngine;

namespace JellyFramework.ExtensionMethod
{
    public static class TransformExtensionMethod
    {
        public static void Copy(this Transform transform, Transform other)
        {
            transform.position = other.position;
            transform.rotation = other.rotation;
        }

        public static void SetData(this Transform transform, Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }


        public static void SetPosX(this Transform transform, float value)
        {
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }

        public static void SetPosY(this Transform transform, float value)
        {
            Vector3 pos = transform.position;
            pos.y = value;
            transform.position = pos;
        }

        public static void SetPosZ(this Transform transform, float value)
        {
            Vector3 pos = transform.position;
            pos.z = value;
            transform.position = pos;
        }

        public static void SetLocalPosX(this Transform transform, float value)
        {
            Vector3 pos = transform.localPosition;
            pos.x = value;
            transform.localPosition = pos;
        }

        public static void SetLocalPosY(this Transform transform, float value)
        {
            Vector3 pos = transform.localPosition;
            pos.y = value;
            transform.localPosition = pos;
        }

        public static void SetLocalPosZ(this Transform transform, float value)
        {
            Vector3 pos = transform.localPosition;
            pos.z = value;
            transform.localPosition = pos;
        }

        public static void SetAnchoredPosX(this RectTransform rectTransform, float value)
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x = value;
            rectTransform.anchoredPosition = pos;
        }

        public static void SetAnchoredPosY(this RectTransform rectTransform, float value)
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y = value;
            rectTransform.anchoredPosition = pos;
        }

        public static void SetEulerAngleX(this Transform transform, float value)
        {
            Vector3 angle = transform.eulerAngles;
            angle.x = value;
            transform.eulerAngles = angle;
        }

        public static void SetEulerAngleY(this Transform transform, float value)
        {
            Vector3 angle = transform.eulerAngles;
            angle.y = value;
            transform.eulerAngles = angle;
        }


        public static void SetEulerAngleZ(this Transform transform, float value)
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = value;
            transform.eulerAngles = angle;
        }

        public static void SetLocalEulerAngleX(this Transform transform, float value)
        {
            Vector3 angle = transform.localEulerAngles;
            angle.x = value;
            transform.localEulerAngles = angle;
        }

        public static void LookToTargetOnXZPlane(this Transform transform, Vector3 target)
        {
            Vector3 direction = target - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
                transform.forward = direction.normalized;
        }

        //public static void RotateToward(this Transform trans, Vector3 target, float maxRadiansDelta)
        //{
        //    Vector3 direction = target - trans.position;
        //    direction.y = 0;
        //    if (direction != Vector3.zero)
        //        trans.forward = Vector3.RotateTowards(trans.forward, direction.normalized, maxRadiansDelta, 0.0f);
        //}
    }
}

