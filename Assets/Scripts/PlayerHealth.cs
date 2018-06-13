using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public Image healthBar;
	public float max_health = 100f;
	public float cur_health = 0f;


	// Use this for initialization
	void Start () {
	cur_health = max_health;
        SetHealthBar();
	}

	public void TakeDamage(float amount) {
	cur_health -= amount;
		SetHealthBar();
	}

	public void SetHealthBar() {
		float my_health = cur_health / max_health;
		healthBar.transform.localScale = new Vector3(Mathf.Clamp (my_health, 0f, 0f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
			
	}



}
