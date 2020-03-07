using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constantly sets the rotation of an object.
/// </summary>
public class CopyRotation : MonoBehaviour
{
    public Transform relativeTo;
    public bool setX;
    public bool setY;
    public bool setZ;

    void Start()
    {

    }

    void Update()
    {
		transform.rotation = Quaternion.Euler(
            setX ? relativeTo.transform.eulerAngles.x : transform.eulerAngles.x,
            setY ? relativeTo.transform.eulerAngles.y : transform.eulerAngles.y,
            setZ ? relativeTo.transform.eulerAngles.z : transform.eulerAngles.z
        );
    }
}
