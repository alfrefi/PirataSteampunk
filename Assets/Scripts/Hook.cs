using MovementWithHook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer rope;
    private Vector3 lastPosition = Vector3.negativeInfinity;

    private void Awake()
    {
        lastPosition = Vector3.negativeInfinity;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = Vector3.zero;
        if ( rope.enabled )
        {
            if (lastPosition == Vector3.negativeInfinity )
            {
                lastPosition = gameObject.transform.position;
            }

            var lineLastPosition = rope.GetPosition(rope.positionCount-1);
            gameObject.transform.position = lineLastPosition;
        } 
        else if ( !lastPosition.Equals(Vector3.negativeInfinity) )
        {
            lastPosition = Vector3.negativeInfinity;
        } 
    }
}
