using Game.StageItem.BassClass;

namespace Game.StageItem
{
    public class Stick:BaseHeroHandleItem
    {
        public override HeroHandleItemEnum ItemEnum { get; } = HeroHandleItemEnum.Stick;
        protected override float LightingIntensity => -3f;
    }
}