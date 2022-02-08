using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RegularMovement
{
    public class Movement : MonoBehaviour
    {
        CharacterCollision charColl;

        Rigidbody2D body2d;

        public float xMov;
        public float yMov;

        public float speed = 10f;

        // Start is called before the first frame update
        void Start()
        {
            body2d = GetComponent<Rigidbody2D>();
            charColl = GetComponent<CharacterCollision>();
        }

        // Update is called once per frame
        void Update()
        {
            body2d.velocity = new Vector2(xMov * speed, body2d.velocity.y);
        }
    }
}
