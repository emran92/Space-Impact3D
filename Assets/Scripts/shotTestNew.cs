using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotTestNew : MonoBehaviour
{

	public GameObject[] bullet;
	public Transform bulletSpawn1;
	public GameObject[] bulletSpawn2;
	public GameObject[] bulletSpawn3;
	public float enemyDamage = 25f;
	//public float fireRate;
	public float delayTime = 0.5f;
	private float counter = 0;
	public float fireDelta = 0.5F;

	private float nextFire = 0.5F;
	private float myTime = 0.0F;
	void Update()
	{
		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire1"))
			FireAction();
		//Destroy(gameObject,3);
	}

	public void FireAction()
	{
		if (myTime > nextFire)
		{
			nextFire = myTime + fireDelta;

			int bulletCount = PlayerPrefs.GetInt("BulletCount");
			if (bulletCount <= 1)
			{
				Instantiate(bullet[0], transform.position, transform.rotation);
				counter = 0;
				RaycastHit hit;
				Ray ray = new Ray(transform.position, transform.forward);

				if (Physics.Raycast(ray, out hit, 100f))
				{
					if (hit.transform.tag == "Boss1")
					{
						hit.transform.GetComponent<BossHealthScript>().RemoveHealth(enemyDamage);
					}
					else
					{
						Instantiate(bulletSpawn1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
					}
				}

			}
			else if (bulletCount == 2)
			{
				for (int i = 0; i < bulletSpawn2.Length; i++)
				{
					Transform shotspawn = bulletSpawn2[i].transform;
					Instantiate(bullet[0], transform.position, transform.rotation);
					counter = 0;
					RaycastHit hit;
					Ray ray = new Ray(transform.position, transform.forward);

					if (Physics.Raycast(ray, out hit, 100f))
					{
						if (hit.transform.tag == "Boss1")
						{
							hit.transform.GetComponent<BossHealthScript>().RemoveHealth(enemyDamage);
						}
						else
						{
							Instantiate(bulletSpawn1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						}
					}
				}
			}
			else if (bulletCount >= 3)
			{
				for (int i = 0; i < bulletSpawn3.Length; i++)
				{
					Transform shotspawn = bulletSpawn3[i].transform;
					Instantiate(bullet[1], shotspawn.position, shotspawn.rotation);
					Instantiate(bullet[0], transform.position, transform.rotation);
					counter = 0;
					RaycastHit hit;
					Ray ray = new Ray(transform.position, transform.forward);

					if (Physics.Raycast(ray, out hit, 100f))
					{
						if (hit.transform.tag == "Boss1")
						{
							hit.transform.GetComponent<BossHealthScript>().RemoveHealth(enemyDamage);
						}
						else
						{
							Instantiate(bulletSpawn1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
						}
					}

				}
			}




			nextFire = nextFire - myTime;
			myTime = 0.0F;
			//int mode = PlayerPrefs.GetInt("PlayerMusic");
			//if (mode == 1) audioSource.Play();
		}	}
}
