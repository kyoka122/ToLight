using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class AudioMana : MonoBehaviour
{
    //[SerializeField] private AudioSource BGM;
    [SerializeField] public Slider BGMslider;
    [SerializeField] public Slider SEslider;
    // Start is called before the first frame update
    void Start()
    {
        BGMslider.value = 0.3f;
        SEslider.value = 0.3f;

        BGMslider.onValueChanged.AddListener(value => BGMManager.Instance.ChangeBaseVolume(value));
        SEslider.onValueChanged.AddListener(value => SEManager.Instance.ChangeBaseVolume(value));
    }
}
