using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftLerper : MonoBehaviour
{
    public enum LerpType
    {
        Position,
        Rotation
    }

    public Vector3 down;
    public Vector3 up;
    public LerpType lerpType;
    public LiftController liftController;

    void Start()
    {
        
    }

    void Update()
    {
        if(lerpType == LerpType.Position)
        {
            transform.localPosition = Vector3.Lerp(down, up, liftController.liftPosition);
        }else if(lerpType == LerpType.Rotation)
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(down), Quaternion.Euler(up), liftController.liftPosition);
        }
    }
}
