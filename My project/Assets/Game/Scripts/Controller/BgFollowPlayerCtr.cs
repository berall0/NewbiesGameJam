using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFollowPlayerCtr : MonoBehaviour
{
    public Transform cameraTs;
    private float yOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        yOffset = cameraTs.position.y - gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, cameraTs.position.y - yOffset + gameObject.transform.position.y,
            gameObject.transform.position.z);
        
    }
}
