using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionNameCtr : MonoBehaviour
{
    private Image _selfSP;
    public Text textSP;
    private void OnEnable()
    {
        _selfSP = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        _selfSP.DOFade(0, 2f);
        textSP.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        _selfSP.color = Color.white;
        textSP.color = new Color(0.3f, 0, 0, 1);

    }
}
