using UnityEngine;
using System.Collections;

public class WeaponController1 : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	void Start()
	{
		InvokeRepeating("Fire", delay, fireRate);
	}

	void Fire()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		if (PlayerPrefs.GetInt("OtherMusic") == 1)
		{
			//AudioSource audioSource = shotSpawn.GetComponent<AudioSource>();
			GetComponent<AudioSource>().Play();
		}
		//GetComponent<AudioSource>().Play();
	}
}
