using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

	public int bossLives;

	private bool bossDestroyed;
	public GameObject bossDestroyedPanel;
	//public Text scoreText;
	//public Text yourScoreText;
	//public Text highestScoreText;
	//public ParticleSystem explosionParticle;
	public GameObject _boss;
	private GameController _gameController;
	private int bossPlayerPrefs;
public int scoreValue;

	[SerializeField]
	public Text bossLivesText;



	void Start() { 
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

	public void UpdateBossLives()
	{
		if (bossLives < 1)
		{
			bossLives = 0;
			Destroy(GameObject.FindWithTag("BossEnemy"));
		}
		bossLivesText.text = "BOSS HEALTH: " + bossLives;
	}

	public void BossDestroyed()
	{
		bossDestroyed = true;
		bossDestroyedPanel.SetActive(true);
		PlayerPrefs.SetInt("BulletCount", 1);
		_gameController.AddScore(scoreValue);
	}

	/*public void PlayerHit()
	{
		StartCoroutine(ResetPlayer());
	}
	IEnumerator ResetPlayer()
	{
		yield return new WaitForSeconds(0.2f);
	_boss.enabled = true;
		yield return new WaitForSeconds(0.2f);
	_boss.enabled = false;
		yield return new WaitForSeconds(0.2f);
	_boss.enabled = true;
		yield return new WaitForSeconds(0.2f);
	_boss.enabled = false;
		yield return new WaitForSeconds(0.2f);
	_boss.enabled = true;
		yield return new WaitForSeconds(0.2f);
	}*/
}
