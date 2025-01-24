using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class FXManager : Singleton<FXManager>
    {
        // public Dictionary<FXType, GameObject> fxPrefabs = new();
        public GameObject fxPrefab;

        // public enum FXType
        // {
        //     NONE,
        //     PHYSICAL,
        //     MAGICAL,
        // }

        public void PlayFX(Vector3 pos)
        {
            Instantiate(fxPrefab, pos, Quaternion.identity);
        }
    }
}