using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpoController : MonoBehaviour
{
    public float timeBetweenAttacks;
    public List<ArmorScript> armors;

    private FlyingBehavior flyingBehavior;
    private BashAttack bashAttack;
    private LaserAttack laserAttack;

    
    void Awake()
    {
        flyingBehavior = GetComponent<FlyingBehavior>();
        bashAttack = GetComponent<BashAttack>();
        laserAttack = GetComponent<LaserAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !flyingBehavior.enabled && !bashAttack.enabled && !laserAttack.enabled )
        {
            ChangeArmorProtection(true);

            flyingBehavior.enabled = true;
            StartCoroutine(DoAttack());
        }
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        
        ChangeArmorProtection(false);

        int attack = UnityEngine.Random.Range(1, 3);

        flyingBehavior.enabled = false;
        switch (attack)
        {
            case 1:
                bashAttack.enabled = true;
                break;
            case 2:
                laserAttack.enabled = true;
                break;
        }
    }

    private void ChangeArmorProtection(bool isProtected)
    {
        armors.ForEach(a => a.isProtected = isProtected);
    }
}
