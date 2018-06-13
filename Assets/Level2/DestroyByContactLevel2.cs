﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactLevel2 : MonoBehaviour
{

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public int life = 1; //energia

	private GameControllerLevel2 gameControllerLevel2;
	private GameObject objGameController;

	private Animator damageAC;

	void Start()
	{
		try
		{
			objGameController = GameObject.FindGameObjectWithTag("GameControllerLevel2");
			if (objGameController == null)
				throw new UnityException("É necessário ter o objeto 'Game Controller' para o chefe funcionar.");

gameControllerLevel2 = objGameController.GetComponent<GameControllerLevel2>();
			if (gameControllerLevel2 == null)
				throw new UnityException("O objeto 'Game Controller' precisa ter o script 'GameController.cs'.");
		}
		catch (System.Exception ex)
		{
			if (ex.Message != null)
				Debug.Log(ex.Message);
		}

		damageAC = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		//abandona execução do script no Game Over
		if (gameControllerLevel2.IsGameOver())
			return;

		//abandonar execução do script caso colida com algo que não deve colidir
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Enemy Shot") || other.CompareTag("Boss"))
			return;

		//diminui energia
		life--;
		if (life > 0 && damageAC != null)
			damageAC.SetTrigger("Damage");

		//só gera explosão se possui uma e não tem mais energia
		if (explosion != null && life == 0)
		{
			Instantiate(explosion, transform.position, transform.rotation); //instancia explosão

			//se o objeto deste script não é um tiro de inimigo e o objeto que colidiu não é o jogador
			if (!gameObject.CompareTag("Enemy Shot") && !other.CompareTag("Player"))
			{
				//add que 1 inimigo foi destruído
gameControllerLevel2.AddEnemiesDestroyed(1);
			}
		}

		//se quem colidiu foi o jogador
		if (other.CompareTag("Player"))
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //instancia explosão do jogador
gameControllerLevel2.GameOver(false); //ativa o Game Over
		}
		else if (!gameObject.CompareTag("Enemy Shot")) //se o objeto deste script não é um tiro de inimigo
		{
gameControllerLevel2.AddShootHit(1);
		}

		//não tem mais energia
		if (life <= 0)
		{
			//add pontos se não foi o jogador que colidiu
			if (!other.CompareTag("Player"))
gameControllerLevel2.AddScore(scoreValue);

			Destroy(gameObject);

			if (CompareTag("Boss"))
			{
gameControllerLevel2.DefeatedBoss(true);
			}
		}

		Destroy(other.gameObject);
	}
}