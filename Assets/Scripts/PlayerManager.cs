using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static int health = 100;
	public GameObject player;
	public GameObject playerExplosion;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		InvokeRepeating("ReduceHealth", 1, 1);	
	}

	void ReduceHealth() {
		health = health - 20;
		if (health <= 0) {
			player.GetComponent<DestroyByContact>().HandleExplosionWithSound(explosion, transform);
		
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
