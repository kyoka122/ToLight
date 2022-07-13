using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSet : MonoBehaviour
{
    [SerializeField] Image _menupanel;
    [SerializeField] Image _creditpanel;
    [SerializeField] Image _audiopanel;
    [SerializeField] Image _keyconpanel;

    private Color color = new Color(0f, 0f, 0f, 1);
    // Start is called before the first frame update
    void Start()
    {
        colorset(_menupanel,color);
        colorset(_creditpanel,color);
        colorset(_audiopanel,color);
        colorset(_keyconpanel,color);
    }

    // Update is called once per frame
    public void colorset(Image x,Color color)
    {
        x.color = color;
    }
    
}
