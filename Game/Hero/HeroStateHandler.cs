using System;
using Game.DebugClass;
using Game.Stage;
using UnityEngine;

namespace Game.Hero
{
    public class HeroStateHandler
    {
        private IHeroState _heroState;
        private StateConditions _stateConditions;
        private IHeroState _noneState;
        
        private HeroStateText _debugStateText;
#if UNITY_EDITOR
        public void InitHeroState(IHero hero,IStage stage,HeroStateText heroStateText)
        {
            _stateConditions = new StateConditions(stage);
            _noneState=_stateConditions.delegates.selectStateNone(hero);
            _heroState=_noneState;
            _debugStateText = heroStateText;
        }
#else
        public void InitHeroState(IHero hero,IStage stage)
        {
            _stateConditions = new StateConditions(stage);
            _noneState=_stateConditions.delegates.selectStateNone(hero);
            _heroState=_noneState;
        }
#endif
        
        public void HandleState(IHero hero,bool isPose)
        {
            //Debug.Log($"_heroState1:{_heroState}");
            IHeroState nextHeroState = ChooseNextHeroState(hero,isPose);
            if (!_heroState.Equals(nextHeroState))
            {
                _heroState.RefreshThisState(hero);
                _heroState = nextHeroState;
            }

#if UNITY_EDITOR
            _debugStateText.SetHeroStateText(_heroState?.GetType().ToString());
#endif
            _heroState.OnAction(hero);
            
        }

        private IHeroState ChooseNextHeroState(IHero hero,bool isPose)
        {
            if (isPose)
            {
                 return _noneState;
            }
            try
            {
                return _heroState.SelectNextStateByInput(hero);
            }
            catch (NullReferenceException e)
            {
                return _heroState.InitState(hero);
            }
        }
    }
}