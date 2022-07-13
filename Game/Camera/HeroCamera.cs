using System;
using Game.Hero;
using Game.SpotLight;
using Game.SpotLight.Struct;
using Game.Stage;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Camera
{
    public class HeroCamera:MonoBehaviour,IHeroCamera
    {
        [SerializeField] private Vector3 cameraToHeroPos;
        [SerializeField] private MoveLimitStructs cameraLimitStructs;
        
        [SerializeField,Range(0,5f)] 
        private float moveSpeed;
        [SerializeField,Range(0,3f)]
        private float rotSpeed;

        //private Rigidbody _rigidbody;
        private IHero _hero;
        private CameraAnimation _cameraAnimation;
        private Vector3 _prevHeroPos;
        private MoveLimitStruct _currentLimit;
        private StageEnum _currentStage = 0;//TODO: Stageから直接取得に変更

        private bool _moveYFlag;
        private bool _moveZFlag;
        
        public void Init(Vector3 heroInitPos,IHero hero)
        {
            _hero = hero;
            transform.position = new Vector3(_currentLimit.left.pos,_hero.pos.y+cameraToHeroPos.y,_hero.pos.z+cameraToHeroPos.z);
            //Debug.Log($"_hero.posY:{_hero.pos.y},_hero.posZ:{_hero.posZ}");
            //Debug.Log($"transform.position:{transform.position}");
            //Debug.Log($"_currentLimit.left.rot:{_currentLimit.left.rot}");
            transform.rotation = Quaternion.Euler(0, _currentLimit.left.rot, 0);
            //transform.rotation. = new Vector3(0, _currentLimit.left.rot, 0);
            //Debug.Log($"transform.rotation:{transform.rotation.eulerAngles}");
        }

        public void StartUpdateCamera()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    Move();
                });
        }
        
        public void MoveNextStage(StageEnum nextStage)
        {
            _currentStage = nextStage;
            _currentLimit = GetStageCameraLimitStructs(_currentStage);
        }

        public void Reset(StageEnum resetStage)
        {
            MoveNextStage(resetStage);
        }

        private void Move()
        {
            Vector3 newVec = _hero.pos - transform.position;
            //Debug.Log($"newVec:{newVec}");
            var moved=TryUpdateMoveCondition(newVec);
            UpdateRotateCondition(moved,newVec);
            _prevHeroPos = new Vector3(_hero.pos.x, _hero.pos.y, _hero.pos.z);
        }

        private Vector3 GetDestination()
        {
            return _currentStage switch
            {
                StageEnum.ClassRoom => GetDestinationMovePosX(),
                StageEnum.ClassToToilet => GetDestinationMovePosXZ(),
                StageEnum.Toilet => GetDestinationMovePosX(),
                StageEnum.ToiletToRoof => GetDestinationMovePosXYZ(),
                StageEnum.Roof => GetDestinationMovePosXYZ(),
                StageEnum.Clear => GetDestinationMovePosXYZ(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 GetDestinationMovePosX()
        {
            return new Vector3(_hero.pos.x , transform.position.y, transform.position.z);
        }
        
        private Vector3 GetDestinationMovePosXZ()
        {
            _moveZFlag = true;
            return new Vector3(_hero.pos.x, transform.position.y, _hero.pos.z + cameraToHeroPos.z);
        }
        
        private Vector3 GetDestinationMovePosXYZ()
        {
            _moveYFlag = true;
            _moveZFlag = true;
            return new Vector3(_hero.pos.x, _hero.pos.y, _hero.pos.z + cameraToHeroPos.z);
        }

        private MoveLimitStruct GetStageCameraLimitStructs(StageEnum stage)
        {
            return stage switch
            {
                StageEnum.ClassRoom => cameraLimitStructs.ClassRoom,
                StageEnum.ClassToToilet => cameraLimitStructs.ClassRoomToToilet,
                StageEnum.Toilet => cameraLimitStructs.Toilet,
                StageEnum.ToiletToRoof => cameraLimitStructs.ToiletToRoof,
                StageEnum.Roof => cameraLimitStructs.Roof,
                StageEnum.Clear => cameraLimitStructs.Roof,
                _ => throw new ArgumentOutOfRangeException(nameof(stage), stage, null)
            };
        }
        
        
        private bool TryUpdateMoveCondition(Vector3 newVec)
        {
            bool canMove = CanMoveByMoveLimit(newVec);
            //Debug.Log($"canMove:{canMove}");
            //Debug.Log($"GetDestination():{GetDestination()}");
            var newPos = Vector3.Lerp(transform.position, GetDestination(), Time.deltaTime*moveSpeed);
            
            if (canMove)
            {
                transform.position=newPos;
                return true;
            }
            
            if (_moveYFlag)
            {
                transform.position = new Vector3(transform.position.x, newPos.y, transform.position.z);
                _moveYFlag = false;
            }

            if (_moveZFlag)
            {
                transform.position=new Vector3(transform.position.x, transform.position.y, newPos.z);
                _moveZFlag = false;
            }
            return false;
        }
        
        private void UpdateRotateCondition(bool moved,Vector3 newVec)
        {
            bool canRotate = !moved && CanRotateByRotateLimit(newVec);

            //Debug.Log($"moved:{moved}");
            //Debug.Log($"canRotate:{canRotate}");
            if (canRotate)
            {
                //Debug.Log($"transform.position:{transform.position}");
                //Debug.Log($"rotVec:{rotVec.x}");
                transform.LookAt(new Vector3(_hero.pos.x,transform.position.y,_hero.pos.z));
            }
        }

        //TODO: 以下Utilにまとめる
        private bool CanMoveByMoveLimit(Vector3 newVec)
        {
            if (newVec.x>0)
            {
                //MEMO:ステージ右端で右回転している時も左の移動制限内に入っているため必要
                bool isLeftRotating = Mathf.Approximately(transform.forward.x,0);
                bool withinRightLimit = transform.position.x < _currentLimit.right.pos;
                //Debug.Log($"isLeftRotating:{isLeftRotating}, withinRightLimit:{withinRightLimit}");
                return !isLeftRotating && withinRightLimit;
            }
            if (newVec.x<0)
            {
                //MEMO:ステージ左端で左回転している時も右の移動制限内に入っているため必要   
                bool isRightRotating = Mathf.Approximately(transform.forward.x ,0);
                bool withinLeftLimit = transform.position.x > _currentLimit.left.pos;
                //Debug.Log($"transform.position.x:{transform.position.x},_currentLimit.left.pos:{_currentLimit.left.pos}");
                //Debug.Log($"isRightRotating:{isRightRotating}, withinLeftLimit:{withinLeftLimit}");
                return !isRightRotating && withinLeftLimit;
            }
            
            return false;
        }
        
        private bool CanRotateByRotateLimit(Vector3 moveVec)
        {
            if (moveVec.x>0)
            {
                bool withinRightLimit = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up) <
                                        _currentLimit.right.rot;
                //Debug.Log($"withinRightLimit:{withinRightLimit}");
                return withinRightLimit;
            }

            if (moveVec.x<0)
            {
                bool withinLeftLimit = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up) >
                                       _currentLimit.left.rot;
                //Debug.Log($"transform.forward:{transform.forward}");
                //Debug.Log($"Vector3.SignedAngle:{Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up)}");
                //Debug.Log($"withinLeftLimit:{withinLeftLimit}");
                return withinLeftLimit;
            }
            return false;
        }
    }
}