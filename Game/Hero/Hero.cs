using System;
using Game.DebugClass;
using Game.Hero.Searcher;
using Game.SpotLight;
using Game.Stage;
using Game.StageItem;
using Game.StageItem.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Hero
{
    //TODO：　パラメーターは公開すべきだったかも
    public class Hero:MonoBehaviour,IHero
    {
        [SerializeField] private GameObject heroPivotOnFootObj;
        private Vector3 HeroPivotOnFoot => heroPivotOnFootObj.transform.position;
        
        private ItemSearcher _itemSearcher;
        private StandableSpotSearcher _standableSpotSearcher;
        private Rigidbody _rigidbody;
        private HeroStateHandler _heroStateHandler;
        private HeroCondition _heroCondition;
        private ISpotLight _spotLight;
        private StageItemData _stageItemData;
        private HeroAnimation _heroAnimation;
        private HeroParameterSettings _heroParam;
        private HeroStateText _heroStateText;

        private Action _gameOver;
        private Vector3 _moveVec;
        private int _battlePowerPoint;
        private bool _isPose=false;

        public void Init(ISpotLight spotLight,IStage stage,Action gameOver)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _heroStateHandler = new HeroStateHandler();
            var heroRaycast = GetComponent<IHeroRaycast>();
            _heroCondition = new HeroCondition(heroRaycast);
            var animator = GetComponent<Animator>();
            _heroAnimation = GetComponent<HeroAnimation>();
            _heroParam = GetComponent<HeroParameterSettings>();
            _stageItemData = FindObjectOfType<StageItemData>();
            _heroAnimation.Init(animator);
            _spotLight = spotLight;
            _gameOver = gameOver;
#if UNITY_EDITOR
            _heroStateText = FindObjectOfType<HeroStateText>();
            _heroStateHandler.InitHeroState(this,stage,_heroStateText);
#else
            _heroStateHandler.InitHeroState(this,stage);
#endif
            UpdateHeroCondition();
        }

        public void StartUpdateHero()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    UpdateCondition();
                });
        }

        private void UpdateCondition()
        {
            UpdateHeroCondition();
            _heroStateHandler.HandleState(this, _isPose);
            ProvideGravity();
        }
        
        
        //TODO: Vector3にまとめる
        //public float posX { get; private set; }
        //public float posY { get; private set; }
        //public float posZ { get; private set; }
        public Vector3 pos { get; private set; }

        /// <summary>
        /// 接地判定
        /// </summary>
        public bool onStandable { get; private set; }
        public bool isInLight { get; private set; }
        public bool inIdleRange { get; private set; }
        public bool facedHavingJumpableHeightObj { get; private set; }//TODO: ココに書きたくない。けど、ここに書かないとパラメータが増える
        //public bool facedUpStairsObj { get; private set; }//TODO: ココに書きたくない。けど、ここに書かないとパラメータが増える
        public IHeroHandleItem handleableItem { get; private set; }
        public IHeroHandleItem handlingItem { get; private set; }
        public bool isOnLight { get; private set; }
        public bool isOnMoveLight { get;private set; }
        public bool isOnActionLight { get; private set; }
        public float maxJumpHeightValue { get; private set; }
        public float jumpStartPosY { get; private set; }

        /// <summary>
        /// 全公開変数を更新
        /// </summary>
        private void UpdateHeroCondition()
        {
            //posX = pos.x;
            //posY = pos.y;
            //posZ = pos.z;
            pos = transform.position;
            _moveVec = _heroCondition.GetMoveVec(pos.y,_spotLight,transform.position);
            onStandable = _heroCondition.OnStandableObj();
            isInLight = _heroCondition.IsInLight(pos.y,_spotLight,_heroParam.WalkRange,HeroPivotOnFoot);
            inIdleRange = _heroCondition.InIdleRange(pos.y,_spotLight, _heroParam.IdleRange, HeroPivotOnFoot);
            facedHavingJumpableHeightObj =
                _heroCondition.FacedJumpableHeightObj(_spotLight, HeroPivotOnFoot.y);
            handleableItem = _heroCondition.GetHandleableItem(_spotLight,handleableItem,handlingItem);
            isOnLight = _spotLight.isOnLight;
            isOnMoveLight = _spotLight.IsOnMoveLight();
            isOnActionLight = _spotLight.IsOnActionLight();
            maxJumpHeightValue = _heroParam.MaxJumpHeight;
        }
        
        public void Walk()
        {
            LookAtDirection(_moveVec);
            _rigidbody.velocity += _moveVec * _heroParam.WalkAcceleration;
            _rigidbody.velocity *= _heroParam.DecelerationRate;
        }

        //MEMO: BackWalkは椅子を引っ張る時のみのため、Physicsの回転を使用
        public void BackWalk()
        {
            PhysicsLookAtDirection(_moveVec);
            _rigidbody.velocity += _moveVec * _heroParam.WalkAcceleration;
            _rigidbody.velocity *= _heroParam.DecelerationRate;
        }

        //MEMO: SideWalkは扉を開ける時のみのため、直接SideWalkAccelerationを使用
        public void SideWalk()
        {
            _rigidbody.velocity += _moveVec * _heroParam.SideWalkAcceleration;
            _rigidbody.velocity *= _heroParam.DecelerationRate;
        }

        public void Walk(float walkSpeed)
        {
            LookAtDirection(_moveVec);
            _rigidbody.velocity += _moveVec * walkSpeed;
            _rigidbody.velocity *= _heroParam.DecelerationRate;
        }
        public void Run()
        {
            LookAtDirection(_moveVec);
            _rigidbody.velocity += _moveVec * _heroParam.RunAcceleration;
            _rigidbody.velocity *= _heroParam.DecelerationRate;
        }
        
        public void Stay()
        {
            //LookAtDirection();
            _rigidbody.velocity = Vector3.zero;
        }

        public void OnJump()
        {
            LookAtDirection(_moveVec);
            //TODO: ↓の変数を無くす
            jumpStartPosY = pos.y;
            _rigidbody.AddForce(Vector3.up * _heroParam.FirstJumpAcceleration, ForceMode.Impulse);
        }
        
        public void Jump()
        {
            LookAtDirection(_moveVec);
            _rigidbody.AddForce(Vector3.up * _heroParam.JumpAcceleration, ForceMode.Impulse);
        }

        public void JumpExcess()
        {
            LookAtDirection(_moveVec);
            _rigidbody.AddForce(Vector3.up * _heroParam.JumpExcessAcceleration, ForceMode.Impulse);
        }

        public void LookAtBoss()
        {
            transform.LookAt(_stageItemData.RoofBoss.GetPosition());
        }

        public void Defeat()
        {
            _isPose = true;
            Debug.Log($"Defeat!!!!!!!!!!!");
            _gameOver.Invoke();
        }

        public void Animate(string animString)
        {
            _heroAnimation.TriggerAnimation(animString);
        }
        
        public void Animate(string animString,Transform rightHandSetPoint,Transform leftHandSetPoint)
        {
            _heroAnimation.HandleAnimation(animString,rightHandSetPoint,leftHandSetPoint);
        }

        private void LookAtDirection(Vector3 lookAtVec)
        {
            var lookAtRotation = Quaternion.LookRotation(lookAtVec);
            transform.rotation = lookAtRotation;
        }
        
        //TODO: HeroConditionに移す
        //MEMO: 物理で操作すると椅子（transform）が変わらない
        private void PhysicsLookAtDirection(Vector3 vecToDestination)
        {
            var angle=Vector3.SignedAngle( transform.forward, -vecToDestination, Vector3.up);
            var rotateAngle = _heroParam.RotAngleWhilePullingChair;
            if (angle<0)
            {
                rotateAngle = -rotateAngle;
            }
            
            _rigidbody.angularVelocity += Vector3.up * rotateAngle;
        }

        public void SetHandlingItem(IHeroHandleItem item)
        {
            //Debug.Log($"SetAsHandlingItem:{item}");
            handlingItem = item;
        }
        
        public void SetItemJoint(IHeroHandleItem item)
        {
            item.GetJoint().connectedBody = _rigidbody;
            //handlingItem = item;
            //item.ThisObject.transform.SetParent(gameObject.transform);
        }
        
        public void ReleaseItemJoint(IHeroHandleItem item)
        {
            if (item.GetJoint()!=null)
            {
                item.GetJoint().connectedBody = null;
                //item.ThisObject.transform.SetParent(null);
            }
        }

        public void Pose(bool on)
        {
            _isPose = on;
        }
        
        public int GetBattlePowerPoint()
        {
            return _battlePowerPoint;
        }

        public void SetBattlePower(int addPoint)
        {
            _battlePowerPoint += addPoint;
        }

        public void AttackBoss()
        {
            _stageItemData.RoofBoss.AddDamage(_heroParam.Attack);
        }

        private void ProvideGravity()
        {
            if (_rigidbody.velocity.y<0)
            {
                _rigidbody.AddForce(Vector3.down * _heroParam.JumpingGravity, ForceMode.Impulse);
            }
        }

        public void Reset(Vector3 newPos)
        {
            _battlePowerPoint = 0;
            transform.position = newPos;
        }

        public void ReStart()
        {
            _isPose = false;
        }
    }
}