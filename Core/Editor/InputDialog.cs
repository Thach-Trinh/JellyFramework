using System;
using UnityEditor;
using UnityEngine;

namespace JellyFramework
{
    public class InputDialog : EditorWindow
    {
        private string input;
        private string label;
        private Action<string> onConfirm;

        public static void Show(string title, string label, Action<string> onConfirm)
        {
            InputDialog window = GetWindow<InputDialog>(title);
            //window.titleContent = new GUIContent(title);
            window.label = label;
            window.onConfirm = onConfirm;
            window.minSize = new Vector2(300, 70);
            window.maxSize = new Vector2(300, 70);
            //window.ShowUtility();
        }

        private void OnGUI()
        {
            input = EditorGUILayout.TextField(label, input);
            if (GUILayout.Button("Confirm"))
            {
                onConfirm?.Invoke(input);
                Close();
            }
        }
    }
}

