using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float secondsToDestroy = 2f;
    public LaserAttack laserAttack;

    void Update()
    {
        StartCoroutine(DestroyInSeconds());
    }

    IEnumerator DestroyInSeconds()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        
        if ( laserAttack != null )
        {
            laserAttack.DoneLasering();
        }

        Destroy(gameObject);
    }
}
