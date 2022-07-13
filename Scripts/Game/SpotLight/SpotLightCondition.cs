using System.Threading;
using Cysharp.Threading.Tasks;
using Game.DeviseInput;
using Game.Hero;
using Game.SpotLight.Struct;
using Game.Stage;
using UnityEngine;

namespace Game.SpotLight
{
    public class SpotLightCondition
    {
        private const float NotRotatingRange = 0.004f;

        private readonly Transform _transform;
        private readonly ISpotLight _spotLight;
        private readonly IStage _stage;
        
        private bool _prevOnActionButton = false;
        private bool _rotated;
        
        public SpotLightCondition(ISpotLight spotLight,IStage stage,Transform transform)
        {
            _spotLight = spotLight;
            _stage = stage;
            _transform = transform;
        }
        
        public void SetActiveLight()
        {
            if (_spotLight.isOnLight)
            {
                _spotLight.SetActiveModel(true);
                return;
            }

            _spotLight.SetActiveModel(false);
        }

        public void UpdateVelocityCondition(bool canYMove,float heroPosY,float spotLightToHero)
        {
            if (!_spotLight.isOnLight)
            {
                _spotLight.ForceVelocityX(0);
                return;
            }
            var stageMoveLimit = _stage.GetSpotLightMoveLimit();
            var moved=TryUpdateMoveCondition(stageMoveLimit,canYMove,heroPosY,spotLightToHero);

            UpdateRotateCondition(moved,stageMoveLimit);
        }
        
       　private bool TryUpdateMoveCondition(MoveLimitStruct stageMoveLimit,bool canYMove,float heroPosY,float spotLightToHero)
        {
            Vector3 moveVec = GetSpotLightHorizontalVec();
            if (canYMove)
            {
                moveVec +=new Vector3(0,heroPosY+spotLightToHero -_transform.position.y,0) ;
            }
            bool canMove = CanMoveByMoveLimit(moveVec,stageMoveLimit);
            
            if (canMove)
            {
                if (moveVec.magnitude > 1)
                {
                    moveVec = moveVec.normalized;
                }

                _spotLight.Move(moveVec);
                return true;
            }
            _spotLight.ForceVelocityX(0);
            return false;
        }

        private void UpdateRotateCondition(bool moved,MoveLimitStruct stageMoveLimit)
        {
            Vector3 moveVec = GetSpotLightHorizontalVec();
            bool canRotate = !moved && CanRotateByRotateLimit(moveVec,stageMoveLimit);

            //Debug.Log($"moved:{moved}");
            //Debug.Log($"canRotate:{canRotate}");
            if (!canRotate)
            {
                _spotLight.ForceAngularVelocityZ(0);
                return;
            }
            
            var rotVec = new Vector3(0, 0, moveVec.x);
            if (rotVec.magnitude > 1)
            {
                rotVec = rotVec.normalized;
            }

            _spotLight.Rotate(rotVec);
        }
        
        //TODO: ReactivePropertyでもいいかも
        public void SwitchActionLightColor()
        {
            if (!_spotLight.isOnLight)
            {
                return;
            }
            if (_prevOnActionButton == _spotLight.IsOnActionLight())
            {
                return;
            }
            _prevOnActionButton = _spotLight.IsOnActionLight();
            if (_prevOnActionButton)
            {
                _spotLight.SetActionLight(true);
                return;
            }

            _spotLight.SetActionLight(false);
        }
        
        
        private Vector3 GetSpotLightHorizontalVec()
        {
            return new Vector3(PlayerActionInput.lateralMovement,0,PlayerActionInput.depthMovement);
        }

        //TODO: Utilにまとめる
        private bool CanMoveByMoveLimit(Vector3 moveVec,MoveLimitStruct stageMoveLimit)
        {
            if (moveVec.x>0)
            {
                //MEMO:ステージ右端で右回転している時も左の移動制限内に入っているため必要
                bool isLeftRotating = -_transform.up.x < -NotRotatingRange;
                bool withinRightLimit = _transform.position.x < stageMoveLimit.right.pos;
                return !isLeftRotating && withinRightLimit;
            }
            if (moveVec.x<0)
            {
                //MEMO:ステージ左端で左回転している時も右の移動制限内に入っているため必要   
                bool isRightRotating = -_transform.up.x >NotRotatingRange;
                bool withinLeftLimit = _transform.position.x > stageMoveLimit.left.pos;
                return !isRightRotating && withinLeftLimit;
            }
            
            return true;
        }
        
        private bool CanRotateByRotateLimit(Vector3 moveVec,MoveLimitStruct stageMoveLimit)
        {
            if (moveVec.x>0)
            {
                bool withinRightLimit = Vector3.SignedAngle(Vector3.down, -_transform.up, Vector3.forward) <
                                        stageMoveLimit.right.rot;
                return withinRightLimit;
            }

            if (moveVec.x<0)
            {
                bool withinLeftLimit = Vector3.SignedAngle(Vector3.down, -_transform.up, Vector3.forward) >
                                       stageMoveLimit.left.rot;
                return withinLeftLimit;
            }
            return true;
        }
    }
}