using System;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using Unity.Mathematics;
using UnityEngine;

public class RespondPlayerCtr : MonoBehaviour,IController
{
    public Transform respondPoint;
    public GameObject prefab;
    public GameObject player;

    private void Start()
    {
        TypeEventSystem.Global.Register<PlayerRespondEvt>(RespondPlayer).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            TypeEventSystem.Global.Send<PlayerRespondEvt>();
            
        }
    }
    void RespondPlayer(PlayerRespondEvt playerRespondEvt)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = respondPoint.position;
        Instantiate(prefab,respondPoint.position,quaternion.identity);
        TypeEventSystem.Global.Send<CloseTrailEvt>();
    }

    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}
