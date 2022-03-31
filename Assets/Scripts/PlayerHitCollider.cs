using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    public GameObject[] lifePoints;
    public int vidaPlayer;
    public List<string> CanDamagePlayer;
    
    public float DamageDelay = 1f;
    public bool canTakeDamage = true;

    public SpriteRenderer sprite;
    private Animator animator;

    public Material normalMaterial;
    public Material damagedMaterial;
    public GameObject sparks;

    void Awake()
    {
        animator = sprite.transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lifePoints = GameObject.FindGameObjectsWithTag("LifeUnit");
        vidaPlayer = lifePoints.Length;
        if(vidaPlayer == 0)
        {
            GameManager.Instance.hasMuerto = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDamagePlayer.Contains(collision.tag))
        {
            SubstractLife();
        }
    }

    public void SubstractLife()
    {
        if ( canTakeDamage )
        {
            canTakeDamage = false;
            sprite.material = damagedMaterial;
            sparks.SetActive(true);

            Destroy(lifePoints[0]);

            Invoke("canTakeDamageAgain", DamageDelay);
        }
    }

    private void canTakeDamageAgain()
    {
        canTakeDamage = true;
        sprite.material = normalMaterial;
        sparks.SetActive(false);
    }
}
