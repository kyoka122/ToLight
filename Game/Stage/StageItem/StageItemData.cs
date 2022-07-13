using Game.StageItem.Roof.Interface;
using UnityEngine;

namespace Game.StageItem
{
    public class StageItemData:MonoBehaviour
    {
        [SerializeField] private GameObject roofBoss;
        public IBoss RoofBoss => roofBoss.GetComponent<IBoss>();
    }
}