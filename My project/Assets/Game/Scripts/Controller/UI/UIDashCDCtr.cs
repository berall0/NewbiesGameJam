using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIDashCDCtr : MonoBehaviour,IController
{
    [SerializeField]
    private Image cdImage;
    [SerializeField]
    private float cdTime;
    private float cdNowTime;
    
    private bool iscd;

    private void Start()
    {
        TypeEventSystem.Global.Register<PlayerDashOnceTimeEvt>(Dash).UnRegisterWhenGameObjectDestroyed(gameObject);
        cdNowTime = cdTime;
        cdImage.fillAmount = 0;
    }

    private void Update()
    {
        if (iscd)
        {
            cdImage.fillAmount -= 1.0f/cdTime*Time.deltaTime;
            if (cdNowTime <= 0.1f)
            {
                iscd = false;
                cdNowTime = cdTime;
            }
        }
    }

    void Dash(PlayerDashOnceTimeEvt playerDashEvent)
    {
        iscd = true;
        cdImage.fillAmount = 1;
    }


    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
