using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetAspectRatio : MonoBehaviour
{
    public Transform screen;

    // Copied from http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.aspect = screen.lossyScale.x / screen.lossyScale.z;
    }

}
