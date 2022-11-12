using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;


public class AudioCtr : MonoBehaviour,IController
{
    public List<AudioClip> BGMList = new List<AudioClip>();
    private Dictionary<string, AudioClip> BGMDict = new Dictionary<string, AudioClip>();
    public List<AudioClip> BGSList = new List<AudioClip>();
    private Dictionary<string, AudioClip> BGSDict = new Dictionary<string, AudioClip>();
    public List<AudioClip> GrassList = new List<AudioClip>();
    public List<AudioClip> SnowList = new List<AudioClip>();
    Random random = new Random();


    public List<AudioSource> AS = new List<AudioSource>();

    private void Start()
    {
        foreach (var clip in BGMList)
        {
            BGMDict.Add(clip.name,clip);
        }
        foreach (var clip in BGSList)
        {
            BGSDict.Add(clip.name,clip);
        }

        TypeEventSystem.Global.Register<PlayerDashOnceTimeEvt>(DashPlay).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<PlayerWalkEvt>(WalkPlay).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<PlayerJumpEvt>(JumpPlay).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<OpenAudioEvt>(TurnOnAudio).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<OffAudioEvt>(TurnOffAudio).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<UIBtnDownEvt>(UIBtnDown).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<PlayerRespondEvt>(RespondPlay).UnRegisterWhenGameObjectDestroyed(gameObject);

        BGMPlay();
    }

    void DashPlay(PlayerDashOnceTimeEvt playerDashEvt)
    {
        BGSDict.TryGetValue("Dash", out AudioClip value);
        AS[1].PlayOneShot(value);
    }
    void RespondPlay(PlayerRespondEvt playerDashEvt)
    {
        BGSDict.TryGetValue("Respond", out AudioClip value);
        AS[1].PlayOneShot(value);
    }
    void WalkPlay(PlayerWalkEvt playerWalk)
    {
        if (!AS[1].isPlaying)
        {
            if (playerWalk.isGrass)
            {
                AS[1].clip = GrassList[random.Next(0, GrassList.Count - 1)];
                AS[1].Play();
            }
            else
            {
                AS[1].clip = SnowList[random.Next(0, SnowList.Count - 1)];
                AS[1].Play();
            }
        }
    }
    void JumpPlay(PlayerJumpEvt playerDashEvt)
    {
        BGSDict.TryGetValue("Jump", out AudioClip value);
        AS[1].PlayOneShot(value);
    }

    void BGMPlay()
    {
        AS[0].Play();
    }

    void TurnOnAudio(OpenAudioEvt openAudioEvt)
    {
        AS[0].volume = 0.3f;
        AS[1].volume = 0.6f;
    }
    
    void TurnOffAudio(OffAudioEvt openAudioEvt)
    {
        foreach (var i in AS)
        {
            i.volume = 0;
        }
    }

    void UIBtnDown(UIBtnDownEvt uiBtnDownEvt)
    {
        BGSDict.TryGetValue("UI", out AudioClip value);
        AS[1].PlayOneShot(value);
    }

    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
