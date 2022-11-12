using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CamerManager : MonoBehaviour
{
    public List<Transform> ChangePoint = new List<Transform>();
    public List<CinemachineVirtualCamera> VCM = new List<CinemachineVirtualCamera>();

    private void Start()
    {
        
    }
}
