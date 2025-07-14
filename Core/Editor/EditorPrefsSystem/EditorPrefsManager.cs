using System.Collections.Generic;
using UnityEditor;

namespace JellyFramework.EditorPrefsSystem
{
    public static class EditorPrefsManager
    {
        private static Dictionary<string, BoolEditorPrefsWrapper> boolPrefsWrappers = new();
        private static Dictionary<string, StringEditorPrefsWrapper> stringPrefsWrappers = new();
        private static Dictionary<string, IntEditorPrefsWrapper> intPrefsWrappers = new();
        private static Dictionary<string, FloatEditorPrefsWrapper> floatPrefsWrappers = new();



        public static IntEditorPrefsWrapper GetIntEditorPrefsWrapper(string key)
        {
            if (!intPrefsWrappers.ContainsKey(key))
                intPrefsWrappers[key] = new IntEditorPrefsWrapper(key, 0);
            return intPrefsWrappers[key];
        }

        public static FloatEditorPrefsWrapper GetFloatEditorPrefsWrapper(string key)
        {
            if (!floatPrefsWrappers.ContainsKey(key))
                floatPrefsWrappers[key] = new FloatEditorPrefsWrapper(key, 0f);
            return floatPrefsWrappers[key];
        }

        public static StringEditorPrefsWrapper GetStringEditorPrefsWrapper(string key)
        {
            if (!stringPrefsWrappers.ContainsKey(key))
                stringPrefsWrappers[key] = new StringEditorPrefsWrapper(key, string.Empty);
            return stringPrefsWrappers[key];
        }

        public static BoolEditorPrefsWrapper GetBoolEditorPrefsWrapper(string key)
        {
            if (!boolPrefsWrappers.ContainsKey(key))
                boolPrefsWrappers[key] = new BoolEditorPrefsWrapper(key, false);
            return boolPrefsWrappers[key];
        }



    }

}
