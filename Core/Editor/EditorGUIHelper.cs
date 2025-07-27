using System.Collections.Generic;
using JellyFramework.EditorPrefsSystem;
using UnityEditor;
using UnityEngine;
using System;
using JellyFramework.EditorTheme;
using Object = UnityEngine.Object;

namespace JellyFramework
{
    public static class EditorGUIHelper
    {
        public static void CreateDefaultFoldout(string content, Action action, string key)
        {
            BoolEditorPrefsWrapper boolEditorPrefsWrapper = EditorPrefsManager.GetBoolEditorPrefsWrapper(key);
            CreateDefaultFoldout(content, action, boolEditorPrefsWrapper);
        }

        public static void CreateFoldoutHeader(string content, Action action, string key)
        {
            BoolEditorPrefsWrapper boolEditorPrefsWrapper = EditorPrefsManager.GetBoolEditorPrefsWrapper(key);
            CreateFoldoutHeader(content, action, boolEditorPrefsWrapper);
        }

        public static void CreateRegion(string label, Color color, Action action, string key)
        {
            BoolEditorPrefsWrapper boolEditorPrefsWrapper = EditorPrefsManager.GetBoolEditorPrefsWrapper(key);
            CreateRegion(label, color, action, boolEditorPrefsWrapper);
        }

        private static void CreateDefaultFoldout(string content, Action action, BoolEditorPrefsWrapper prefsWrapper)
        {
            bool oldValue = prefsWrapper.Value;
            bool newValue = EditorGUILayout.Foldout(prefsWrapper.Value, content, true);
            if (newValue != oldValue)
                prefsWrapper.Value = newValue;
            if (newValue)
            {
                EditorGUI.indentLevel++;
                action?.Invoke();
                EditorGUI.indentLevel--;
            }
        }

        private static void CreateFoldoutHeader(string content, Action action, BoolEditorPrefsWrapper prefsWrapper)
        {
            bool oldValue = prefsWrapper.Value;
            bool newValue = EditorGUILayout.BeginFoldoutHeaderGroup(prefsWrapper.Value, content);
            if (newValue != oldValue)
                prefsWrapper.Value = newValue;
            if (newValue)
            {
                EditorGUI.indentLevel++;
                action?.Invoke();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private static void CreateRegion(string label, Color color, Action action, BoolEditorPrefsWrapper prefHandler)
        {
            GUI.backgroundColor = color;
            EditorGUILayout.BeginVertical(EditorThemeController.CurrentThemeName);
            EditorGUI.indentLevel++;
            bool oldValue = prefHandler.Value;
            bool newValue = EditorGUILayout.Foldout(prefHandler.Value, label, true);
            if (newValue != oldValue)
                prefHandler.Value = newValue;
            if (newValue)
            {
                EditorGUI.indentLevel++;
                action.Invoke();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = Color.white;
        }

        public static void ShowList<T>(string title, List<T> lst, Action<int, T> showElement,
            UnityEngine.Object owner, bool showAddBtn, bool showRemoveBtn, string key)
        {
            void ShowElements()
            {
                int? removedPatternIndex = null;
                for (int i = 0; i < lst.Count; i++)
                {
                    int index = i;
                    EditorGUILayout.BeginHorizontal();
                    showElement(index, lst[index]);
                    if (showRemoveBtn && GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash").image, GUILayout.Width(20)))
                        removedPatternIndex = i;
                    EditorGUILayout.EndHorizontal();
                }
                if (removedPatternIndex != null)
                {
                    if (owner != null)
                        Undo.RegisterCompleteObjectUndo(owner, "Remove Element");
                    lst.RemoveAt(removedPatternIndex.Value);
                }
                if (showAddBtn && GUILayout.Button("Add Element"))
                {
                    if (owner != null)
                        Undo.RegisterCompleteObjectUndo(owner, "Add Element");
                    lst.Add(default);
                }
            }
            if (lst == null)
            {
                EditorGUILayout.LabelField($"{title}: null");
                return;
            }
            CreateDefaultFoldout($"{title}: {lst.Count}", ShowElements, key);
        }


        public static void ShowDictionary<TKey, TValue>(string title, Dictionary<TKey, TValue> dict, Action<TKey> showKey, Action<TValue> showValue, string key)
        {
            void ShowElements()
            {
                foreach (KeyValuePair<TKey, TValue> kvp in dict)
                {
                    EditorGUILayout.BeginHorizontal();
                    showKey?.Invoke(kvp.Key);
                    showValue?.Invoke(kvp.Value);
                    EditorGUILayout.EndHorizontal();
                }
            }
            if (dict == null)
            {
                EditorGUILayout.LabelField($"{title}: null");
                return;
            }
            CreateDefaultFoldout($"{title}: {dict.Count}", ShowElements, key);
        }

        public static T ShowGenericField<T>(string label, T target, Func<T, string> getInfo = null)
        {
            if (typeof(T) == typeof(int))
                return (T)(object)(EditorGUILayout.IntField(label, (int)(object)target));
            if (typeof(T) == typeof(float))
                return (T)(object)EditorGUILayout.FloatField(label, (float)(object)target);
            if (typeof(T) == typeof(bool))
                return (T)(object)EditorGUILayout.Toggle(label, (bool)(object)target);
            if (typeof(T) == typeof(string))
                return (T)(object)EditorGUILayout.TextField(label, (string)(object)target);
            if (typeof(T) == typeof(Vector2))
                return (T)(object)EditorGUILayout.Vector2Field(label, (Vector2)(object)target);
            if (typeof(T) == typeof(Vector3))
                return (T)(object)EditorGUILayout.Vector3Field(label, (Vector3)(object)target);
            if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)))
                return (T)(object)EditorGUILayout.ObjectField(label, (UnityEngine.Object)(object)target, typeof(T), true);
            EditorGUILayout.LabelField(label, getInfo != null ? getInfo(target) : target.ToString());
            return target;
        }

        public static void ShowEnumGUI<T>(string label, Object obj, ref T curValue) where T : Enum
        {
            T newValue = (T)EditorGUILayout.EnumPopup(label, curValue);
            if (!newValue.Equals(curValue))
            {
                Undo.RegisterCompleteObjectUndo(obj, "Enum Value");
                curValue = newValue;
            }
        }

        public static void ShowIntGUI(string label, Object obj, ref int curValue)
        {
            int newValue = EditorGUILayout.IntField(label, curValue);
            if (newValue != curValue)
            {
                Undo.RegisterCompleteObjectUndo(obj, "Int Value");
                curValue = newValue;
            }
        }

        public static void ShowFloatGUI(string label, Object obj, ref float curValue)
        {
            float newValue = EditorGUILayout.FloatField(label, curValue);
            if (newValue != curValue)
            {
                Undo.RegisterCompleteObjectUndo(obj, "Float Value");
                curValue = newValue;
            }
        }

        public static void ShowStringGUI(string label, Object obj, ref string curValue)
        {
            string newValue = EditorGUILayout.TextField(label, curValue);
            if (newValue != curValue)
            {
                Undo.RegisterCompleteObjectUndo(obj, "String Value");
                curValue = newValue;
            }
        }

        public static void ShowBoolGUI(string label, Object obj, ref bool curValue)
        {
            bool newValue = EditorGUILayout.Toggle(label, curValue);
            if (newValue != curValue)
            {
                Undo.RegisterCompleteObjectUndo(obj, "Bool Value");
                curValue = newValue;
            }
        }
    }
}
