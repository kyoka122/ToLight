using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;




public class LookAtMenu : MonoBehaviour
{
    [SerializeField] private UIManager uimanager;

    public static LookAtMenu Instance;
    
    // Start is called before the first frame update
    void Start()
    {
         var observable = Observable.EveryUpdate()
          .Select(_ => Input.inputString)
          .Where(_ => Input.GetKeyDown(KeyCode.Q))
          .Subscribe(_ => uimanager.MenuPisActiveWithGoBack())
          .AddTo(this);
       

        
    }
}
