using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float speed = 5f;

    public Vector2 PointA;
    public Vector2 PointB;

    private Vector2 nextTarget;

    void Awake()
    {
        nextTarget = PointB;
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextTarget, step);
        
        if ( Vector3.Distance(transform.position, nextTarget) < 0.001f )
        {
            if (nextTarget == PointB )
            {
                nextTarget = PointA;
            }
            else
            {
                nextTarget = PointB;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(PointA, PointB);

        Gizmos.DrawWireSphere(PointA, .4f);
    }
}
