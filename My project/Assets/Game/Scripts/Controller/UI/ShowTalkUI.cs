using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTalkUI : MonoBehaviour
{
    public GameObject UI;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            UI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            UI.SetActive(false);
            UI.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
