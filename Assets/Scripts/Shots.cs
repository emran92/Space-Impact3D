using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{

	public GameObject bullet;
	public Transform bulletSpawn1;
	//public Transform bulletSpawn2;

	public float enemyDamage = 25f;
	//public float fireRate;
	public float delayTime = 0.5f;
	private float counter = 0;
	void FixedUpdate()
	{
		if (Input.GetButton("Fire1"))
		{
			Instantiate(bullet, transform.position, transform.rotation);
			//Instantiate(bullet[1], bulletSpawn1.position, bulletSpawn2.rotation);
			//GetComponent<AudioSource>().Play();
			counter = 0;


			RaycastHit hit;
			Ray ray = new Ray(transform.position, transform.forward);

			if (Physics.Raycast(ray, out hit, 100f))
			{
				if (hit.transform.tag == "BossEnemy")
				{
					hit.transform.GetComponent<BossController>().UpdateBossLives();
				}
				else
				{
					Instantiate(bulletSpawn1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
				}
			}
		}
	}
}
