using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Enums;
// Thanks, https://circuitstream.com/blog/grab-oculus/
[RequireComponent(typeof(Collider))]
public class OculusGrab : MonoBehaviour
{

    public Side hand;

    public List<(Grabbable grabbable, GameObject gameObject)> inRadius;
    public (Grabbable grabbable, GameObject gameObject) objectInHand;

    [System.NonSerialized]
    public Grabbable previousClosest = null;

    Transform oldParent;

    private void Start()
    {
        inRadius = new List<(Grabbable grabbable, GameObject gameObject)>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Grabbable grabbable = other.GetComponent<Grabbable>();
        if (grabbable == null) return;

        inRadius.Add((grabbable, other.gameObject));


    }

    public void GrabbableEnteredRange(Grabbable grabbable)
    {
        inRadius.Add((grabbable, grabbable.gameObject));
        print(inRadius);
    }

    public void GrabbableOutOfRange(Grabbable grabbable)
    {
        bool removed = inRadius.Remove((grabbable, grabbable.gameObject));
    }

    (Grabbable grabbable, GameObject gameObject) NearestGrabbable()
    {
        if (inRadius.Count == 0) return (null, null);
        return inRadius.Aggregate((i1, i2) =>
            Vector3.Distance(transform.position, i1.gameObject.transform.position) < Vector3.Distance(transform.position, i2.gameObject.transform.position) ? i1 : i2
        );
    }

    void Update()
    {
        float grabAxis;
#if UNITY_EDITOR
        grabAxis = Input.GetMouseButton(0) ? 1 : 0;
#else
        grabAxis = Input.GetAxis(
            hand == Side.Left ?
            "Oculus_CrossPlatform_PrimaryHandTrigger" :
            "Oculus_CrossPlatform_SecondaryHandTrigger"
        );
#endif
        if (grabAxis > 0.2f && inRadius.Count > 0)
        {
            GrabObject();
        }

        if (grabAxis < 0.2f)
        {
            ReleaseObject();
        }

        Grabbable closest = NearestGrabbable().grabbable;
        if(previousClosest != null) previousClosest.OnClosestStateChanged(false, hand);
        if(closest != null) closest.OnClosestStateChanged(closest != objectInHand.grabbable, hand);
        previousClosest = closest;
        
    }

    public void GrabObject()
    {
        if (objectInHand.gameObject != null) return;
        var nearest = NearestGrabbable();
        if(nearest.grabbable.grabber != null)
        {
            nearest.grabbable.grabber.ReleaseObject();
        }
        print("Picked up");
        objectInHand = nearest;
        objectInHand.grabbable.InternalOnGrabStart(this);
        oldParent = objectInHand.gameObject.transform.parent;
        objectInHand.gameObject.transform.SetParent(transform);
    }

    private void ReleaseObject()
    {
        if (objectInHand.grabbable == null) return;
        print("Released");
        objectInHand.grabbable.InternalOnGrabEnd(this);
        objectInHand.gameObject.transform.SetParent(oldParent);
        objectInHand = (null, null);
    }

}