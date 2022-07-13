using UnityEngine;

namespace Game.SpotLight
{
    public class LightMoveLimiter:MonoBehaviour
    {
        [SerializeField] private MoveLimitStructs lightLimitStructs;
        public MoveLimitStructs LightLimitStructs => lightLimitStructs;
    }
}