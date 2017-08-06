﻿using UnityEngine;

public class GoingUp : MonoBehaviour
{

	private CharacterController thePlayer;

	// Use this for initialization
	void Start()
	{
		thePlayer = FindObjectOfType<CharacterController>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
		{
			//Debug.Log("Collision Enter");
			thePlayer.canGoUp = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
		{
			//Debug.Log("Collision Exit");
			thePlayer.canGoUp = false;
		}
	}
}