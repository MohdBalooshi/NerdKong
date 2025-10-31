using System.IO;
using UnityEngine;

namespace NerdKong.Utils
{
    public static class JsonLoader
    {
        public static T LoadFromResources<T>(string resourcePathWithoutExt) where T : class
        {
            TextAsset asset = Resources.Load<TextAsset>(resourcePathWithoutExt);
            if(asset == null)
            {
                Debug.LogError($"[JsonLoader] Could not find TextAsset at Resources/{resourcePathWithoutExt}");
                return null;
            }
            return JsonUtility.FromJson<T>(asset.text);
        }
    }
}
