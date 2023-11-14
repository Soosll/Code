#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlmostEngine
{
    public class AssetUtils
    {
        public static List<T> LoadAll<T>(string path = "") where T : UnityEngine.Object
        {
            string filter = "t:";
            if (typeof(T) == typeof(GameObject))
            {
                filter += "prefab";
            }
            else
            {
                filter += typeof(T).ToString();
            }
            List<string> guids = AssetDatabase.FindAssets(filter).ToList<string>();
            // Debug.Log("T : " + typeof(T).ToString() + " Found guids " + guids.Count);
            // foreach (var s in guids)
            // {
            //     Debug.Log("s " + s);
            //     Debug.Log("p " + AssetDatabase.GUIDToAssetPath(s));
            //     Debug.Log("loading type " + typeof(T).ToString() + " : " + AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(T)));
            // }
            // AssetDatabase.LoadAllAssetsAtPath


            List<string> loadedGUIDS = new List<string>();

            List<T> assets = new List<T>();
            foreach (var id in guids)
            {
                // Only load a guid one time (for asset contained in an other asset)
                if (loadedGUIDS.Contains(id))
                    continue;
                loadedGUIDS.Add(id);

                if (path != "" && !id.Contains(path))
                    continue;

                // Debug.Log("Found id " + id);
                var paths = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(id));
                foreach (var obj in paths)
                {
                    if (obj != null && ((obj.GetType() == typeof(T)) || obj.GetType().IsSubclassOf(typeof(T))))
                    {
                        assets.Add((T)obj);
                        // if (typeof(T).ToString().Contains("Node"))
                        // {
                        //     Debug.Log("Asset " + obj.name);
                        // }
                    }

                }
                // assets.AddRange(objs
                // .Where(x => x != null && (x.GetType() == typeof(T) || x.GetType().IsSubclassOf(typeof(T))))
                // .Select(x => (T)x));
            }
            return assets;
        }

        public static T GetOrCreate<T>(string name, string path = "Assets/") where T : ScriptableObject
        {
            T asset = null;
            List<T> objs = LoadAll<T>();
            if (objs.Count == 0)
            {
                Debug.Log("Asset created at " + path);
                string fullpath = path + name + ".asset";
                asset = ScriptableObject.CreateInstance<T>();
                asset.name = name;
                AssetDatabase.CreateAsset(asset, fullpath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                asset = objs[0];
            }
            return asset;
        }

    }
}



#endif