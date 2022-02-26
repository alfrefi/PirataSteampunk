using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorScript : MonoBehaviour
{
    public bool isProtected = true;

    SpriteRenderer sprite;
    Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
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
}
