using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float secondsToDestroy = 2f;
    public LaserAttack laserAttack;

    [SerializeField]
    public BoxCollider2D laserCollider;
    [SerializeField]
    public SpriteRenderer spriteRenderer;

    float m_ScaleX, m_ScaleY;

    void Start()
    {
        /*Vector2 v = GetComponent<Renderer>().bounds.size; 
        
        BoxCollider2D b = new BoxCollider2D();

        //BoxCollider2D b = collider2D as BoxCollider2D;

        b.size = v;*/
        //laserCollider.size = new Vector2(spriteRenderer.size.x, laserCollider.size.y);

        /*m_ScaleX = 1.0f;
        m_ScaleY = 1.0f;

        laserCollider = GetComponent<BoxCollider2D>();
        laserCollider.size = new Vector2(m_ScaleX, m_ScaleY);*/

    }

    void Update()
    {
        StartCoroutine(DestroyInSeconds());
    }

    IEnumerator DestroyInSeconds()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        
        if ( laserAttack != null )
        {
            laserAttack.DoneLasering();
        }

        Destroy(gameObject);
    }
}
