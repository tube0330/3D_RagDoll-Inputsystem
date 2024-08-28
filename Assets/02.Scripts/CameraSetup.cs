using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    CinemachineVirtualCamera cineCam;

    void Start()
    {
        cineCam = FindObjectOfType<CinemachineVirtualCamera>();

        cineCam.LookAt = transform;
        cineCam.Follow = transform;
    }
}
