using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Game.SpotLight
{
    public class SpotLightParameterSettings : MonoBehaviour
    {
        [Tooltip("ライトの移動速度"), Range(0, 0.3f)] [SerializeField]
        private float acceleration;
        public float Acceleration => acceleration;

        [Tooltip("ライトの移動減速度"), Range(0, 1)] [SerializeField]
        private float moveDecelerationRate;
        public float MoveDecelerationRate => moveDecelerationRate;

        [Tooltip("ライトの回転速度"), Range(0, 5f)] [SerializeField]
        private float rotSpeed;
        public float RotSpeed => rotSpeed;

        [Tooltip("ライトの回転減速度"), Range(0, 1)] [SerializeField]
        private float rotDecelerationRate;
        public float RotDecelerationRate => rotDecelerationRate;

        [Tooltip("ライトを強制移動させる時の移動速度"), Range(0,5f)] [SerializeField]
        private float forceMoveSpeed;

        public float ForceMoveSpeed => forceMoveSpeed;
        
        [SerializeField] private Color defaultLightColor;
        public Color DefaultLightColor => defaultLightColor;

        [SerializeField] private Color actionLightColor;
        public Color ActionLightColor => actionLightColor;
    }
}