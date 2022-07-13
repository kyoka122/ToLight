using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class TitleBGM : MonoBehaviour
{
    //GameObject gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void BGMplay()
    {
        if (!BGMManager.Instance.IsPlaying())
        {
            Debug.Log("BGM ON");
            BGMManager.Instance.Play(BGMPath.AMACHA_MIZUNOUEDEUTAU1);
        }
        

    }
    public void BGMend()
    {
        Debug.Log("BGM END");
        BGMManager.Instance.Stop(BGMPath.AMACHA_MIZUNOUEDEUTAU1);
    }
    public void BGMfade()
    {
        Debug.Log("BGM FADE");
        BGMManager.Instance.FadeOut(BGMPath.AMACHA_MIZUNOUEDEUTAU1, 1, () => {
            Debug.Log("BGMフェードアウト終了");
        });
    }
    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    /*
    private void OnMouseEnter()
    {
        Debug.Log("MouseEnter");
        SEManager.Instance.Play(SEPath.MAOU_ZIPPO);
    }
    */

    // Update is called once per frame

}
