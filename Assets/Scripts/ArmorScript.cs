
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorScript : MonoBehaviour
{
    public bool isProtected = true;
    public PulpoController pulpoController;
    public ParticleSystem Explosion;

    SpriteRenderer sprite;
    Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        pulpoController = GameObject.Find("Pulpo").GetComponent<PulpoController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isProtected)
        {
            collider2d.enabled = false;
            sprite.color = new Color32(123, 252, 224, 143);
        }
        else
        {
            collider2d.enabled = true;
            sprite.color = new Color32(58, 58, 58, 143);
        }
    }

    private void OnDestroy() 
    {
        if (Explosion != null )
        {
            var explosion = Instantiate(Explosion, transform.position, transform.rotation);
            explosion.Play();
            //StartCoroutine(RemoveParticleSystem(explosion));
        }

        pulpoController.RemoveArmor(this);
    }


    private IEnumerator RemoveParticleSystem(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(particle.gameObject);
    }
}
