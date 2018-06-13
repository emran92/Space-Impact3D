using UnityEngine;
using System.Collections;
public class CoinCollectorOnContact : MonoBehaviour
{


	//public GameObject coinExplosion;
	public GameObject coinCollected;
	private CoinController _coinController;
	public int coinValue;


	void Start()
	{
		GameObject coinControllerObject = GameObject.FindWithTag("CoinController");

		if (coinControllerObject != null)
		{
			_coinController = coinControllerObject.GetComponent<CoinController>();

		}
		else
		{
			Debug.Log("Cannot find 'CoinController' script");
		}
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "goldCoin")
		{
			Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>(), false);
		}

		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("BulletActivator") || other.gameObject.CompareTag("ShieldActivator") || other.gameObject.CompareTag("CleanerActivator"))
		{
			return;
		}
		Instantiate(coinCollected, transform.position, transform.rotation);

		if (other.tag == "Player")
		{
			Instantiate(coinCollected, other.transform.position, other.transform.rotation);
			if (PlayerPrefs.GetInt("OtherMusic") == 1)
			{
				AudioSource audioSource = GetComponent<AudioSource>();
				audioSource.Play();
			}

			Destroy(this.gameObject);
			_coinController.AddCoin(coinValue);
		}

	}
}
