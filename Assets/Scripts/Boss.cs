using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Boss : MonoBehaviour
{
	//private Transform _myTransform;
	//private float _speed = 3.0f;
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private BossController _bossController;
	private GameController _gameController;
//private GameController gameController;
private ShieldController shieldController;
	//public Animation bossDestroyed;
	//private string bossDestroy;
	public GameObject explosionParticle;

	void Start()
	{

      
        //_myTransform = this.transform;
        _bossController = GameObject.Find("BossController").GetComponent<BossController>();
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

	void Update()
	{
		//_myTransform.Translate(Vector3.forward * _speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bolt")
		{
			_bossController.bossLives--;
            Instantiate(explosion, transform.position, transform.rotation);
			_bossController.UpdateBossLives();
			Debug.Log("Boss Lives updated");
			//_bossController.PlayerHit();
			if (_bossController.bossLives < 1)
			{
				Debug.Log("No lives remainning");
				//bossDestroyed.Play(bossDestroy);
				_bossController.BossDestroyed();
                _gameController.AddScore(scoreValue);
				//explosionParticle.Play(true);
                Instantiate(explosionParticle, transform.position, transform.rotation);
				CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
				_gameController.StopAllCoroutine();
				CancelInvoke();
				_gameController.AddScore(scoreValue);
			}
			Destroy(this.gameObject, 2000f);

		}

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

	}
}
