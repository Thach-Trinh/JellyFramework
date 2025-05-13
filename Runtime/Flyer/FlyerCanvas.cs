using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.FlyerSystem
{
    public class FlyerCanvas : MonoBehaviour
    {
        [SerializeField] private FlyerAnimationPlayerFactory playerFactory;
        [SerializeField] private FlyerObject objPrefab;
        [SerializeField] private RectTransform rectTransform;
        private Stack<FlyerObject> objPool = new Stack<FlyerObject>();

        public void SpawnFlyer(Vector3 worldPos, FlyerAnimationStyle style)
        {
            Vector2 anchoredPos = PositionConverter.ConvertWorldPosToAnchoredPos(Camera.main, worldPos, rectTransform, 0.5f * Vector2.one);
            FlyerObject obj = GetObj();
            obj.SetAnchoredPosition(anchoredPos);
            StartCoroutine(PlayAnim(obj, style));
        }

        private IEnumerator PlayAnim(FlyerObject newTxt, FlyerAnimationStyle style)
        {
            //yield return newTxt.PlayAnim();
            //yield break;
            yield return playerFactory.GetPlayer(style).PlayAnim(newTxt);
            newTxt.gameObject.SetActive(false);
            objPool.Push(newTxt);
        }


        private FlyerObject GetObj()
        {
            if (objPool.Count > 0)
            {
                FlyerObject obj = objPool.Pop();
                obj.gameObject.SetActive(true);
                //obj.transform.SetParent(activeObjParent);
                obj.transform.SetAsLastSibling();
                return obj;
            }
            return Instantiate(objPrefab, rectTransform);
        }
    }
}

