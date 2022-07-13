using Game.Hero.Searcher;
using Game.SpotLight;
using Game.StageItem.Interface;
using UnityEngine;
using Utility;

namespace Game.Hero
{
    public class HeroRaycast:MonoBehaviour,IHeroRaycast
    {
        private ItemSearcher _itemSearcher;
        private SearchParameterSettings _searchParameterSettings;

        private SearchParameter _verticalParam;
        private SearchParameter _horizontalParam;

        private RaycastHit[] _horizontalRaycastHits=new RaycastHit[0];
        private bool _onStandanleObj;
        private bool hadInit;
        
        public void Init()
        {
            _itemSearcher = new ItemSearcher();
            _searchParameterSettings = GetComponent<SearchParameterSettings>();
            SetSearchParameter();
            hadInit = true;
        }

        //TODO: 後でオブザーバーに変更
        private void FixedUpdate()
        {
            if (hadInit)
            {
                SetSearchParameter();
                SetFacedObjects();
                SetOnStandableObj();
            }
        }
        
        private void SetSearchParameter()
        {
            _verticalParam = _searchParameterSettings.GetVerticalSearchParameter();
            _horizontalParam = _searchParameterSettings.GetHorizontalSearchParameter();
        }

        private void SetFacedObjects()
        {
            _horizontalRaycastHits = Physics.SphereCastAll(_horizontalParam.OriginPos, _horizontalParam.raycastRadius,
                _horizontalParam.OriginTransform.forward, _horizontalParam.maxDistance, LayerInfo.heroRaycastLayerMask);
            /*foreach (var hit in _horizontalRaycastHits)
            {
                if (hit.collider.gameObject.name=="yuka")
                {
                    Debug.Log($"Yuka");
                }
                
            }*/
        }

        private RaycastHit verticalRaycastHit;
        private void SetOnStandableObj()
        {
            //Debug.DrawRay(SphereCastOriginTransform.position, -SphereCastOriginTransform.up, Color.red, maxDistance);
            _onStandanleObj = Physics.SphereCast(_verticalParam.OriginPos, _verticalParam.raycastRadius,
                -_verticalParam.OriginTransform.up,out verticalRaycastHit, _verticalParam.maxDistance,
                LayerInfo.standableItemLayerMask);

            //Debug.Log($"standableHit{verticalRaycastHit}");

        }
        
        public bool TryGetJumpableSpot(ISpotLight spotLight,out RaycastHit jumpableSpot)
        {
            return FacedLayerSpot.HadFacedLayerSpot(spotLight, _horizontalRaycastHits,out jumpableSpot,LayerInfo.StandableItem);
        }
        
        public bool TryGetCanGoingUpStairsSpot(ISpotLight spotLight,out RaycastHit canGoingUpStairsSpot)
        {
            foreach (var a in _horizontalRaycastHits)
            {
                Debug.Log($"_horizontalRaycastHits:{a}");
            }
            return FacedLayerSpot.HadFacedLayerSpot(spotLight, _horizontalRaycastHits,out canGoingUpStairsSpot,LayerInfo.Stairs);
        }

        public IHeroHandleItem GetHandleableItem(ISpotLight spotLight)
        {
            return _itemSearcher.GetHandleableItem(gameObject.transform.position,spotLight,_horizontalRaycastHits);
        }

        public bool OnStandableObj()
        {
            return _onStandanleObj;
        }

    }
}