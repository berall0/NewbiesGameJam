using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class GameLineUpCtr : MonoBehaviour
{
    private RaycastHit2D raycastHit2D1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            raycastHit2D1 = Physics2D.Raycast(col.transform.position, new Vector2(0,-1), 50f, LayerMask.GetMask("GameLineDown"));
            TypeEventSystem.Global.Send<CloseTrailEvt>();
            col.transform.position = raycastHit2D1.point + Vector2.up* 0.5f;
        }
    }
}
