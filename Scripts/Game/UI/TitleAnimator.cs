using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    [SerializeField] private CanvasGroup Mekakushi;
    [SerializeField] private Button StartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        Mekakushi.alpha = 0;
        Setup();
        //DOTween.To(value => { }, 0, 1, 1).SetUpdate(true);
    }
    private void Setup()
    {
        StartButton.OnClickAsObservable()
           .Subscribe(_ => GameIsGoingToStart())
           .AddTo(this);
    }
    private void GameIsGoingToStart()
    {
       
        Mekakushi.alpha = 1;
        //var sequence = DOTween.Sequence();
        //sequence.Append(Mekakushi.DOFade(1, 1));
        Mekakushi.DOFade(0, 2).SetUpdate(true);

        //sequence.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
