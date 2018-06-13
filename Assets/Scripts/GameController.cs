using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public int sceneIndexForRestarting;
	public bool pauseWaves = false;
	public GameObject[] hazards;
	public GameObject bulletActivator;
	public GameObject shieldActivator;
	public GameObject cleanerActivator;
	public GameObject cleanerButton;
	public GameObject[] boss1;


	public GameObject player;
	public GameObject cleaner;
	public Vector3 spawnValue;

	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int activateBullet = 100;
	public Text scoreText;
	public Text yourScoreText;
	public Text highestScoreText;


	private bool gameOver;
	private int score;
	private int upgradeBullet = 0;

	public GameObject gameOverPanel;
	public GameObject gamePausePanel;
	public GameObject pauseButton;

	private Mover mover;
	private int bulletActiveIndex = 0;
	private bool activateBulletActivator = true;
	private int shieldActiveIndex = 0;
	private bool activateShieldActivator = true;
	private int cleanerActiveIndex = 0;
	private bool activateCleanerActivator = true;


	private bool isPaused;
	private string GameMode;

	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20, 40);
	public Vector2 size = new Vector2(60, 20);
	public Texture2D emptyTex;
	public Texture2D fullTex;

	public float speedIncreaser = -0.0001f;
	private float updateSpeedBy = 0.0f;

	private Coroutine wavesCoroutine;
	private Coroutine bossCoroutine;
	private IEnumerator spawnWaveCoroutine;
	private IEnumerator spawnBossCoroutine;
	public float bossSpawn;

	public int lives = 10;

	private Renderer _player;
	private CoinController _coinController;

	[SerializeField]
	private Text _liveText;

	void Start()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			_player = playerObject.GetComponent<Renderer>();
		}
		else
		{
			Debug.Log("Cannot find 'Player' script");
		}

		GameObject coinControllerObject = GameObject.FindWithTag("CoinController");
		if (coinControllerObject != null)
		{
			_coinController = coinControllerObject.GetComponent<CoinController>();
		}
		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}

		//_player = GameObject.Find("Player").GetComponent<Renderer>();

		score = 0;
		gameOver = false;
		isPaused = false;
		UpdateScore();
		StartCoroutine(SpawnWaves());
		PlayerPrefs.SetInt("BulletCount", 1);
		GameMode = PlayerPrefs.GetString("GameMode");
		this.spawnWaveCoroutine = SpawnWaves();

		StartCoroutine(this.spawnWaveCoroutine);

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseTheGame();
		}
		if (!gameOver)
		{
			if (GameMode == "NormalMode")
			{
				updateSpeedBy += speedIncreaser;
			}
			else if (GameMode == "ExpertMode")
			{
				updateSpeedBy += speedIncreaser * 10;
			}
		}
		//StartCoroutine(SpawnBoss());
		//wavesCoroutine = StartCoroutine(SpanWaves);
	}
	/*void OnGUI()
	{
		DrawProgressBar();
	}

		public void DrawProgressBar () {
			//draw the background:
			GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
	
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
			GUI.EndGroup();
			GUI.EndGroup();
		}*/

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			int bulletCount = PlayerPrefs.GetInt("BulletCount");
			int totalCount = hazardCount * bulletCount;
			if (!activateBulletActivator && shieldActiveIndex >= totalCount)
				activateBulletActivator = true;

			if (!activateShieldActivator && shieldActiveIndex >= totalCount)
				activateShieldActivator = true;

			if (!activateCleanerActivator && cleanerActiveIndex >= totalCount)
				activateCleanerActivator = true;

			for (int i = 0; i < totalCount; i++)
			{
				if (gameOver) break;

				if (activateBulletActivator && bulletCount < 3 && upgradeBullet == Random.Range(0, 10))
				{
					if (score > bulletCount * activateBullet)
					{
						ObjectActivator(bulletActivator);
						activateBulletActivator = true; // false chilo, true dile bullecActivator ashtei thake, false dile dui powerup er por ashena 
						bulletActiveIndex = 0;
					}
					else
					{
						GenerateHazard(bulletCount);
					}
				}
				else if (activateShieldActivator && Random.Range(0, 20) == Random.Range(0, 20))
				{
					GameObject isShieldPresent = GameObject.FindWithTag("Shield");
					if (isShieldPresent == null)
					{
						ObjectActivator(shieldActivator);
						activateShieldActivator = true;
						shieldActiveIndex = 0;
					}
					else
					{
						GenerateHazard(bulletCount);
					}
				}
				else if (activateCleanerActivator && Random.Range(0, 20) == Random.Range(0, 20))
				{
					GameObject isCleanerPresent = GameObject.FindWithTag("Cleaner");
					if (isCleanerPresent == null)
					{
						ObjectActivator(cleanerActivator);
						activateCleanerActivator = true;
						cleanerActiveIndex = 0;
					}
					else
					{
						GenerateHazard(bulletCount);
					}
				}
				else
				{
					GenerateHazard(bulletCount);
				}

				if (!activateBulletActivator)
					bulletActiveIndex++;

				if (!activateShieldActivator)
					shieldActiveIndex++;

				if (!activateCleanerActivator)
					cleanerActiveIndex++;

				yield return new WaitForSeconds(spawnWait / bulletCount);
			}
			yield return new WaitForSeconds(waveWait / bulletCount);
			upgradeBullet++;
			if (upgradeBullet >= 10)
			{
				upgradeBullet = 0;
			}
			Debug.Log("Boss Incoming waited for bossSpawn");
			yield return new WaitForSeconds(bossSpawn);
			HaltSpawning();
			Debug.Log("halt successfull ?? ");
			StartCoroutine(SpawnBoss());
			StopCoroutine(this.spawnWaveCoroutine);
			Debug.Log("off to SpawnBossCoroutine() method");
			while (pauseWaves)
				yield return null;
		}
		/*startWait++;
		yield return null;*/
	}
	void HaltSpawning()
	{
		StopCoroutine(this.spawnWaveCoroutine);
	}

	public void StopAllCoroutine()
	{
		StopAllCoroutines();
		print("Stopped all Coroutines: " + Time.time);
	}

	void SpawnBossCoroutine()
	{
		this.spawnBossCoroutine = SpawnBoss();
		StartCoroutine(this.spawnBossCoroutine);
		Debug.Log("off to SpawnBoss() method");
	}

	IEnumerator SpawnBoss()
	{
		StopCoroutine(this.spawnWaveCoroutine);
		Debug.Log("I am a boss, I will destroy you");

		StopCoroutine(this.spawnWaveCoroutine);
		Debug.Log("on the SpawnBoss Coroutine");
		GameObject bossEnemy = boss1[Random.Range(0, boss1.Length)];
		Vector3 spawnPositionBoss = new Vector3(Random.Range(-spawnValue.x, spawnValue.y), spawnValue.y, 9.9f);
		Quaternion spawnRoationBoss = Quaternion.identity;
		GameObject bossObject = Instantiate(bossEnemy, spawnPositionBoss, spawnRoationBoss) as GameObject;
		if (bossObject != null)
		{
			mover = bossObject.GetComponent<Mover>();
			if (mover != null) mover.speed = mover.speed + updateSpeedBy;
		}
		yield return null;
		StopCoroutine(this.spawnWaveCoroutine);
		while (pauseWaves)
			yield return null;
	}
	void GenerateHazard(int bulletCount)
	{
		GameObject hazard = hazards[Random.Range(0, hazards.Length)];
		Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject hazardObject = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;
		if (hazardObject != null)
		{
			mover = hazardObject.GetComponent<Mover>();
			if (mover != null) mover.speed = mover.speed + updateSpeedBy;
		}
	}

	void ObjectActivator(GameObject activator)
	{
		Quaternion spawnRotation1 = Quaternion.identity;
		Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
		Instantiate(activator, spawnPosition1, spawnRotation1);
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore()
	{
		if (scoreText != null) scoreText.text = "SCORE: " + score;
		//CheckForEnemiesLeft();
		if (PlayerPrefs.GetInt("BestScore") < score)
		PlayerPrefs.SetInt("BestScore", score);
	}

	/*public void CheckForEnemiesLeft()
	{
		if (GameObject.FindWithTag("Enemy") == null)
		{
			//Do any finishing up
			SceneManager.LoadScene("Game");
		}
	}*/

	public void GameOver()
	{
		gameOver = true;
		StopAllCoroutine();
		_coinController.StopCoinCoroutine();
		pauseButton.SetActive(false);
		gameOverPanel.SetActive(true);
		gamePausePanel.SetActive(false);
		PlayerPrefs.SetInt("BulletCount", 1);

		//int bestScore = PlayerPrefs.GetInt("BestScore");
		/*if (score > bestScore)
		{
			bestScore = score;
			//if (GameMode != "ChildMode") PlayerPrefs.SetInt("BestScore", score);
		}*/

		if (PlayerPrefs.GetInt("BestScore") < score)
			PlayerPrefs.SetInt("BestScore", score);

		//highestScoreText.text = "SCORE: " + score;


		//scoreText.text = "";
		yourScoreText.text = "Your Score - " + score;
		highestScoreText.text = "Best score - " + PlayerPrefs.GetInt("BestScore");
	}

	public void UpgradeWeapon()
	{
		int bulletCount = PlayerPrefs.GetInt("BulletCount");
		bulletCount += 1;
		PlayerPrefs.SetInt("BulletCount", bulletCount);
	}

	public void ActivateCleaner()
	{
		cleanerButton.SetActive(true);
	}

	public void ReStartGame()
	{
		if (isPaused)
		{
			Time.timeScale = 1;
		}
		updateSpeedBy = 0.0f;
		//SceneManager.LoadScene("Level 1 coin test"); // change this to whatever

		SceneManager.LoadScene(sceneIndexForRestarting);

	}

	public void BackFromGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneIndexForRestarting);
	}

	public void PauseTheGame()
	{
		//UpgradeWeapon();
		pauseButton.SetActive(isPaused);
		isPaused = !isPaused;
		if (!isPaused)
		{
			Time.timeScale = 1;
		}
		else if (isPaused)
		{
			Time.timeScale = 0;
		}
		gamePausePanel.SetActive(isPaused);
	}

	public void UpdateLives()
	{
		if (lives < 1)
		{
			lives = 0;
		}
		_liveText.text = "Lives: " + lives;

	}

	public void PlayerHit()
	{
		StartCoroutine(ResetPlayer());
	}
	IEnumerator ResetPlayer()
	{
		yield return new WaitForSeconds(0.5f);
		_player.enabled = true;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = false;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = true;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = false;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = true;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = false;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = true;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = false;
		yield return new WaitForSeconds(0.5f);
		_player.enabled = true;
		yield return new WaitForSeconds(0.5f);
	}
}

