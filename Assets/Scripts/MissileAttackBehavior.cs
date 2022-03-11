using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackBehavior : MonoBehaviour
{
    public GameObject MissilePrefab;
    public float DelayBetweenMissiles = 2f;
    public Transform SpawnPoint;

    private bool isShooting = false;

    private GlowingBehavior glowingBehavior;

    // Start is called before the first frame update
    void Awake()
    {
        glowingBehavior = GetComponent<GlowingBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShooting)
            StartCoroutine(LaunchMissiles());
    }

    private IEnumerator LaunchMissiles()
    {
        isShooting = true;
        yield return new WaitForSeconds(DelayBetweenMissiles);
        glowingBehavior.glowForSecondsTime = 1f;
        glowingBehavior.DoGlowDeGlow = true;
        Instantiate(MissilePrefab, SpawnPoint.position, Quaternion.identity);
        isShooting = false;
    }
}
