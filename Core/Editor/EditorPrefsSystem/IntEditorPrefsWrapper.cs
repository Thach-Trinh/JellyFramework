using UnityEditor;


namespace JellyFramework.EditorPrefsSystem
{
    public class IntEditorPrefsWrapper : BaseEditorPrefsWrapper<int>
    {
        public IntEditorPrefsWrapper(string key, int defaultValue) : base(key, defaultValue) { }

        protected override int Load() => EditorPrefs.GetInt(key);

        protected override void Save(int newValue) => EditorPrefs.SetInt(key, newValue);
    }
}
