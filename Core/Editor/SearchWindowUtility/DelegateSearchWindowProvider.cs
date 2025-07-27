using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JellyFramework.SearchWindowUtility
{
    public class DelegateSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        Func<SearchWindowContext, List<SearchTreeEntry>> createSearchTree;
        Action<object> onSelect;

        public void Init(Func<SearchWindowContext, List<SearchTreeEntry>> createSearchTree, Action<object> onSelect)
        {
            this.createSearchTree = createSearchTree;
            this.onSelect = onSelect;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) => createSearchTree(context);

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            onSelect?.Invoke(SearchTreeEntry.userData);
            return true;
        }
    }

}
