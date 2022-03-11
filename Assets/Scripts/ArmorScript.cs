
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorScript : MonoBehaviour
{
    public bool isProtected = true;
    public PulpoController pulpoController;

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
        pulpoController.RemoveArmor(this);
    }
}
