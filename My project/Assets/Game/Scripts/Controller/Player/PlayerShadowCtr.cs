using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class PlayerShadowCtr : MonoBehaviour,IController
{
    [SerializeField]private float activeTime;//阴影显现时间
    [SerializeField]private float activeNowTime;
    //透明度相关
    [SerializeField] private float alpha;
    [SerializeField] private float initAlpha;
    [SerializeField] private float alphaMultiply;
    
    [SerializeField]private SpriteRenderer sp;
    [SerializeField]private SpriteRenderer playerSp;

    [SerializeField]private Transform playerTs;

    [SerializeField]private Color _color;

    private void OnEnable()
    {
        playerTs = GameObject.FindWithTag("Player").transform;
        playerSp = playerTs.gameObject.GetComponent<SpriteRenderer>();
        alpha = initAlpha;
        
        transform.position = playerTs.position;
        transform.localScale = playerTs.localScale;

        sp = GetComponent<SpriteRenderer>();
        sp.sprite = playerSp.sprite;
        
        activeNowTime = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiply;
        _color = new Color(0.65f, 0.1f, 0, alpha);

        sp.color = _color;

        if (Time.time >= activeTime + activeNowTime)
        {
            //回到池中
            TypeEventSystem.Global.Send(new AddToShadowPool()
            {
                go = gameObject
            });
                
        }
    }
    
    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
