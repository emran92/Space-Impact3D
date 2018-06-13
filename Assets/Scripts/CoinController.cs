using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{

	public GameObject coins;
	public Vector3 spawnValues;
	public int coinCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public static int coin;
	public Text coinText;

	public GameController _gameController;

	void Start()
	{
		//coin = 0;
		//UpdateCoin();


		StartCoroutine(SpawnCoins());
		coin = PlayerPrefs.GetInt("TotalCoin", coin);
	}

	void Update()
	{

		if (coin < 0)
			coin = 0;
		//coinText.text = "" + coin;

	}
	IEnumerator SpawnCoins()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < coinCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(coins, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	public void StopCoinCoroutine()
	{
		StopAllCoroutines();
		print("Stopped coin Coroutines: " + Time.time);	}


	public void AddCoin(int newCoinValue)
	{
		coin += newCoinValue;
		PlayerPrefs.SetInt("TotalCoin", coin);
		coinText.text = "COINS: " + coin;
		/*if (coin == null)
			coin = 0;*/
		//UpdateCoin();
	}

	/*void UpdateCoin()
	{
		
		if (PlayerPrefs.GetInt("TotalCoin") < coin) 
			
		PlayerPrefs.SetInt("TotalCoin", coin);
		coinText.text = "COINS: " + coin;
	}*/

}

