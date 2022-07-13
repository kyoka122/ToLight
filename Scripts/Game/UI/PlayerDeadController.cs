using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
/*
public class PlayerDeadController : MonoBehaviour
{

    //public PlayerDeadController = Instance;
    // Start is called before the first frame update
    [SerializeField] RespawnAnimator _respawnAnimator;
    public enum PlayerState
    {
        Live,
        Dead
    }
    public PlayerState NowPlayer;

    void Start()
    {
        // Instance = this;
        
         // NowPlayer = PlayerState.Live;
         // this.ObserveEveryValueChanged(x => x.NowPlayer)
         //         .Where(x=>x==PlayerState.Dead)
         //         .Subscribe(_ => _respawnAnimator.AnimeStarter())
         //         .AddTo(this);
        
        NowPlayer = PlayerState.Live;
        this.ObserveEveryValueChanged(x => x.NowPlayer)
                //.Where(x => x == PlayerState.Dead)
                .Subscribe(_ =>{
                    if (NowPlayer == PlayerState.Dead) {
                        _respawnAnimator.AnimeStarter();
                        }
                    if (NowPlayer == PlayerState.Live)
                    {
                        _respawnAnimator.AnimeFinisher();
                        }
                }

                )
                .AddTo(this);
    }
    
}
*/