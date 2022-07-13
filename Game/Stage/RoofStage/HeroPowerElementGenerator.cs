using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Hero;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.StageItem.Roof
{
    public class HeroPowerElementGenerator:MonoBehaviour
    {
        //MEMO: y座標はleftForwardInstanceRangeに従う
        [SerializeField] private Vector3 leftForwardInstanceRange;
        [SerializeField] private Vector3 rightBackInstanceRange;
        [SerializeField] private HeroPower heroPowerPrefab;

        [Range(0, 10f)] 
        [SerializeField] private float elementGenerateSpan;
        
        [Range(0,10f)]
        [SerializeField] private int maxElementNum;

        
        private Action<IHero> _heroManagerActions;
        
        private bool _isOnGenerator=false;
        private int _currentElementNum = 0;

        public void Init(Action<IHero> heroManagerActions)
        {
            _heroManagerActions = heroManagerActions;
            GenerateHeroPower();
        }

        public void OnGenerator(bool active)
        {
            _isOnGenerator = active;
        }

        public void DecreaseElementCount()
        {
            _currentElementNum--;
        }

        private void GenerateHeroPower()
        {
            float passedTimePrevGenerate=0;

            this.UpdateAsObservable()
                .Where(_ => _isOnGenerator)
                .Subscribe(_ =>
                {
                    passedTimePrevGenerate += Time.deltaTime;
                    var isGenerateTime = passedTimePrevGenerate > elementGenerateSpan;
                    var generatedMaxElement = maxElementNum <= _currentElementNum;

                    if (!isGenerateTime)
                    {
                        return;
                    }
                    passedTimePrevGenerate = 0;
                    
                    if (!generatedMaxElement)
                    {
                        var heroPower = Instantiate(heroPowerPrefab, GetInstancePos(), Quaternion.identity);
                        heroPower.Init(_heroManagerActions);
                        _currentElementNum++;
                    }
                });
        }
        
        
        
        

        private Vector3 GetInstancePos()
        {
            var randomX = Random.Range(leftForwardInstanceRange.x, rightBackInstanceRange.x);
            var randomZ = Random.Range(leftForwardInstanceRange.z, rightBackInstanceRange.z);
            return new Vector3(randomX,leftForwardInstanceRange.y,randomZ);
        }

    }
}