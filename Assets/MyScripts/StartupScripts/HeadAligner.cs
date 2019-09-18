using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HeadAligner : MonoBehaviour
{

    public Transform head;
    public SpriteRenderer circleSprite;
    public SpriteRenderer crossSprite;
	public float alignTime = 3f;
	public Color defaultColor = Color.red;
	public Color waitColor = Color.green;
    public bool isAligned { get; private set; }

	IEnumerator waitCoroutine = null;

    void Awake()
    {
        isAligned = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = head.position.x;
        newPos.y = head.position.y;
        transform.position = newPos;

        int layerMask = 1 << 8; // only layer 8
        Vector3 start = head.transform.position;
        Vector3 dir = head.transform.forward;
		Debug.DrawRay(start, dir);
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide))
        {
            crossSprite.enabled = true;
			Vector3 newLocation = hit.point;
            newLocation.z = transform.position.z;
			crossSprite.transform.position = newLocation;

			if (hit.transform.CompareTag("AlignmentCircle"))
			{
				// if not already fading
                if (waitCoroutine == null)
				{
					waitCoroutine = Wait();
					StartCoroutine(waitCoroutine);
				}
			}
			else
			{
                Halt();
            }
        }
        else
        {
			Halt();
            crossSprite.enabled = false;
        }

    }

	void Halt()
	{
		if (waitCoroutine != null)
		{
            StopCoroutine(waitCoroutine);
			waitCoroutine = null;
        }
        circleSprite.color = defaultColor;
	}

	IEnumerator Wait()
	{
        circleSprite.color = waitColor;
		yield return new WaitForSeconds(alignTime);
		isAligned = true;
	}

}
