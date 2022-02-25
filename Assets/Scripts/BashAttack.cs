using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashAttack : MonoBehaviour
{
    public float bashSpeed = 5f;
    public float rotationSpeed = 5f;
    
    public GameObject player;

    public Vector3 origin = Vector3.negativeInfinity;
    public Quaternion originalRotation;
    public Vector3 target = Vector3.negativeInfinity;
    public float distance;
    public bool bashAttackDone;
    public bool rotationDone;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalRotation = transform.rotation;
        Reset();
    }

    void Update()
    {
        if ( target.Equals(Vector3.negativeInfinity) )
        {
            origin = transform.position;
            target = player.transform.position;
        }

        if ( LookAt(target) ) { 
            
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * bashSpeed);
            
            distance = Vector3.Distance(transform.position, target);
            if ( distance <= .5 )
            {
                rotationDone = false;
                bashAttackDone = true;
            }
        }

        if (bashAttackDone)
        {
            StartCoroutine(AnimateRotationTowards(transform, Quaternion.identity, 1f));

            if ( rotationDone ) 
            { 
                transform.position = Vector3.Lerp(transform.position, origin, Time.deltaTime * (bashSpeed/2));
            
                distance = Vector3.Distance(transform.position, origin);
                if ( distance <= .5 )
                {
                    Reset();
                    rotationDone = false;
                    this.enabled = false;
                }
            }
        }
    }

    bool LookAt(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - transform.position.x;
        float distanceY = lookAtPosition.y - transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotationSpeed);

        return transform.rotation == endRotation;
    }

    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        target = Vector3.negativeInfinity;
        origin = Vector3.negativeInfinity;
        bashAttackDone = false;
        rotationDone = false;
    }

    private System.Collections.IEnumerator AnimateRotationTowards(Transform target, Quaternion rot, float dur)
    {
        float t = 0f;
        Quaternion start = target.rotation;
        while ( t < dur )
        {
            target.rotation = Quaternion.Slerp(start, rot, t / dur);
            yield return null;
            t += Time.deltaTime;
        }
        target.rotation = rot;

        rotationDone = true; 
    }
}
