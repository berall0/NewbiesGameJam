using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalCGCtr : MonoBehaviour
{
    public Text Text;
    public GameObject go;
    private GameObject player;
    public ParticleSystem pt;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;
            Play();
        }
    }

    void Play()
    {
        player.GetComponent<PlayerMoveCtr>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Animator>().Play("Idle");
        StartCoroutine(PlayCG());
    }

    IEnumerator PlayCG()
    {
        pt.Stop();
        go.SetActive(true);
        player.GetComponent<Rigidbody2D>().freezeRotation = false;
        Text.text = "这.....这是什么鬼....";
        go.transform.DOShakePosition(3f, 5f, 10,360);
        yield return new WaitForSeconds(3f);
        Text.text = "山顶居然有这种东西？" +
                    "真<color=red>诡异</color>啊...";
        go.transform.DOShakePosition(3f, 10f, 10,360,true);
        yield return new WaitForSeconds(3f);
        Text.text = "头....头好昏。";
        go.transform.DOShakePosition(3f, 15f, 10,360,true);
        yield return new WaitForSeconds(3f);
        Text.text = "重力这是....<color=red>倒转</color>了？" +
                    "那我现在是摔向<color=red>山顶？山底？</color>" +
                    "难道...";
        DOTween.To((value) =>
        {
             player.GetComponent<Rigidbody2D>().gravityScale = value;
        },0,-3,30f);
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up);
        go.transform.DOShakePosition(3f, 20f, 10,360,true);
        yield return new WaitForSeconds(3f);
        Text.text = "<size=70><color=red>山顶就是山底？？？？？</color></size>";
        go.transform.DOShakePosition(3f, 25f, 10,360,true);
        yield return new WaitForSeconds(3f);
        Text.text = "<size=145>嗷！！！</size>";
        go.transform.DOShakePosition(3f, 30f, 10,360,true);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
}
