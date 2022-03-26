using Cinemachine;
using MovementWithHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AngryShakeBehavior : MonoBehaviour
{
    PulpoController pulpoController;

    Camera mainCamera;
    CinemachineVirtualCamera cmvCam;
    
    Coroutine shakeCoroutine;

    CharacterCollision player;
    List<Collider2D> platforms;

    // Start is called before the first frame update
    void Awake()
    {
        pulpoController = GetComponent<PulpoController>();

        mainCamera = Camera.main;
        player = GameObject.Find("Player").GetComponent<CharacterCollision>();
        platforms = GameObject.FindGameObjectsWithTag("Platform").AsQueryable().Select(g => g.GetComponent<Collider2D>()).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (cmvCam == null )
        {
            cmvCam = mainCamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        }

        if (shakeCoroutine == null ) 
        { 
           shakeCoroutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        while ( pulpoController.AttackInProgress() )
        {
            yield return false;
        }

        StartShake();
        yield return false;
        
        while ( !player.onGround )
        {
            yield return false;
        }
        DoneShaking();
    }

    void DoneShaking()
    {
        platforms.ForEach(p => p.enabled = true);
        cmvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        shakeCoroutine = null;
        this.enabled = false;
    }

    void StartShake()
    {
        cmvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2;
        platforms.ForEach(p => p.enabled = false);
    }
}
