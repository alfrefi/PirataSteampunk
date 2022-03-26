using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class Jump : MonoBehaviour
    {
        Rigidbody2D body2d;
        private CharacterCollision charColl;

        public bool jumped;
        public bool jumpHold;
        public float jumpForce = 10f;

        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        public bool jumpedFromRope = false;

        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            body2d = GetComponent<Rigidbody2D>();
            charColl = GetComponent<CharacterCollision>();
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if ( jumped && charColl.onGround || jumpedFromRope)
            {
                body2d.velocity = new Vector2(body2d.velocity.x, 0);
                body2d.velocity += Vector2.up * jumpForce;

                jumpedFromRope = false;

                animator.SetBool("Jumped", true);
            }
        }
    }
}
