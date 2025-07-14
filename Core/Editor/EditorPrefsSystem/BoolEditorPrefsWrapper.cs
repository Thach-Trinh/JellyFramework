using UnityEditor;



namespace JellyFramework.EditorPrefsSystem
{
    public class BoolEditorPrefsWrapper : BaseEditorPrefsWrapper<bool>
    {
        public BoolEditorPrefsWrapper(string key, bool defaultValue) : base(key, defaultValue) { }

        protected override bool Load() => EditorPrefs.GetInt(key) != 0;

        protected override void Save(bool newValue) => EditorPrefs.SetInt(key, newValue ? 1 : 0);
    }
}