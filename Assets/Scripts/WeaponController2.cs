using UnityEngine;
using System.Collections;

public class WeaponController2 : MonoBehaviour
{
	public GameObject shot1;
public GameObject shot2;
	public Transform shotSpawn1;
public Transform shotSpawn2;
	public float fireRate;
	public float delay;

	void Start()
	{
		InvokeRepeating("Fire1", delay, fireRate);
        InvokeRepeating("Fire2", delay, fireRate);
	}

	void Fire1()
	{
		Instantiate(shot1, shotSpawn1.position, shotSpawn1.rotation);
		if (PlayerPrefs.GetInt("OtherMusic") == 1)
		{
			//AudioSource audioSource = shotSpawn.GetComponent<AudioSource>();
			//GetComponent<AudioSource>().Play();
		}
		//GetComponent<AudioSource>().Play();
	}

void Fire2()
{
	Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
	if (PlayerPrefs.GetInt("OtherMusic") == 1)
	{
		//AudioSource audioSource = shotSpawn.GetComponent<AudioSource>();
		//GetComponent<AudioSource>().Play();
	}
	//GetComponent<AudioSource>().Play();	}
}
