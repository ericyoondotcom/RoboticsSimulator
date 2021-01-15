using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TurboIntakes : MonoBehaviour
{
    public float forceZ;
    public float forceY;

    List<Rigidbody> objects = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        objects.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        objects.Remove(other.attachedRigidbody);
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        bool enabled = Input.GetKey(KeyCode.LeftShift);
#else
        bool enabled = Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.5f;
#endif
        if (enabled)
        {
            foreach(Rigidbody i in objects)
            {
                i.AddForce(-forceZ * Time.fixedDeltaTime * transform.forward);
                i.AddForce(forceY * Time.fixedDeltaTime * transform.up);
            }
        }
    }
}
