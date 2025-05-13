using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFramework
{
    public class InputHandler : MonoBehaviour, IPointerDownHandler
    {
        public Action<Vector2> onPointerDown;

        private void Start()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Down");
            onPointerDown?.Invoke(eventData.position);
        }
    }
}