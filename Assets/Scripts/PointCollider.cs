using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PointCollider : MonoBehaviour
{

    public UnityEvent onHit;

    Vector3 prevPos;
    int collisionLayer = 9;

    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        // frontfact
        if (Physics.Linecast(prevPos, transform.position, out hit, 1 << 9))
        {
            StartZoneController startZone = hit.rigidbody.GetComponent<StartZoneController>();
            if (startZone)
            {
                startZone.Enter();
            }
            
            InterceptCheck interceptCheck = hit.rigidbody.GetComponent<InterceptCheck>();
            if (interceptCheck)
            {
                onHit.Invoke();
                Vector3 v = (transform.position - prevPos) / Time.fixedDeltaTime;
                interceptCheck.Hit(transform.position, v);
            }
        }

        // backface
        if (Physics.Linecast(transform.position, prevPos, out hit, 1 << 9))
        {
            StartZoneController startZone = hit.rigidbody.GetComponent<StartZoneController>();
            if (startZone)
            {
                startZone.Leave();
            }

            InterceptCheck interceptCheck = hit.rigidbody.GetComponent<InterceptCheck>();
            if (interceptCheck)
            {
                Vector3 v = (transform.position - prevPos) / Time.fixedDeltaTime;
                interceptCheck.Hit(transform.position, v);
            }
        }

        prevPos = transform.position;
    }
}
