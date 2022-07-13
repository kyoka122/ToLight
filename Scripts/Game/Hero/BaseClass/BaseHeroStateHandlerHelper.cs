using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Hero
{
    public abstract class BaseHeroStateHandlerHelper
    {
        protected List<Func<IHero, IHeroState>> _delegate;
        protected Func<IHero, IHeroState> _noneState;

        protected BaseHeroStateHandlerHelper(in StateConditions.Delegates heroBehaviourDelegates)
        {
            _delegate = new List<Func<IHero, IHeroState>>();
            _noneState = heroBehaviourDelegates.selectStateNone;
            RegisterDelegateInOrder(heroBehaviourDelegates);
        }

        public IHeroState InitState(IHero hero)
        {
            return _noneState(hero);
        }

        protected abstract void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates);
        //public abstract void OnAction(hero hero);
        //public abstract void RefreshThisState();
        
        public virtual IHeroState SelectNextStateByInput(IHero hero)
        {
            foreach (var func in _delegate)
            {
                IHeroState heroState = func(hero);
                if (heroState != null)
                {
                    return heroState;
                }
            }
            
            return null;
        }
        
    }
}