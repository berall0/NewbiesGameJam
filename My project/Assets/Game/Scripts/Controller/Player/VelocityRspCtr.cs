using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class VelocityRspCtr : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (MathF.Abs(rb.velocity.y) >= 30)
        {
           TypeEventSystem.Global.Send<PlayerRespondEvt>();
        }
    }
}
