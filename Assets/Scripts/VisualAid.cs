using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAid : MonoBehaviour
{
    public GameObject VisualAidObject;
    public LayerMask VisualAidLayerMask;
    private Collider2D col2d;

    public bool ShowCursor = true;

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
        Cursor.visible = ShowCursor;
    }

    private void Update()
    {
        var collisions = new List<Collider2D>();
        col2d.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);

        for ( int i = 0; i < collisions.Count; i++ )
        {
            if ( collisions[i].gameObject.layer == 9 )
            {
                VisualAidObject.SetActive(true);
                break;
            }
            else
            {
                VisualAidObject.SetActive(false);
            }
        }
    }
}
