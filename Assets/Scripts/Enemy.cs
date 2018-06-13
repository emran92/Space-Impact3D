using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Transform _myTransform;
	private float _speed = 3.0f;
	private GameController gameController;
	private ShieldController shieldController;
	//private Renderer _render;
	private GameController _gameController;
	void Start()
	{
		_myTransform = this.transform;
		//_render = GetComponent<Renderer>();
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			_gameController = gameControllerObject.GetComponent<GameController>();
		}
		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	// Update is called once per frame
	void Update()
	{
		_myTransform.Translate(Vector3.back * _speed * Time.deltaTime);

		/*if (_myTransform.position.y < -6f)
		{
			Destroy(this.gameObject);
		}*/

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_gameController.lives--;
			_gameController.UpdateLives();
			Debug.Log("Lives updated");
			_gameController.PlayerHit();
			if (_gameController.lives < 1)
			{
				Debug.Log("No lives remainning");
				_gameController.GameOver();
			}
			Destroy(this.gameObject, 1f);
		}

		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("BulletActivator") || other.gameObject.CompareTag("ShieldActivator") || other.gameObject.CompareTag("CleanerActivator"))
		{
			return;
		}
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
		/*if (other.CompareTag("Cleaner")) // !other dile MeshRenderer error ashe , just other dile 2/3 ta enenmy hit kore cleaner destroy hoe jay
		{

			Destroy(other.gameObject);
		}*/

		HandleExplosionWithSound(explosion, transform);
		Destroy(gameObject);
		_gameController.AddScore(scoreValue);

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}



	public void HandleExplosionWithSound(GameObject explosion, Transform transform)
	{
		GameObject explosionObject = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		if (PlayerPrefs.GetInt("OtherMusic") == 1)
		{
			AudioSource audioSource = explosionObject.GetComponent<AudioSource>();
			audioSource.Play();
		}	}
}
