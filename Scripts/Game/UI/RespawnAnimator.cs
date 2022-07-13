using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Game.UI.Interface;

public class RespawnAnimator : MonoBehaviour,IUIController
{
    [SerializeField]private Material _transitionOut;
    [SerializeField] private Material _transitionIn;
    [SerializeField] private CanvasGroup _black;

    private Action _actionThenFadeIn;
    private Action _actionThenFadeOut;
   // [SerializeField] private Material _transitionOut;
    void Start()
    {
        _transitionOut.SetFloat("_Alpha", 0);
        _black.alpha = 0;
    //    _transitionOut.SetFloat("_Alpha", 0);
        //StartCoroutine(BeginTransition());
    }
    public void StartFadein(Action reset,Action restart)
    {
        _actionThenFadeIn = reset;
        _actionThenFadeOut = restart;
        StartCoroutine(BeginTransition());
    }
    IEnumerator BeginTransition()
    {
        yield return Animate(_transitionOut, 2);
        _black.alpha = 255;

        _actionThenFadeIn.Invoke();
        FadeoutFinisher();
    }
    public void FadeoutFinisher()
    {
        StartCoroutine(EndTransition());
    }
    IEnumerator EndTransition()
    {
        _black.alpha = 0;
        //_black.DOFade(0, 2);
        //yield return null;
        yield return Animate(_transitionIn, 2);
        _actionThenFadeOut.Invoke();
    }
    
    /// <summary>
    /// time秒かけてトランジションを行う
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Animate(Material material, float time)
    {
        
        //GetComponent<Image>().material = material;
        
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        //_black.alpha = 255;
        if(material==_transitionOut)material.SetFloat("_Alpha", 0);
        
        
    }
    
}