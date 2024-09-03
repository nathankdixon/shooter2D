using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public GameObject cineCamera;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private bool shaking = false;
    private void Start()
    {
        cinemachineVirtualCamera = cineCamera.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        print("START SHAKE");
        shakeTimer = time + Time.time;
        shaking = true;
    } 

    void Update()
    {
        if (! shaking)
        {
            return;
        }
        if (Time.time >= shakeTimer)
        {
            print("STOP SHAKE");
            shaking = false;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}
