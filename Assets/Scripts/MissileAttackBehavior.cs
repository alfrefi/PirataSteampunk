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
    public bool waitForAttackToShoot = false;
    public bool stackGlowOnAttack = true;

    private PulpoController pulpoController;
    private GlowingBehavior glowingBehavior;

    // Start is called before the first frame update
    void Awake()
    {
        pulpoController = GetComponent<PulpoController>();
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
        
        while( waitForAttackToShoot && pulpoController.AttackInProgress() )
        {
            yield return false;
        }

        if ( stackGlowOnAttack || !pulpoController.AttackInProgress() )
        {
            glowingBehavior.glowForSecondsTime = 1f;
            glowingBehavior.DoGlowDeGlow = true;
        }

        Instantiate(MissilePrefab, SpawnPoint.position, Quaternion.identity);
        isShooting = false;
    }
}
