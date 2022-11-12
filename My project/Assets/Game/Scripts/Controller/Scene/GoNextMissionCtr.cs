using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextMissionCtr : MonoBehaviour
{
    private bool AniDone = false;
    private void Start()
    {
        TypeEventSystem.Global.Register<EnterSceneAniFinEvt>(Ani).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            TypeEventSystem.Global.Send<GoNextSceneEvt>();
            StartCoroutine(GoScene(SceneManager.GetActiveScene().buildIndex + 1 ));
        }
    }

    IEnumerator GoScene(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (AniDone)
            {
                asyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
        
    }

    void Ani(EnterSceneAniFinEvt aniFinEvt)
    {
        AniDone = true;
    }

    // private void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //     }
    // }
}