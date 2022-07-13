using System;
using Game.SpotLight;
using Game.StageItem.Interface;
using UnityEngine;

namespace Game.Hero
{
    public class HeroCondition
    {
        //private readonly HeroInput _heroInput;
        private readonly IHeroRaycast _heroRaycast;
        private bool _prevActionButton=false;
        private float _moveFactor=10;
        
        public HeroCondition(IHeroRaycast heroRaycast)
        {
            _heroRaycast = heroRaycast;
            _heroRaycast.Init();
        }
        
        public bool IsInLight(float heroPosY,ISpotLight spotLight, float walkRange, Vector3 heroPivotOnFoot)
        {
            if (!spotLight.isOnLight)
            {
                return false;
            }
            Vector3 lightedPoint = GetMoveDestination(heroPosY,spotLight);
            bool inLight = Math.Abs(heroPivotOnFoot.x - lightedPoint.x) <= walkRange &&
                           Math.Abs(heroPivotOnFoot.z - lightedPoint.z) <= walkRange;
            
            return inLight;
        }

        public bool FacedJumpableHeightObj(ISpotLight spotLight,float heroPivotOnFoot)
        {
            var hadStandableItem = _heroRaycast.TryGetJumpableSpot(spotLight,out RaycastHit jumpableSpot);
            if (!hadStandableItem)
            {
                return false;
            }
            
            bool haveJumpDistance = jumpableSpot.point.y > heroPivotOnFoot;
            return haveJumpDistance;
        }
        
        public Vector3 GetMoveVec(float heroPosY,ISpotLight spotLight,Vector3 pos)
        {
            Vector3 moveVec = GetVecToDestination(heroPosY,spotLight, pos);
            
            if (moveVec.magnitude > 1)
            {
                moveVec.Normalize();
            }

            return moveVec * _moveFactor;
        }

        public bool OnStandableObj()
        {
            return _heroRaycast.OnStandableObj();
        }

        public bool InIdleRange(float heroPosY,ISpotLight spotLight, float idleRange, Vector3 heroPivotOnFoot)
        {
            Vector3 lightedPoint = GetMoveDestination(heroPosY,spotLight);
            bool inIdleRange = Math.Abs(heroPivotOnFoot.x - lightedPoint.x) <= idleRange &&
                               Math.Abs(heroPivotOnFoot.z - lightedPoint.z) <= idleRange;
            
            return inIdleRange;
        }
        
        public IHeroHandleItem GetHandleableItem(ISpotLight spotLight,IHeroHandleItem currentHandleableItem,IHeroHandleItem handlingItem)
        {
            var newHandleableItem = _heroRaycast.GetHandleableItem(spotLight);
            BrightenSelectedItem(currentHandleableItem,newHandleableItem,handlingItem);
            return newHandleableItem;
        }
        
        private Vector3 GetVecToDestination(float heroPosY,ISpotLight spotLight,Vector3 heroPos)
        {
            Vector3 lightedPoint = GetMoveDestination(heroPosY,spotLight);
            Vector3 destination = new Vector3(lightedPoint.x, heroPos.y, lightedPoint.z);
            Vector3 moveVec = destination - heroPos;
            return moveVec;
        }
        
        private Vector3 GetMoveDestination(float heroPosY,ISpotLight spotLight)
        {
            var gotLightedPoint = spotLight.TryGetLightedPoint(out var lightedPoint);
            if (!gotLightedPoint)
            {
                //Debug.Log($"Didn`t hit lay");
                var lightOrigin = spotLight.GetLightOriginTransform();
                
                float factor;
                try
                {
                    factor = (heroPosY - lightOrigin.position.y) / -lightOrigin.up.y;
                }
                catch (DivideByZeroException e)
                {
                    Debug.LogError($"LightDirectionY Is 0:{e}");
                    throw;
                }

                lightedPoint = lightOrigin.position + factor * -lightOrigin.up;
            }
            
            return lightedPoint;
        }
        
        private void BrightenSelectedItem(IHeroHandleItem currentHandleableItem,IHeroHandleItem newHandleableItem,IHeroHandleItem handlingItem)
        {
            //MEMO: handlingItemは既に光らせる処理を別の箇所で書いてあるため。
            if (handlingItem!=null)
            {
                return;
            }
            if (currentHandleableItem != newHandleableItem)
            {
                newHandleableItem?.OnSelectedLight();
                currentHandleableItem?.OffSelectedLight();
            }
        }
    }
}