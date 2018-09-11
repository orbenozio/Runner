using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
	[SerializeField] private Vector3 _offset;
	[SerializeField] private Vector3 _rotationVelocity;
	[SerializeField] private float _recycleOffset;
	[SerializeField] private float _spawnChance;

	private void Start()
	{
		GameEventManager.GameOver += OnGameOver;
		gameObject.SetActive(false);
	}

	private void Update()
	{
		if (transform.localPosition.x + _recycleOffset < Runner.DistanceTraveled)
		{
			gameObject.SetActive(false);
			return;
		}
		
		transform.Rotate(_rotationVelocity * Time.deltaTime);
	}

	public void SpawnIfAvailable(Vector3 position)
	{
		if (gameObject.activeSelf || _spawnChance <= Random.Range(0f, 100f))
		{
			return;
		}

		transform.localPosition = position + _offset;
		gameObject.SetActive(true);
	}
	
	private void OnGameOver()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		Runner.AddBoost();
		gameObject.SetActive(false);
	}
}
