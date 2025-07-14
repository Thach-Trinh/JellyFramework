
using UnityEditor;

namespace JellyFramework.EditorTheme
{
    public static class EditorThemeController
    {
        private static string currentThemeName;
        private static EditorThemeStyle currentThemeStyle;

        public static string CurrentThemeName => currentThemeName;

        static EditorThemeController() => SetTheme(EditorThemeStyle.Box);

        public static void OnGUI()
        {
            EditorThemeStyle newTheme = (EditorThemeStyle)EditorGUILayout.EnumPopup("Theme", currentThemeStyle);
            if (newTheme != currentThemeStyle)
                SetTheme(newTheme);
        }

        private static void SetTheme(EditorThemeStyle newTheme)
        {
            string GetStyleName(EditorThemeStyle theme)
            {
                switch (theme)
                {
                    case EditorThemeStyle.Box: return "box";
                    case EditorThemeStyle.HelpBox: return "helpbox";
                    case EditorThemeStyle.Button: return "button";
                    default: return default;
                }
            }
            currentThemeStyle = newTheme;
            currentThemeName = GetStyleName(newTheme);
        }
    }
}

