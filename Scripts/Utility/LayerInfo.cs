using UnityEngine;

namespace Utility
{
    public static class LayerInfo
    {
        private const int Hero = 6;
        private const int HeroHandleableItem = 7;
        public static readonly int StandableItem = 8;
        public static readonly int Stairs = 11;
        
        //MEMO: RaycastAllで取得したいレイヤーを全て含める
        public static LayerMask lightRaycastLayerMask = 1 << Hero
                                                        | 1 << HeroHandleableItem
                                                        | 1 << StandableItem;

        public static LayerMask heroRaycastLayerMask = 1 << HeroHandleableItem
                                                       | 1 << StandableItem;

        public static LayerMask standableItemLayerMask = 1 << StandableItem
                                                         | 1 << Stairs;

        public static string standableItemTag = "Standable";
    }
}