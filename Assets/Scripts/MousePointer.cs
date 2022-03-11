using MovementWithHook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    GameObject player;
    GameObject center;

    float maxDistance;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        center = GameObject.Find("FirePoint");
        maxDistance = player.GetComponentInChildren<GrapplingGun>().maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aimPosition;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Vector2.Distance(center.transform.position, mousePosition) <= 10) 
        {
            aimPosition = mousePosition;
        } 
        else
        {
            aimPosition = (Vector2)center.transform.position + (mousePosition - (Vector2)center.transform.position).normalized * maxDistance;
        }

        transform.position = aimPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
