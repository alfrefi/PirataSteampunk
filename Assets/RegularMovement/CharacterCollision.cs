using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RegularMovement
{
    public class CharacterCollision : MonoBehaviour
    {
        public LayerMask groundLayer;
    
        public bool onGround;
        //public bool onWall;
        //public bool onRightWall;
        //public bool onLeftWall;
        //public int wallSide;

        public float collisionRadius = 0.25f;
        public Vector2 bottomOffset;//, rightOffset, leftOffset;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
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

