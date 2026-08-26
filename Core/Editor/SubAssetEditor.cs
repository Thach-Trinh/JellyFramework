using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JellyFramework
{
    public class SubAssetEditor
    {
        private Object owner;
        private List<Object> assets;
        private Func<Object> createAsset;
        private string key;

        public SubAssetEditor(Object owner, string key, Func<Object> createAsset)
        {
            this.owner = owner;
            this.key = key;
            this.createAsset = createAsset;
            assets = new List<Object>();
            Refresh();
        }

        public void OnGUI()
        {
            EditorGUIHelper.ShowList("Sub Assets", assets, ShowElement,
                owner, key, true, createAsset, true, Delete);
            if (GUILayout.Button("Refresh"))
                Refresh();
        }


        private void ShowElement(int index, Object asset) => EditorGUILayout.ObjectField(asset, typeof(Object), false);

        public void Refresh()
        {
            assets.Clear();
            string path = AssetDatabase.GetAssetPath(owner);
            assets.AddRange(AssetDatabase.LoadAllAssetsAtPath(path).Where(x => x != owner));
        }

        public void Delete(Object asset)
        {
            AssetDatabase.RemoveObjectFromAsset(asset);
            //Object.DestroyImmediate(asset, true);
            AssetDatabase.SaveAssets();
        }
    }
}


