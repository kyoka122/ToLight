using UnityEngine;

namespace Game.StageItem.Interface
{
    public interface IHeroHandleItem
    {
        public HeroHandleItemEnum ItemEnum { get; }
        public float Weight { get; }
        public Transform HeroRightHandSetPoint { get; }
        public Transform HeroLeftHandSetPoint { get; }
        public void FreezePosition();
        public void FreeFreezingPosition();
        public Joint GetJoint();
        public void RemoveJoint();
        public void OnSelectedLight();
        public void OffSelectedLight();
    }
    
}