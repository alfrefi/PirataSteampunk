using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class CameraExtensions
{
    public static Vector3 GetWorldPositionOnPlane(this Camera camera, Vector3 screenPosition, float z = 0)
    {
        Ray ray = camera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

}
