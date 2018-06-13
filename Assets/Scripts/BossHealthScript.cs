
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossHealthScript : MonoBehaviour
{

	public float health = 100.0f;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}


	}
	public void RemoveHealth(float amount)
	{

		health -= amount;
		gameController.AddScore(scoreValue);
		if (health <= 0)
		{
			gameController.GameOver();
			GameObject gameObjBoss = GameObject.FindWithTag("Boss1");
		
			Destroy(gameObjBoss);
			//SceneManager.LoadScene("Home");
			//Debug.Log("Boss destroyed");


			//gameController.BossDestroyed();
		}

	}
}
