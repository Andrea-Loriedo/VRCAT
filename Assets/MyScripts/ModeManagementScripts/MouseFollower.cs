using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToMouse();
    }

    void MoveToMouse()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = transform.position;
        newPosition.x = mousePosition.x;
        newPosition.y = mousePosition.y;
        transform.position = newPosition;
    }
}
