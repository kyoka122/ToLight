using Game.StageItem.BassClass;

namespace Game.StageItem
{
    public class Door:BaseHeroHandleItem
    {
        public override HeroHandleItemEnum ItemEnum => HeroHandleItemEnum.Door;
        protected override float LightingIntensity => 3f;
    }
}