using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;

public class ShadowPoolCtr : MonoBehaviour,IController
{
    [SerializeField] private GameObject ShadowPrefabs;
    [SerializeField]private int poolCount;

    private Queue<GameObject> shadowQueue = new Queue<GameObject>();

    private void Awake()
    {
        CreatePool();
    }

    private void Start()
    {
        TypeEventSystem.Global.Register<AddToShadowPool>(ToAddPool).UnRegisterWhenGameObjectDestroyed(gameObject);
        TypeEventSystem.Global.Register<PlayerDashEvt>(GetFromPool).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var shadow = Instantiate(ShadowPrefabs,transform);
            AddPool(shadow);
        }
    }

    void ToAddPool(AddToShadowPool addToShadowPool)
    {
        AddPool(addToShadowPool.go);
    }

    public void AddPool(GameObject go)
    {
        go.SetActive(false);
        shadowQueue.Enqueue(go);
    }

    public void GetFromPool(PlayerDashEvt playerDashEvt)
    {
        if (shadowQueue.Count == 0) CreatePool();
        var go = shadowQueue.Dequeue();
        go.SetActive(true);
    }

    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
