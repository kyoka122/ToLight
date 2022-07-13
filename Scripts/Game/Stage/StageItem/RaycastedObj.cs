using System.Collections.Generic;
using UnityEngine;

namespace Game.StageItem
{
    public class RaycastedObj:MonoBehaviour
    {
        [SerializeField] private List<GameObject> raycastedRootObj;

        public List<GameObject> RaycastedRootObj => raycastedRootObj;
    }
}