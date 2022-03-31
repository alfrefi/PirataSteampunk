using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class CharacterCollision : MonoBehaviour
    {
        public LayerMask groundLayer;
        public Collider2D collision;
        public bool onGround;
        //public bool onWall;
        //public bool onRightWall;
        //public bool onLeftWall;
        //public int wallSide;

        public float collisionRadius = 0.25f;
        public Vector2 bottomOffset;//, rightOffset, leftOffset;

        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            collision = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
            onGround = collision;
            //onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            //    || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            //onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            //onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            //wallSide = onRightWall ? -1 : 1;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
            //Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
            //Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        }
    }
}
