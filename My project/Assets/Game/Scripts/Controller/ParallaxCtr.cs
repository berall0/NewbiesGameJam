using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCtr : MonoBehaviour
{
    public Transform cam;
    private Vector2 parallaxNumMid1= new Vector2(0.2f,0.17f);
    private Vector2 parallaxNumMid2= new Vector2(0.1f,0.08f);
    private Vector2 parallaxNumback= new Vector2(0.05f,0.03f);
    
    public Transform midGround1;
    public Transform midGround2;
    public Transform backGround;
    
    private Vector2 startpointMid1 ;
    private Vector2 startpointMid2 ;
    private Vector2 startpointBack ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startpointMid1 = midGround1.position;
        startpointBack = backGround.position;
        startpointMid2 = midGround2.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = cam.position;
        midGround1.position = new Vector2(startpointMid1.x + position.x * parallaxNumMid1.x,
            midGround1.position.y);
        midGround2.position = new Vector2(startpointMid2.x + position.x * parallaxNumMid2.x,
            midGround2.position.y);
        backGround.position = new Vector2(startpointBack.x + position.x * parallaxNumback.x,
            backGround.position.y);
        // var position = cam.position;
        // midGround1.position = new Vector2(startpointMid1.x + position.x * parallaxNumMid1.x,
        //     startpointMid1.y +  position.y * parallaxNumMid1.y);
        // midGround2.position = new Vector2(startpointMid2.x + position.x * parallaxNumMid2.x,
        //     startpointMid2.y +  position.y * parallaxNumMid2.y);
        // backGround.position = new Vector2(startpointBack.x + position.x * parallaxNumback.x,
        //     startpointBack.y +  position.y * parallaxNumback.y);
    }
    
    // public Transform skyBox;
    // public Transform sky;
    // public Transform fowardSky;
    // public Transform bigBackGD;
    // public Transform forwardGD;
    //
    // public Vector2 skyBoxv2;
    // public Vector2 skyv2;
    // public Vector2 fowardSkyv2;
    // public Vector2 bigBackGDv2;
    // public Vector2 forwardGDv2;
    //
    // public Transform camera;
    //
    // private void Start()
    // {
    //     skyBoxv2 = skyBox.position;
    //     skyv2 = sky.position;
    //     fowardSkyv2 = fowardSky.position;
    //     bigBackGDv2 = bigBackGD.position;
    //     forwardGDv2 = forwardGD.position;
    // }
    //
    // private void Update()
    // {
    //     skyBox.position = ParallaxCount(skyBox.position.z, skyBoxv2);
    //     sky.position = ParallaxCount(sky.position.z, skyv2);
    //     fowardSky.position = ParallaxCount(fowardSky.position.z, fowardSkyv2);
    //     bigBackGD.position = ParallaxCount(bigBackGD.position.z, bigBackGDv2);
    //     forwardGD.position = ParallaxCount(forwardGD.position.z, forwardGDv2);
    //
    // }
    //
    // Vector3 ParallaxCount(float v,Vector3 stv)
    // {
    //     float pl = 1 / v;
    //     return new Vector3(stv.x + camera.position.x * pl, stv.y + camera.position.y * pl * 0.8f);
    // }
}