using Game.StageItem.BassClass;
using Game.StageItem.Interface;
using UnityEngine;
using Utility;

namespace Game.StageItem
{
    public class Chair:BaseHeroHandleItem,IHeroStandableItem
    {
        public override HeroHandleItemEnum ItemEnum => HeroHandleItemEnum.Chair;
        protected override float LightingIntensity => 3f;
    }
}