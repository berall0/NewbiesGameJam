using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class ForcePlayerCtr : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            UI.SetActive(true);
            TypeEventSystem.Global.Send<PlayerRespondEvt>();
            TypeEventSystem.Global.Send<CloseTrailEvt>();
            
        }
    }

    private IEnumerator Push()
    {
        for (int i = 0; i < 3; i++)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(2, player.GetComponent<Rigidbody2D>().velocity.y);
            yield return new WaitForSeconds(0.1f);
        }
       
    }
}
