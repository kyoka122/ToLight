using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Hero;
using Game.Stage;
using Game.StageItem.Roof.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utility.CustomEditor;

namespace Game.StageItem.Roof
{
    public class Boss : MonoBehaviour,IBoss
    {
        [SerializeField] private GameObject model;
        
        [SerializeField,Range(0,1f)]
        private float decelerationRate;

        [SerializeField,Range(0,10f)]
        private float maxDistanceToHero;
        
        [SerializeField,Range(0,1f)]
        private float getCloserSpeedToHero;
        
        [SerializeField,Range(-1,0f)]
        private float leaveSpeedFromHero;
        
        [SerializeField,Range(-1,0f)]
        private float leaveDistanceWhenDamaged;
        
        [SerializeField, EnumIndex(typeof(BossStateEnum))]
        private BossStateParameter[] stateParam;

        private const float BossExitedJudgeRange = 0.01f;
        private const float RoundSpan = 7f;
        
        private IStage _stage;
        private IHero _hero;
        private BossCondition _bossCondition;
        private BossStateEnum _state=BossStateEnum.First;
        private Rigidbody _rigidbody;
        
        private float _maxHp;
        private float _currentHp;
        private float _currentTargetDistanceToHero;
        private bool _canConditionUpdate;
        private bool _defeatFlag;
        private bool _damagedFlag;

        public Vector3 position => transform.position;

        public void Init(IStage stage,IHero hero)
        {
            _stage = stage;
            _hero = hero;
            _rigidbody = GetComponent<Rigidbody>();
            _bossCondition = new BossCondition(this);

            ResetParameter();
            
            this.FixedUpdateAsObservable()
                .Where(_=>_canConditionUpdate)
                .Subscribe(_ =>
                {
                    UpdateCondition();
                });
        }

        public void StartBattle()
        {
            model.SetActive(true);
            _canConditionUpdate = true;
        }

        private async void UpdateCondition()
        {
            if (_damagedFlag)
            {
                _currentTargetDistanceToHero = Mathf.Min(_currentTargetDistanceToHero + leaveDistanceWhenDamaged,
                    maxDistanceToHero);
                _damagedFlag =
                    (stateParam[(int) _state].movePos.exitEnd - transform.position).magnitude >
                    BossExitedJudgeRange;
                if (!_damagedFlag)
                {
                    _state++;
                    transform.position = stateParam[(int) _state].movePos.entryStart;
                    CancellationToken token = this.GetCancellationTokenOnDestroy();
                    await UniTask.Delay(TimeSpan.FromSeconds(RoundSpan), cancellationToken: token);
                }
            }
            else
            {
                _currentTargetDistanceToHero = _bossCondition.GetTargetDistanceToHero(maxDistanceToHero,
                    _currentTargetDistanceToHero, leaveSpeedFromHero);
            }
            
            _bossCondition.TryMove(_damagedFlag,_hero);
            _bossCondition.TryUpdateNextState(_state,_currentHp,stateParam[(int)_state].hp);
        }
        
        public void GetCloser(Vector3 velocity)
        {
            LookAtDirection(velocity);
            _rigidbody.velocity += velocity* getCloserSpeedToHero;
            _rigidbody.velocity *= decelerationRate;
        }

        public void LeaveStage()
        {
            var newVel = Vector3.Lerp(transform.position, stateParam[(int) _state].movePos.exitEnd,
                Time.deltaTime * leaveSpeedFromHero);
            
            LookAtDirection(newVel);
            _rigidbody.velocity += newVel* leaveSpeedFromHero;
            _rigidbody.velocity *= decelerationRate;
        }
        
        public void UpdateState(BossStateEnum newState)
        {
            _state = newState;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void AddDamage(float damage)
        {
            _currentHp -= damage;
            _damagedFlag = true;
        }

        public void ResetParameter()
        {
            _maxHp = stateParam.Select(x => x.hp).Sum();
            _currentHp = _maxHp;
            gameObject.transform.position = stateParam[(int) BossStateEnum.First].movePos.entryStart;
            model.SetActive(false);
            UpdateState(BossStateEnum.First);
        }

        public void Defeated()
        {
            _canConditionUpdate = false;
            _stage.ClearRoofStage();
        }
        
        private void LookAtDirection(Vector3 lookAtVec)
        {
            var lookAtRotation = Quaternion.LookRotation(lookAtVec);
            transform.rotation = lookAtRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_defeatFlag && other.gameObject.TryGetComponent(out IHero hero))
            {
                _defeatFlag = true;
                hero.Defeat();
                _canConditionUpdate = false;
            }
        }
    }
        
}
