using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRagdoll : MonoBehaviour
{
	public float pushforce = 10.0f;

	private Rigidbody[] bodies;

	private bool dead = false;

	private void Start()
	{
		bodies = GetComponentsInChildren<Rigidbody>();

		foreach (Rigidbody body in bodies)
			body.isKinematic = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !dead)
		{
			dead = true;

			Collider col = GetComponent<Collider>();

			Vector3 direction = other.bounds.center - col.bounds.center;
			direction.Normalize();

			foreach (Rigidbody body in bodies)
			{
				body.isKinematic = false;

				body.AddForce(-direction * pushforce, ForceMode.Impulse);
			}
		}
	}
}
