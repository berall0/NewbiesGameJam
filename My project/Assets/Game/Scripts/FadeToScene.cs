using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class FadeToScene : MonoBehaviour
{
    public Animator At;

    private void Awake()
    {
        TypeEventSystem.Global.Register<GoNextSceneEvt>(EnterScene).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void Start()
    {
        ExitScene();
    }

    void EnterScene(GoNextSceneEvt goNextSceneEvt)
    {
        At.Play("FadeToScene");
    }

    void ExitScene()
    {
        At.Play("FadeBackScene");
    }

    private void SendEvt()
    {
        TypeEventSystem.Global.Send<EnterSceneAniFinEvt>();
    }
}
