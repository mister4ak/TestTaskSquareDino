using UnityEngine;

namespace CodeBase.AssetManagment
{
    public class AssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, rotation);
        }
        
        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, rotation, parent);
        }
    }
}