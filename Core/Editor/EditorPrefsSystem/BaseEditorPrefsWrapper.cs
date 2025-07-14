using UnityEditor;


namespace JellyFramework.EditorPrefsSystem
{
    public abstract class BaseEditorPrefsWrapper<T>
    {
        protected string key;
        private T value;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                Save(value);
            }
        }

        protected abstract void Save(T newValue);
        protected abstract T Load();


        public BaseEditorPrefsWrapper(string key, T defaultValue)
        {
            Init(key, defaultValue);
        }

        private void Init(string key, T defaultValue)
        {
            this.key = key;
            if (EditorPrefs.HasKey(key))
                value = Load();
            else
            {
                value = defaultValue;
                Save(defaultValue);
            }
        }
    }

}


