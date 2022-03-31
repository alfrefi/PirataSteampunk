using MovementWithHook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAngle : MonoBehaviour
{
    public GrapplingGun grapplingGun;
    public float duration = .4f;

    private bool rotated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( grapplingGun.grappleRope.isGrappling )
        {
            var zRotation = grapplingGun.transform.parent.rotation.eulerAngles.z;
            var eulerAngles = new Vector3(0, 0, zRotation - 90);

            transform.rotation = Quaternion.Euler(eulerAngles);
            rotated = true;
        } 
        else
        {
            if ( rotated )
            {
                StartCoroutine(RotateBack());
            }
        }
    }

    IEnumerator RotateBack()
    {
        rotated = false;

        var rotation = transform.rotation;
        var rotation0 = Quaternion.Euler(0, 0, 0);
        for ( float t = 0; t < duration; t += Time.deltaTime )
        {
            transform.rotation = Quaternion.Slerp(rotation, rotation0, t / duration);
            yield return null;
        }
    }
}
