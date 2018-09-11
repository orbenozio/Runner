using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] _particleSystems;

	private	void Start ()
	{
		GameEventManager.GameStart += OnGameStart;
		GameEventManager.GameOver += OnGameOver;
		OnGameOver();
	}

	private void OnGameStart()
	{
		foreach (var ps in _particleSystems)
		{
			ps.Clear();
			var e = ps.emission;
			e.enabled = true;
		}
	}

	private void OnGameOver()
	{
		foreach (var ps in _particleSystems)
		{
			var e = ps.emission;
			e.enabled = false;
		}	
	}
}
