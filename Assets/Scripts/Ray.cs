using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Ray
{
    public bool hit;
    public Vector3 point;
    public Collider other;

    public Ray(bool hit, Vector3 point, Collider other)
    {
        this.hit = hit;
        this.point = point;
        this.other = other;
    }
}

