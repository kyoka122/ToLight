using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.DeviseInput;
using Game.Hero;
using Game.Stage;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.SpotLight
{
    public class SpotLight : MonoBehaviour, ISpotLight
    {
        [SerializeField] private Light lightModel;
        //[SerializeField] private GameObject lightModel;
        [SerializeField] private Light centerLightModel;

        private SpotLightRayCast _spotLightRayCast; 
        private Rigidbody _rigidbody;
        private SpotLightCondition _spotLightCondition;
        private SpotLightParameterSettings _spotLightParam;
        private IStage _stage;
        private IHero _hero;
        
        public bool isOnLight => PlayerActionInput.activeLight;
        
        private bool _canUpdateLightVelocity=true;
        private bool _isPose=false;
        private bool _isOnYMove;
        private float _spotLightToHero;

        public void Init(IStage stage,IHero hero)
        {
            _spotLightRayCast = GetComponent<SpotLightRayCast>();
            _rigidbody = GetComponent<Rigidbody>();
            _spotLightCondition = new SpotLightCondition(this,stage,transform);
            _spotLightParam = GetComponent<SpotLightParameterSettings>();
            _stage = stage;
            _hero = hero;
            lightModel.color = _spotLightParam.DefaultLightColor;
            centerLightModel.color = _spotLightParam.DefaultLightColor;
            _spotLightToHero=transform.position.y - hero.pos.y;
            Debug.Log($"_spotLightToHero:{_spotLightToHero}");
        }

        public void StartUpdateSpotLight()
        {
            this.FixedUpdateAsObservable()
                .Where(x=>!_isPose)
                .Subscribe(_ =>
                {
                    UpdateCondition();
                });
        }
        
        private void UpdateCondition()
        {
            _spotLightCondition.SetActiveLight();
            _spotLightCondition.SwitchActionLightColor();
            if (_canUpdateLightVelocity)
            {
                _spotLightCondition.UpdateVelocityCondition(_isOnYMove,_hero.pos.y,_spotLightToHero);
            }
        }

        public void SetLightSize()
        {
            //TODO: LightとRayのサイズを調整
        }
        
        public void Move(Vector3 moveVec)
        { 
            _rigidbody.velocity += moveVec * _spotLightParam.Acceleration;
            _rigidbody.velocity *= _spotLightParam.MoveDecelerationRate;
            //Debug.Log($"SpotLightMove{moveVec * acceleration}");
        }

        public void ForcePosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        public void Rotate(Vector3 moveVec)
        {
            _rigidbody.angularVelocity += _spotLightParam.RotSpeed*moveVec;
            _rigidbody.angularVelocity *= _spotLightParam.RotDecelerationRate;
        }

        public void ForceRotation(Quaternion newRot)
        {
            transform.rotation = newRot;
        }

        public void ForceVelocityX(float forcedVelocityX)
        {
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(forcedVelocityX, velocity.y, velocity.z);
        }

        public void ForceAngularVelocityZ(float forcedVelocityZ)
        {
            var velocity = _rigidbody.angularVelocity;
            _rigidbody.angularVelocity = new Vector3(velocity.x, velocity.y, forcedVelocityZ);
        }
        
        public void SetActiveModel(bool active)
        {
            lightModel.gameObject.SetActive(active);
            centerLightModel.gameObject.SetActive(active);
        }

        public void SetActionLight(bool isAction)
        {
            if (isAction)
            {
                lightModel.color = _spotLightParam.ActionLightColor;
                centerLightModel.color = _spotLightParam.ActionLightColor;
                return;
            }
            lightModel.color = _spotLightParam.DefaultLightColor;
            centerLightModel.color = _spotLightParam.DefaultLightColor;
        }
        
        public bool IsOnActionLight()
        {
            return PlayerActionInput.onActionButton && isOnLight;
        }
        
        public bool IsOnMoveLight()
        {
            return !PlayerActionInput.onActionButton && isOnLight;
        }
        
        public List<RaycastHit> SearchInSpotLight()
        {
            return _spotLightRayCast.SearchRaycastHitsInSpotLight();
        }

        /// <summary>
        /// ライトが当たっている面(移動可能範囲にて)の中心座標。見つからない場合はVector3.zeroを返す。
        /// </summary>
        /// <returns></returns>
        public bool TryGetLightedPoint(out Vector3 lightedPoint)
        {
            return _spotLightRayCast.TryGetLightedBottom(out lightedPoint);
        }

        public Transform GetLightOriginTransform()
        {
            return gameObject.transform;
        }

        public List<GameObject> GetObjectsInSpotLight()
        {
            return _spotLightRayCast.SearchObjectsInSpotLight();
        }

        public void UpdateNextStage()
        {
            _isOnYMove = _stage.currentStage==StageEnum.ToiletToRoof;
            ForceMoveToNewStage();
        }
        
        private async void ForceMoveToNewStage()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _canUpdateLightVelocity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity=Vector3.zero;
            _hero.Pose(true);
            var spotLightAnim=new SpotLightAnimation(_hero,this,transform,_spotLightParam.ForceMoveSpeed);
            await spotLightAnim.ForceMoveNewStageTask(token);
            _hero.Pose(false);
            _canUpdateLightVelocity = true;
        }

        public void Reset(Vector3 restartPos)
        {
            _isPose = true;
            transform.position = new Vector3(restartPos.x, restartPos.y+_spotLightToHero, restartPos.z);
        }

        public void ReStart()
        {
            _isPose = false;
        }
        
    }
}