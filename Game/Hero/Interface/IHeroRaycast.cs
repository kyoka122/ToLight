using Game.SpotLight;
using Game.StageItem.Interface;
using UnityEngine;

namespace Game.Hero
{
    public interface IHeroRaycast
    {
        //public List<RaycastHit> raycastHits { get; }
        public void Init();
        public bool TryGetJumpableSpot(ISpotLight spotLight, out RaycastHit jumpableSpot);
        public bool TryGetCanGoingUpStairsSpot(ISpotLight spotLight, out RaycastHit canGoingUpStairsSpot);
        public IHeroHandleItem GetHandleableItem(ISpotLight spotLight);
        public bool OnStandableObj();
    }
}