using System;
using Game.Camera;
using Game.Hero;
using Game.SpotLight;
using Game.SpotLight.Struct;
using Game.StageItem;
using Game.StageItem.Roof;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utility.CustomEditor;

namespace Game.Stage
{
    //TODO: 名前StageHandlerかStageManagerに変える？
    public class Stage : MonoBehaviour, IStage
    {
        [SerializeField] private HeroPowerElementGenerator heroPowerElementGenerator;
        [SerializeField,Tooltip("（x座標の値のみ判定に使用する）")] 
        private float battleStartPosX;

        [SerializeField,EnumIndex(typeof(StageEnum))]
        private CheckPoint[] checkPoint;
        
        public StageEnum currentStage { get; private set; } = StageEnum.ClassRoom;

        private MoveLimitStructs _lightMoveLimitStructs;
        private StageRange _stageRange;
        private IHero _hero;
        private ISpotLight _spotLight;
        private IHeroCamera _camera;
        private StageItemData _stageItemData;
        private HeroPowerElementManager _heroPowerManager;

        private bool _hadClearedRoofStage;
        private bool _hadAppearedBoss;

        public void Init(LightMoveLimiter lightMoveLimiter,StageRange stageRange, IHero hero, ISpotLight spotLight,IHeroCamera camera)
        {
            _lightMoveLimitStructs = lightMoveLimiter.LightLimitStructs;
            _stageRange = stageRange;
            _hero = hero;
            _spotLight = spotLight;
            _camera = camera;
            _stageItemData = FindObjectOfType<StageItemData>();
            _heroPowerManager= new HeroPowerElementManager(heroPowerElementGenerator);
            _camera.MoveNextStage(currentStage);
            _stageItemData.RoofBoss.Init(this,_hero);
        }

        public void StartUpdate()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    UpdateStageState();
                    OnStageEvent();
                })
                .AddTo(this);
        }
        
        public MoveLimitStruct GetSpotLightMoveLimit()
        {
            return currentStage switch
            {
                StageEnum.ClassRoom => _lightMoveLimitStructs.ClassRoom,
                StageEnum.ClassToToilet => _lightMoveLimitStructs.ClassRoomToToilet,
                StageEnum.Toilet => _lightMoveLimitStructs.Toilet,
                StageEnum.ToiletToRoof => _lightMoveLimitStructs.ToiletToRoof,
                StageEnum.Roof => _lightMoveLimitStructs.Roof,
                StageEnum.Clear => _lightMoveLimitStructs.Clear,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void ClearRoofStage()
        {
            _hadClearedRoofStage = true;
        }

        public Vector3 GetCheckPoint(StageEnum stageEnum)
        {
            return checkPoint[(int) stageEnum].pos;
        }

        private void UpdateStageState()
        {
            if (CanUpdateState())
            {
                currentStage++;
                _spotLight.UpdateNextStage();
                _camera.MoveNextStage(currentStage);
            }
        }
        
        public void ResetStage()
        {
            if (currentStage==StageEnum.Roof)
            {
                _heroPowerManager.ActiveGenerator(false);
                _hadAppearedBoss = false;
            }
        }

        private bool CanUpdateState()
        {
            return currentStage switch
            {
                StageEnum.ClassRoom => HadMovedNextStage(),
                StageEnum.ClassToToilet => HadMovedNextStage(),
                StageEnum.Toilet => HadMovedNextStage(),
                StageEnum.ToiletToRoof => HadMovedNextStage(),
                StageEnum.Roof => _hadClearedRoofStage,
                StageEnum.Clear => false,
                _ => throw new ArgumentOutOfRangeException()
            };
            
        }

        private void OnStageEvent()
        {
            //MEMO: 数が少ないため、if文で
            bool canStartBattle = battleStartPosX > _hero.pos.x;
            if (currentStage==StageEnum.Roof&&!_hadAppearedBoss&&canStartBattle)
            {
                StartRoofStage();
                _hadAppearedBoss = false;
            }
        }

        private bool HadMovedNextStage()
        {
            if (_hero.pos.x>GetStageRightLimit((int)currentStage))
            {
                return true;
            }

            return false;
        }
        
        private float GetStageRightLimit(int stageNum)
        {
            return _stageRange.Stage[stageNum].RightLimit;
        }
        
        private void StartRoofStage()
        {
            _heroPowerManager.ActiveGenerator(true);
            _stageItemData.RoofBoss.StartBattle();
        }
    }
}