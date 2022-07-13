using UnityEngine;

namespace Game.Hero
{
    public class SearchParameterSettings:MonoBehaviour
    {
        //TODO: Parameterにまとめる
        [Header("垂直方向のRay設定"),Range(0,0.3f)]
        [SerializeField] private float vRaycastRadius;
        [Range(0,3)]
        [SerializeField] private float vMaxDistance;
        [SerializeField] private GameObject vSphereCastOrigin;

        [Header("水平方向のRay設定"),Range(0,0.5f)]
        [SerializeField] private float hRaycastRadius;
        [Range(0,0.5f)]
        [SerializeField] private float hMaxDistance;
        [SerializeField] private GameObject hSphereCastOrigin;
        
        public SearchParameter GetVerticalSearchParameter()
        {
            return new SearchParameter(vRaycastRadius, vMaxDistance, vSphereCastOrigin);
        }
        
        public SearchParameter GetHorizontalSearchParameter()
        {
            return new SearchParameter(hRaycastRadius, hMaxDistance, hSphereCastOrigin);
        }

    }

    public class SearchParameter
    {
        public readonly float raycastRadius;
        public readonly float maxDistance;
        private readonly GameObject _sphereCastOrigin;
        public Transform OriginTransform=>_sphereCastOrigin.transform;
        public Vector3 OriginPos=>OriginTransform.position;

        public SearchParameter(in float raycastRadius,in float maxDistance,in GameObject sphereCastOrigin)
        {
            this.raycastRadius = raycastRadius;
            this.maxDistance = maxDistance;
            _sphereCastOrigin = sphereCastOrigin;
        }
    }
}