using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMissile1 : MonoBehaviour {

	public Transform target;
	public GameObject shotSpawn;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		Vector3 direction = target.position - rb.position;

		direction.Normalize();

		Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);

		//rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.angularVelocity = rotateAmount * rotateSpeed;


		rb.velocity = transform.forward * speed;
	}

}
