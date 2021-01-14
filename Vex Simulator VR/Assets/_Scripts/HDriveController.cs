using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static Enums;

public class HDriveController : MonoBehaviour
{
    public List<HDriveWheelInfo> wheels;
    public float maxMotorTorque;

    public void FixedUpdate()
    {
        float leftTorque;
        float rightTorque;

#if UNITY_EDITOR
        leftTorque = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
        rightTorque = Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
#else
        leftTorque = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
        rightTorque = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
#endif

        leftTorque *= maxMotorTorque;
        rightTorque *= maxMotorTorque;

        foreach (HDriveWheelInfo wheelInfo in wheels)
        {
            wheelInfo.wheel.motorTorque = (wheelInfo.side == Side.Left) ? leftTorque : rightTorque;
            
        }
    }
}

[System.Serializable]
public class HDriveWheelInfo
{
    public WheelCollider wheel;
    public Side side;
    public Axle backOrFront;
}