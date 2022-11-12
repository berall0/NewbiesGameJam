using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoUIScaleCtr : MonoBehaviour
{
    private Transform ts;

    // Start is called before the first frame update
    void Start()
    {
        ts = transform;
        ts.localScale = new Vector3(0, 0, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(PlayUI());
    }

    IEnumerator PlayUI()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        yield return new WaitForSeconds(.2f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }
}
