using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public class GameLineDownCtr : MonoBehaviour
{
    private RaycastHit2D raycastHit2D;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            raycastHit2D = Physics2D.Raycast(col.transform.position, Vector2.up, 50f, LayerMask.GetMask("GameLineUp"));
            TypeEventSystem.Global.Send<CloseTrailEvt>();
            col.transform.position = raycastHit2D.point + Vector2.down* 0.5f;
        }
    }
}
