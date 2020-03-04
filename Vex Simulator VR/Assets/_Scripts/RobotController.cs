using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static Enums;

public class RobotController : MonoBehaviour
{
    public List<WheelInfo> wheels;
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

        foreach (WheelInfo wheelInfo in wheels)
        {
            wheelInfo.wheel.motorTorque = (wheelInfo.side == Side.Left) ? leftTorque : rightTorque;
            
        }
    }
}

[System.Serializable]
public class WheelInfo
{
    public WheelCollider wheel;
    public Side side;
    public Axle backOrFront;
}