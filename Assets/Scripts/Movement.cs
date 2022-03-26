using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class Movement : MonoBehaviour
    {
        Rigidbody2D body2d;
        CharacterCollision charColl;
        private SpriteRenderer sprite;
        private Animator animator;

        public float xMov;
        public float yMov;

        public float Speed = 10f;

        public bool CanMove;
        public bool IsGrappled;

        // Start is called before the first frame update
        void Start()
        {
            body2d = GetComponent<Rigidbody2D>();
            charColl = GetComponent<CharacterCollision>();
            sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if ( CanMove )
            {
                body2d.velocity = new Vector2(xMov * Speed, body2d.velocity.y);
            } 
            else if ( IsGrappled )
            {
                body2d.velocity += new Vector2(xMov/50, 0);
            }

            if ( xMov > 0)
            {
                sprite.flipX = false;
            } 
            else  if (xMov < 0 )
            {
                sprite.flipX = true;
            }

            animator.SetBool("IsRunning", xMov != 0);
            animator.SetBool("IsGrounded", charColl.onGround);
            animator.SetFloat("YVelocity", body2d.velocity.y);
        }
    }
}
