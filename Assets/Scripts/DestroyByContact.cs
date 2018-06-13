using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DestroyByContact : MonoBehaviour
{

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	//public int destroyValue;

	//private string GameMode;
	private GameController gameController;
	private ShieldController shieldController;

	//public int enemiesLeft = 3;
	//bool killedAllEnemies = false;	
	//int numberOfEnemiesLeft = 3;
	void Start()
	{
		//GameMode = PlayerPrefs.GetString("GameMode");
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}

		//enemiesLeft = 3; // or whatever;
	}

	/*void OnCollisionStay(Collider col) {

		if (col.gameObject.name == "Bolt(clone)") { 
			numberOfEnemies--;

			 Destroy(col.gameObject);


			 Destroy(gameObject);
		
		}
		if (numberOfEnemies == 0) {
			SceneManager.LoadScene("Home");
		}
	}*/
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("BulletActivator") || other.gameObject.CompareTag("ShieldActivator") || other.gameObject.CompareTag("CleanerActivator"))
		{
			return;
		}
		//Debug.Log("Collision happened 1");
		//SceneManager.LoadScene("Home");
		if (other.gameObject.CompareTag("Shield"))
			{
				Debug.Log("Shield Collected");
			//enemiesLeft = gameObject.Length;
            //HandleExplosionWithSound(explosion, transform);
			//Destroy(other.gameObject);	
			//enemiesLeft --;
			//if(enemiesLeft == 0){
				//	killedAllEnemies = true;
             	//	SceneManager.LoadScene("Home");
              //  Destroy(gameObject);
				}
			/*if (other.gameObject.name == "Bolt(Clone)")
			{

				Destroy(other.gameObject);
				numberOfEnemiesLeft--;

				if (numberOfEnemiesLeft == 0)
					SceneManager.LoadScene("Home");

				Destroy(gameObject);//This needs to be last, because if we destroy the gameObject before checking for the new level, that code might never get executed.

			}*/

			if (other.gameObject.CompareTag("Shield"))
			{
				Debug.Log("Shield Collected");
				HandleExplosionWithSound(explosion, transform);
				Destroy(gameObject);

				shieldController = other.gameObject.GetComponent<ShieldController>();
				if (shieldController.countUntillDestroy == 1)
				{
					HandleExplosionWithSound(playerExplosion, other.transform);
					Destroy(other.gameObject);
				}
				else
				{
					shieldController.countUntillDestroy -= 1;
				}
				return;
			}
			else if (other.gameObject.CompareTag("Player"))
			{
				GameObject shield = GameObject.FindWithTag("Shield");
				if (shield != null)
				{
					shieldController = shield.GetComponent<ShieldController>();
					if (shieldController.countUntillDestroy > 0)
					{
						return;
					}
				}
			}

			/*if (GameMode == "ExpertMode" && other.gameObject.CompareTag("Bolt"))
			{
				destroyValue -= 1;
				if (destroyValue > 0)
				{
					Destroy(other.gameObject);
					return;
				}
			}*/

			if (other.gameObject.CompareTag("Player"))
			{
				HandleExplosionWithSound(playerExplosion, other.transform);
				
				gameController.GameOver();
			}
			else
			{
				gameController.AddScore(scoreValue); // commented on 22 April 2018 08:45PM ; ERROR :: NullReferenceException: Object reference not set to an instance of an object
			}

			if (!other.gameObject.CompareTag("Cleaner"))
			{
				Destroy(other.gameObject);
			}

			HandleExplosionWithSound(explosion, transform);
			Destroy(gameObject);
		}

	public void HandleExplosionWithSound(GameObject explosion, Transform transform)
	{
		GameObject explosionObject = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		if (PlayerPrefs.GetInt("OtherMusic") == 1)
		{
			AudioSource audioSource = explosionObject.GetComponent<AudioSource>();
			audioSource.Play();
		}
	}
}
