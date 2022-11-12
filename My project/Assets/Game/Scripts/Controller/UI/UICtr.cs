using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class UICtr : MonoBehaviour,IController
{
    public GameObject pausePanel;
    public Button pauseBtn;

    public Button ContinueBtn;
    public Button QuitBtn;
    public Button AudioBtn;

    public Text textAudio;

    private bool isPause = false;
    private bool isOffAudio = false;

    private void Start()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            PausePanelOpen();
            isPause = true;
            TypeEventSystem.Global.Send<UIBtnDownEvt>();
        });
        ContinueBtn.onClick.AddListener(() =>
        {
            ContinueGame();
            TypeEventSystem.Global.Send<UIBtnDownEvt>();

        });
        QuitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
            TypeEventSystem.Global.Send<UIBtnDownEvt>();

        });
        AudioBtn.onClick.AddListener(() =>
        {
            TypeEventSystem.Global.Send<UIBtnDownEvt>();

            if (isOffAudio)
            {
                OpenAudio();
            }
            else
            {
                CloseAudio();
            }
        });
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPause)
        {
            PausePanelOpen();
        }

        if (Input.GetButtonDown("Pause") && isPause)
        {
            ContinueGame();
        }

        pauseBtn.image.raycastTarget = !isPause;
        
    }

    void PausePanelOpen()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    void OpenAudio()
    {
        TypeEventSystem.Global.Send<OpenAudioEvt>();
        isOffAudio = false;
        textAudio.text = "目前音量：有声儿";
    }

    void CloseAudio()
    {
        TypeEventSystem.Global.Send<OffAudioEvt>();
        isOffAudio = true;
        textAudio.text = "目前音量：没声儿";
    }
    
    
    
    

    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
