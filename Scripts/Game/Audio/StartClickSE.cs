using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;
using UniRx;

public class StartClickSE : MonoBehaviour, IPointerEnterHandler
{
    //GameObject gameObject;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.OnClickAsObservable()
           .Subscribe(_ => OnClick())
           .AddTo(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseEnter");
        SEManager.Instance.Play(SEPath.MAOU_ZIPPO);
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    /*
    private void OnMouseEnter()
    {
        Debug.Log("MouseEnter");
        SEManager.Instance.Play(SEPath.MAOU_ZIPPO);
    }
    */
    private void OnClick()
    {
        Debug.Log("Click");
        SEManager.Instance.Play(SEPath.MAOU_SYSTEM48);
    }
    // Update is called once per frame

}
