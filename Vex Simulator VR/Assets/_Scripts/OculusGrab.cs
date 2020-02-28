using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Thanks, https://circuitstream.com/blog/grab-oculus/
[RequireComponent(typeof(Collider))]
public class OculusGrab : MonoBehaviour
{

    public List<(IGrabbable grabbable, GameObject gameObject, Rigidbody rb)> inRadius;
    public (IGrabbable grabbable, GameObject gameObject, Rigidbody rb) objectInHand;

    Transform oldParent;

    public void OnTriggerEnter(Collider other)
    {
        IGrabbable grabbable = other.GetComponent<IGrabbable>();
        if (grabbable == null) return;

        inRadius.Add((grabbable, other.gameObject, other.attachedRigidbody));


    }

    public void OnTriggerExit(Collider other)
    {
        IGrabbable grabbable = other.GetComponent<IGrabbable>();
        if (grabbable == null) return;

        bool removed = inRadius.Remove((grabbable, other.gameObject, other.attachedRigidbody));
        if (removed)
        {
            if(grabbable == objectInHand.grabbable)
            {
                ReleaseObject();
            }
        }
    }

    (IGrabbable grabbable, GameObject gameObject, Rigidbody rb) NearestGrabbable()
    {
        return inRadius.Aggregate((i1, i2) =>
            Vector3.Distance(transform.position, i1.gameObject.transform.position) > Vector3.Distance(transform.position, i2.gameObject.transform.position) ? i1 : i2
        );
    }

    void Update()
    {
        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.2f && inRadius.Count > 0)
        {
            GrabObject();
        }

        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") < 0.2f && inRadius.Count > 0)
        {
            ReleaseObject();
        }
    }

    public void GrabObject()
    {
        objectInHand = NearestGrabbable();
        objectInHand.grabbable.OnGrabStart();
        oldParent = objectInHand.gameObject.transform.parent;
        objectInHand.gameObject.transform.SetParent(transform);
        objectInHand.rb.isKinematic = true;
    }

    private void ReleaseObject()
    {
        objectInHand.grabbable.OnGrabEnd();
        objectInHand.rb.isKinematic = false;
        objectInHand.gameObject.transform.SetParent(oldParent);
        objectInHand = (null, null, null);
    }

}