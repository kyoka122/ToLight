using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Game.SpotLight
{
    public class SpotLightRayCast:MonoBehaviour
    {
        [Range(0,0.5f)]
        [SerializeField] private float objectsRaycastRadius;
        [Range(0,2)]
        [SerializeField] private float inLightRaycastRadius;
        [Range(0,30)]
        [SerializeField] private float maxDistance;
        [Tooltip("光っている場所の判定と光の中にあるオブジェクトの判定に使用")]
        [SerializeField] private GameObject sphereCastOrigin;
        
        private Vector3 SphereCastOrigin=>sphereCastOrigin.transform.position;
        
        private RaycastHit[] _hitsInSpotLight=new RaycastHit[0];
        private RaycastHit[] _searchedRaycastHits=new RaycastHit[0];
        
        //TODO: 後でオブザーバーに変更/////////////////////
        private void FixedUpdate()
        {
            GetSearchObjectsRaycastHits();
            GetInSpotLightRaycastHits();
        }

        private void GetSearchObjectsRaycastHits()
        {
            //Debug.DrawRay(SphereCastOriginTransform.position, -SphereCastOriginTransform.up, Color.red, maxDistance);
            _searchedRaycastHits = Physics.SphereCastAll(SphereCastOrigin,objectsRaycastRadius, -transform.up,maxDistance,
                LayerInfo.lightRaycastLayerMask);
            /*foreach (var raycastHit in _hitsInSpotLight)
            {
                Debug.Log($"raycastHit:{raycastHit.collider.gameObject}");
            }*/
        }
        
        private void GetInSpotLightRaycastHits()
        {
            //Debug.DrawRay(SphereCastOriginTransform.position, -SphereCastOriginTransform.up, Color.red, maxDistance);
            _hitsInSpotLight = Physics.SphereCastAll(SphereCastOrigin,inLightRaycastRadius, -transform.up,maxDistance,
                LayerInfo.lightRaycastLayerMask);
        }
        
        /// <summary>
        /// 主人公が向かう場所を求める(ライトは端以外真っすぐ下を向いている前提)
        /// </summary>
        /// <returns></returns>
        public bool TryGetLightedBottom(out Vector3 lightedBottom)
        {
            foreach (var raycastHit in _hitsInSpotLight)
            {
                if ( raycastHit.collider.gameObject.layer==LayerInfo.StandableItem)
                {
                    lightedBottom= raycastHit.point;
                    return true;
                }
            }

            lightedBottom=Vector3.zero;
            return false;
        }

        public List<RaycastHit> SearchRaycastHitsInSpotLight()
        {
            var inSpotLightRaycasts=_searchedRaycastHits.
                ToList();
            return inSpotLightRaycasts;
        }

        public List<GameObject> SearchObjectsInSpotLight()
        {
            var inSpotLightObjects=_searchedRaycastHits.Select(x=>x.collider.gameObject)
                .ToList();
            return inSpotLightObjects;
        }

    }
}