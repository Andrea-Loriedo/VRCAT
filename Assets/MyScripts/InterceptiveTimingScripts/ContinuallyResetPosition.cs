using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuallyResetPosition : MonoBehaviour {

	/* 
	
	The rigidbody means the spheres can drift from the 
	controller positions when they lose tracking. This
	is a quick fix to fix the localposition to 0, 0, 0.

	 */

	Rigidbody rb;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		rb.velocity = (transform.parent.position - rb.position) / Time.fixedDeltaTime;
		rb.rotation = transform.parent.rotation;
	}

}
