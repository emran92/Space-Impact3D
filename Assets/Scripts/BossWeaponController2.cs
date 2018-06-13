﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController2 : MonoBehaviour
{

	public GameObject bossShot1;
	public GameObject bossShot2;
	public GameObject bossShot3;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
public Transform shotSpawn3;
	public float fireRate;
	public float delay;
	//public GameObject[] shotspawns2;
	//public GameObject[] shotspawns3;
	void Start()
	{
		InvokeRepeating("Fire1", delay, fireRate);
		InvokeRepeating("Fire2", delay, fireRate);
        InvokeRepeating("Fire3", delay, fireRate);

	}

	void Fire1()
	{
		Instantiate(bossShot1, shotSpawn1.position, shotSpawn1.rotation);
		//Instantiate(bossShot[1], shotSpawn2.position, shotSpawn2.rotation);
		//GetComponent<AudioSource>().Play();
	}

	void Fire2()
	{
		Instantiate(bossShot2, shotSpawn2.position, shotSpawn2.rotation);
		//Instantiate(bossShot[1], shotSpawn2.position, shotSpawn2.rotation);
		//GetComponent<AudioSource>().Play();
	}

void Fire3()
{
	Instantiate(bossShot3, shotSpawn3.position, shotSpawn3.rotation);
	//Instantiate(bossShot[1], shotSpawn2.position, shotSpawn2.rotation);
	//GetComponent<AudioSource>().Play();	}
}
