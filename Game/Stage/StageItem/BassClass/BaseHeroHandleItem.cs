using Game.StageItem.Interface;
using UnityEngine;
using Utility;

namespace Game.StageItem.BassClass
{
    public abstract class BaseHeroHandleItem:MonoBehaviour,IHeroHandleItem
    {
        [SerializeField] private float weight;
        [SerializeField] private Joint joint;
        [SerializeField] private Renderer lightingTarget;
        
        public abstract HeroHandleItemEnum ItemEnum { get; }
        protected abstract float LightingIntensity { get; }
        public float Weight=>weight;

        [SerializeField] private Transform heroRightHandSetPoint;
        public Transform HeroRightHandSetPoint => heroRightHandSetPoint;

        
        [SerializeField] private Transform heroLeftHandSetPoint;
        public Transform HeroLeftHandSetPoint => heroLeftHandSetPoint;
        

        public void FreezePosition()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void FreeFreezingPosition()
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        public void RemoveJoint()
        {
            Destroy(joint);
        }

        public void OnSelectedLight()
        {
            EmissionChanger.ChangeEmissionColor(lightingTarget,LightingIntensity);
        }

        public void OffSelectedLight()
        {
            EmissionChanger.ChangeEmissionColor(lightingTarget,0);
        }

        public Joint GetJoint()
        {
            if (joint==null)
            {
                return null;
            }
            return joint;
        }
    }
}