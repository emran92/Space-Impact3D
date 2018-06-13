using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContactScript : MonoBehaviour {

	//float speed = 200f;
	float damage = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider other) {

		other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
		Debug.Log("Contact");
	}


}
