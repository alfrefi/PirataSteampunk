using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class Movement : MonoBehaviour
    {
        CharacterCollision charColl;

        Rigidbody2D body2d;

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
        }
    }
}
