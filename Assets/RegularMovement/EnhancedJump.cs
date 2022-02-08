using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RegularMovement
{
    public class EnhancedJump : MonoBehaviour
    {
        Rigidbody2D body2d;
        private CharacterCollision charColl;

        public bool jumped;
        public bool jumpHold;
        public float jumpForce = 10f;

        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        // Start is called before the first frame update
        void Start()
        {
            body2d = GetComponent<Rigidbody2D>();
            charColl = GetComponent<CharacterCollision>();
        }

        // Update is called once per frame
        void Update()
        {
            if ( jumped && charColl.onGround )
            {
                body2d.velocity = new Vector2(body2d.velocity.x, 0);
                body2d.velocity += Vector2.up * jumpForce;
            }

            if ( body2d.velocity.y < 0 )
            {
                body2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if ( body2d.velocity.y > 0 && !jumpHold )
            {
                body2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
