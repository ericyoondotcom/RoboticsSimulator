using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TurboRollers : MonoBehaviour
{
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
        bool enabled = Input.GetKey(KeyCode.Space);
#else
        bool enabled = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.5f;
#endif
        if (enabled)
        {
            foreach (Rigidbody i in objects)
            {
                i.AddForce(forceY * Time.fixedDeltaTime * transform.up);
            }
        }
    }
}
