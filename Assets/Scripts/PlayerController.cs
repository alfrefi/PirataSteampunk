using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class PlayerController : MonoBehaviour
    {
        Movement movement;
        Jump jump;
        GrapplingGun grapplingGun;

        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<Movement>();
            jump = GetComponent<Jump>();
            grapplingGun = GetComponentInChildren<GrapplingGun>();
        }

        // Update is called once per frame
        void Update()
        {
            movement.xMov = Input.GetAxis("Horizontal");
            movement.yMov = Input.GetAxis("Vertical");

            jump.jumped = Input.GetButtonDown("Jump");
            jump.jumpHold = Input.GetButton("Jump");

            if (grapplingGun.grappleRope.isGrappling)
            {
                jump.jumpedFromRope = Input.GetButtonDown("Jump");
            }
        }
    }
}
