using Game.Camera;
using Game.DeviseInput;
using Game.Hero;
using Game.SpotLight;
using Game.Stage;
using Game.UI.Interface;
using UnityEngine;
using UniRx;

namespace Game
{
    public class GameManager:MonoBehaviour
    {
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private GameObject spotLightPrefab;
        [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private LightMoveLimiter lightMoveLimiter;
        [SerializeField] private StageRange stageRange;
        [SerializeField] private Stage.Stage stage;
        [SerializeField] private RespawnAnimator respawnAnimator;
        

        //TODO: ゲームオブジェクト経由の取得に修正
        private readonly Vector3 heroInstancePos = new Vector3(-19.39f,0.035f,-3.84f);
        private readonly Vector3 lightInstancePos = new Vector3(-19.39f,2.14f,-3.84f);
        
        private IHero _hero;
        private ISpotLight _spotLight;
        private IHeroCamera _heroCamera;
        private IStage _stage;
        private IUIController _uiController;
        
        [SerializeField] TimeLineChecker timeLineChecker;
        [SerializeField]GameObject Opening;
    public void OpeningStart()
    {

         timeLineChecker.IsFinish.AsObservable().Subscribe(_=>{
            if(timeLineChecker.IsFinish.Value)
            {
                 GameStart();
                 Opening.SetActive(false);
            }
            
        }
            
        ).AddTo(this);

        timeLineChecker.PlayTimeLine();
    }
    


        private void GameStart()
        {
            new PlayerActionInput.PlayerActionSettings();

            //TODO: とりあえずここで生成（後で移動する）
            _hero = Instantiate(heroPrefab,heroInstancePos,Quaternion.identity).GetComponent<Hero.Hero>();
            _spotLight = Instantiate(spotLightPrefab,lightInstancePos,Quaternion.identity).GetComponent<SpotLight.SpotLight>();
            _heroCamera = Instantiate(cameraPrefab).GetComponent<HeroCamera>();
            
            //TODO: シングルトンに変更
            _stage = stage.GetComponent<IStage>();
            
            _uiController = new UIController(respawnAnimator);
            
            _spotLight.Init(_stage,_hero);
            _hero.Init(_spotLight,_stage,GameOver);
            _stage.Init(lightMoveLimiter,stageRange,_hero,_spotLight,_heroCamera);
            _heroCamera.Init(heroInstancePos,_hero);
            
            _hero.StartUpdateHero();
            _spotLight.StartUpdateSpotLight();
            _heroCamera.StartUpdateCamera();
            _stage.StartUpdate();
        }

        private void GameOver()
        {
            _uiController.StartFadein(Reset,ReStart);
        }
        
        private void Reset()
        {
            _stage.ResetStage();
            var restartPos=_stage.GetCheckPoint(_stage.currentStage);
            _hero.Reset(restartPos);
            _spotLight.Reset(restartPos);
            _heroCamera.Reset(_stage.currentStage);
        }

        private void ReStart()
        {
            _hero.ReStart();
            _spotLight.ReStart();
        }

        
    }
}