using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
	public Transform target;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	public ParticleSystem explosion;

	private Rigidbody2D rb;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void FixedUpdate()
	{
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

    void OnTriggerEnter2D(Collider2D collision)
	{
        //if ( collision.tag == "Player" )
        if ( collision.name == "HitCollider" && collision.transform.parent.tag == "Player" )
        {
			collision.GetComponent<PlayerHitCollider>().SubstractLife();
			DestroyMissile();
        }
		else if ( collision.name == "Hook" )
        {
			DestroyMissile();
		}
	}

    void DestroyMissile()
    {
        if (explosion != null )
        {
			var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
			newExplosion.Play();
			Destroy(gameObject);
		}
		else
        {
			Destroy(gameObject);
		}
    }
}
