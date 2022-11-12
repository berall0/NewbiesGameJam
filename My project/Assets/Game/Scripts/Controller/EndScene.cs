using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public Text text;
    private float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
        StartCoroutine(Play());
    }

    private void Update()
    {
        text.color = new Color(1, 1, 1, alpha);
    }

    IEnumerator Play()
    {
        DOTween.To((value) =>
        {
            alpha = value;
        }, 0, 1, 2f);
        text.transform.DOScale(new Vector3(1,1,1),2f);
        yield return new WaitForSeconds(5f);
        text.text = "完？";
        yield return new WaitForSeconds(1f);
        DOTween.To((value) =>
        {
            alpha = value;
        }, 1, 0, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Village");
    }
}
