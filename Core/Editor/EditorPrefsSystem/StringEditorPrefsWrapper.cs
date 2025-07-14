using UnityEditor;



namespace JellyFramework.EditorPrefsSystem
{
    public class StringEditorPrefsWrapper : BaseEditorPrefsWrapper<string>
    {
        public StringEditorPrefsWrapper(string key, string defaultValue) : base(key, defaultValue) { }

        protected override string Load() => EditorPrefs.GetString(key);

        protected override void Save(string newValue) => EditorPrefs.SetString(key, newValue);
    }
}
