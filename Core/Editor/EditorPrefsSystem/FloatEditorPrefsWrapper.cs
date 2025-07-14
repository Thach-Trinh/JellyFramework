using UnityEditor;



namespace JellyFramework.EditorPrefsSystem
{
    public class FloatEditorPrefsWrapper : BaseEditorPrefsWrapper<float>
    {
        public FloatEditorPrefsWrapper(string key, float defaultValue) : base(key, defaultValue) { }

        protected override float Load() => EditorPrefs.GetFloat(key);

        protected override void Save(float newValue) => EditorPrefs.SetFloat(key, newValue);
    }
}

