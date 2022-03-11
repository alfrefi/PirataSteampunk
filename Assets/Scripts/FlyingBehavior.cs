using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehavior : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotateSpeed = 5.0f;

    Vector3 newPosition;

    void Start()
    {
        PositionChange();
    }

    void PositionChange()
    {
        newPosition = new Vector2(Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f), 
                                  Random.Range(transform.position.y, transform.position.y + 2.0f));
    }

    void Update()
    {
        if ( Vector2.Distance(transform.position, newPosition) < 1 )
            PositionChange();

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

        LookAt2D(newPosition);
    }

    void LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - transform.position.x;
        float distanceY = lookAtPosition.y - transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }

    internal void SetPositionToBottom()
    {
        newPosition = new Vector2(transform.position.x, 0f);
    }
}
