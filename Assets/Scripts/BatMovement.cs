using MovementWithHook;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float speed = 5f;

    public Vector2 PointA;
    public Vector2 PointB;
    public bool CanMove;

    private Vector2 nextTarget;
    private bool isFullyGrappled;

    void Awake()
    {
        nextTarget = PointB;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
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
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(PointA, PointB);

        Gizmos.DrawWireSphere(PointA, .4f);
        Gizmos.DrawWireSphere(PointB, .4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Hook")
        {
            isFullyGrappled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Hook" && collision.GetComponentInParent<GrapplingGun>().grappleRope.isGrappling)
        {
            if ( !isFullyGrappled )
            {
                //Play Stasis animation or something
                GetComponent<SpriteRenderer>().color = Color.cyan;
                isFullyGrappled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.name == "Hook" )
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
