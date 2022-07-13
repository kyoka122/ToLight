using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;
//using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject TitlePanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject CreditPanel;
    [SerializeField] GameObject GoBackPanel;
    [SerializeField] GameObject AudioSettingPanel;
    [SerializeField] GameObject KeyconPanel;
    [SerializeField] private Button Startbutton;
    [SerializeField] private Button Settingbutton;
    [SerializeField] private Button Creditbutton;
    [SerializeField] private Button Endgamebutton;
    [SerializeField] private Button Soundbutton;
    [SerializeField] private Button Keyconbutton;
    [SerializeField] private Button Backbutton;
    [SerializeField] private Button Gohomebutton;
    [SerializeField] private Button CreditBackbutton;

    [SerializeField] ColorSet _colorset;
    [SerializeField] TitleBGM _titleBgm;

    //add oriver
    [SerializeField] GameManager gameManager;

    public enum PanelState
    {
        Title,
        Play,
        Menu,
        AudioKey,
        KeySet
    }
    public PanelState currentPanelState;
    public PanelState StartPanelState;
    // Start is called before the first frame update
    void Start()
    {
        TitlePisActive();
        Setup();
    }

    private void Setup()
    {
        /// StartPanel
        Startbutton.OnClickAsObservable()
            .Subscribe(_ => {
                PlayisActive();
                gameManager.OpeningStart();
    
    })
            .AddTo(this);
        Settingbutton.OnClickAsObservable()
            .Subscribe(_ => MenuPisActiveWithoutGoBack())
            .AddTo(this);
        Creditbutton.OnClickAsObservable()
            .Subscribe(_ => CreditPisActive())
            .AddTo(this);
        Endgamebutton.OnClickAsObservable()
            .Subscribe(_ => EndgameisActive())
            .AddTo(this);
        /// MenuPanel
        Soundbutton.OnClickAsObservable()
            .Subscribe(_ => SoundPisActive())
            .AddTo(this);
        Keyconbutton.OnClickAsObservable()
            .Subscribe(_ => KeyconPisActive())
            .AddTo(this);
        Backbutton.OnClickAsObservable()
            .Subscribe(_ => BackPage())
            .AddTo(this);
        Gohomebutton.OnClickAsObservable()
            .Subscribe(_ => TitlePisActive())
            .AddTo(this);

        /// CreditPanel
        CreditBackbutton.OnClickAsObservable()
            .Subscribe(_ => TitlePisActive())
            .AddTo(this);


    }
   
    public void TitlePisActive()
    {
        //GameController.Instance.SetCurrentState(GameState.Start);
        _colorset.colorset(MenuPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 1));
        _colorset.colorset(AudioSettingPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 1));
        _colorset.colorset(KeyconPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 1));

        Time.timeScale = 0f;
        TitlePanel.SetActive(true);
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(false);
        GoBackPanel.SetActive(false);
        AudioSettingPanel.SetActive(false);
        KeyconPanel.SetActive(false);
        StartPanelState = PanelState.Title;
        currentPanelState = PanelState.Title;
        _titleBgm.BGMplay();
    }
    public void CreditPisActive()
    {
        //_colorset.colorset(CreditPanel.GetComponent<Image>(),new Color(0f,0f,0f,1));
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(true);
        GoBackPanel.SetActive(false);
        AudioSettingPanel.SetActive(false);
        KeyconPanel.SetActive(false);
    }
    public void MenuPisActiveWithoutGoBack()
    {
        Time.timeScale = 0f;
        //_colorset.colorset(MenuPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 1));
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(true);
        CreditPanel.SetActive(false);
        GoBackPanel.SetActive(false);
        AudioSettingPanel.SetActive(false);
        KeyconPanel.SetActive(false);
        currentPanelState = PanelState.Menu;
    }
    public void MenuPisActiveWithGoBack()
    {
        if (currentPanelState != PanelState.Title)
        {
            Time.timeScale = 0f;
            //_colorset.colorset(MenuPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 0.7f));
            _titleBgm.BGMend();
            TitlePanel.SetActive(false);
            MenuPanel.SetActive(true);
            CreditPanel.SetActive(false);
            GoBackPanel.SetActive(true);
            AudioSettingPanel.SetActive(false);
            KeyconPanel.SetActive(false);
            currentPanelState = PanelState.Menu;
        }
    }

    public void PlayisActive()
    {
        //GameController.Instance.SetCurrentState(GameState.Playing);
        _colorset.colorset(MenuPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 0.9f));
        _colorset.colorset(AudioSettingPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 0.9f));
        _colorset.colorset(KeyconPanel.GetComponent<Image>(), new Color(0f, 0f, 0f, 0.9f));

        Time.timeScale = 1f;
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(false);
        GoBackPanel.SetActive(false);
        AudioSettingPanel.SetActive(false);
        KeyconPanel.SetActive(false);
        StartPanelState = PanelState.Play;
        currentPanelState = PanelState.Play;
        _titleBgm.BGMfade();
        Debug.Log("play!");
    }
    public void BackPage()
    {
        if (currentPanelState == PanelState.AudioKey && StartPanelState==PanelState.Title)
        {
            MenuPisActiveWithoutGoBack();
        }
        else if (currentPanelState == PanelState.Menu && StartPanelState == PanelState.Play)
        {
            PlayisActive();
        }
        else if (currentPanelState == PanelState.AudioKey && StartPanelState == PanelState.Play)
        {
            Debug.Log("Back to menu page!");
            MenuPisActiveWithGoBack();
        }
        Debug.Log("Back 1 page!");
    }
    public void SoundPisActive()
    {
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(false);
        GoBackPanel.SetActive(true);
        AudioSettingPanel.SetActive(true);
        KeyconPanel.SetActive(false);
        currentPanelState = PanelState.AudioKey;
        Debug.Log("Music!");
    }
    public void KeyconPisActive()
    {
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(false);
        GoBackPanel.SetActive(true);
        AudioSettingPanel.SetActive(false);
        KeyconPanel.SetActive(true);
        currentPanelState = PanelState.AudioKey;
        Debug.Log("Key config");
    }
    public void EndgameisActive()
    {
        //GameController.Instance.SetCurrentState(GameState.End);
        Debug.Log("End!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        //#else
            Application.Quit();

            #endif
    }
    
}
