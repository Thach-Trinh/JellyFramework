using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JellyFramework.SearchWindowUtility
{
    public static class SearchWindowHelper
    {
        public static void ShowSelectButtonToOpenSearchWindowForSingleGroup<T>(string curSelection, string messageOnEmpty, string title, T[] database, Action<object> load)// where T : Object
        {
            if (GUILayout.Button(string.IsNullOrEmpty(curSelection) ? messageOnEmpty : curSelection, EditorStyles.popup))
                OpenSearchWindowForSingleGroup(title, database, load);
        }

        public static void OpenSearchWindowForSingleGroup<T>(string title, T[] database, Action<object> load)// where T : Object
        {
            List<SearchTreeEntry> CreateSearchTreeForSingleGroup(SearchWindowContext context)
            {
                List<SearchTreeEntry> list = new List<SearchTreeEntry>();
                list.Add(GetSearchTreeGroupEntry(title, 0));
                for (int i = 0; i < database.Length; i++)
                    list.Add(GetSearchTreeEntry(database[i], 1));
                return list;
            }
            DelegateSearchWindowProvider provider = ScriptableObject.CreateInstance<DelegateSearchWindowProvider>();
            provider.Init(CreateSearchTreeForSingleGroup, load);
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
        }

        public static void OpenSearchWindowForMultipleGroups<T1, T2>(string title, T1[] groups, Func<T1, T2[]> getDatabase, Action<object> load) where T1 : Object where T2 : Object
        {
            List<SearchTreeEntry> CreateSearchTreeForMultipleGroup(SearchWindowContext context)
            {
                List<SearchTreeEntry> list = new List<SearchTreeEntry>();
                list.Add(GetSearchTreeGroupEntry(title, 0));
                for (int i = 0; i < groups.Length; i++)
                {
                    T1 group = groups[i];
                    list.Add(GetSearchTreeGroupEntry(group.name, 1));

                    T2[] database = getDatabase(group);
                    for (int j = 0; j < database.Length; j++)
                        list.Add(GetSearchTreeEntry(database[j], 2));
                }
                return list;
            }
            DelegateSearchWindowProvider provider = ScriptableObject.CreateInstance<DelegateSearchWindowProvider>();
            provider.Init(CreateSearchTreeForMultipleGroup, load);
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
        }

        private static SearchTreeGroupEntry GetSearchTreeGroupEntry(string title, int level) => new SearchTreeGroupEntry(new GUIContent(title), level);

        private static SearchTreeEntry GetSearchTreeEntry(object data, int level)
        {
            string GetCleanTitle(string title)
            {
                if (string.IsNullOrEmpty(title))
                    return title;
                int index = title.LastIndexOf('(');
                if (index != -1)
                    return title.Substring(0, index).Trim();
                return title;
            }
            GUIContent GetContent()
            {
                if (data is Object)
                {
                    GUIContent content1 = EditorGUIUtility.ObjectContent(data as Object, data.GetType());
                    return new GUIContent(GetCleanTitle(content1.text), content1.image);
                }
                return new GUIContent(data.ToString(), EditorGUIUtility.IconContent("GameObject Icon").image);
            }
            //GUIContent content1 = EditorGUIUtility.ObjectContent(data, data.GetType());
            GUIContent content2 = GetContent();
            SearchTreeEntry searchTreeEntry = new SearchTreeEntry(content2);
            searchTreeEntry.level = level;
            searchTreeEntry.userData = data;
            return searchTreeEntry;
        }
    }
}