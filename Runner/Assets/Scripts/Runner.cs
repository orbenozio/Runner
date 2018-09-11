using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour 
{
	public static float DistanceTraveled;
	private static int Boosts;
	
	[SerializeField] private float _acceleration;
	[SerializeField] private Vector3 _jumpVelocity;
	[SerializeField] private Vector3 _boostVelocity;
	[SerializeField] private float _gameOverY;

	private bool _touchingPlatform;
	private Rigidbody _rigidbody;
	private Vector3 _startPosition;
	private Renderer _renderer;

	private void Start()
	{
		GameEventManager.GameStart += OnGameStart;
		GameEventManager.GameOver += OnGameOver;
		
		_startPosition = transform.localPosition;
		_rigidbody = GetComponent<Rigidbody>();
		_renderer = GetComponent<Renderer>();

		_renderer.enabled = false;
		_rigidbody.isKinematic = true;
		enabled = false;		
	}

	private void Update () 
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (_touchingPlatform)
			{
				_rigidbody.AddForce(_jumpVelocity, ForceMode.VelocityChange);
				_touchingPlatform = false;
			}
			else if (Boosts > 0)
			{
				_rigidbody.AddForce(_boostVelocity, ForceMode.VelocityChange);
				Boosts--;
				HUBManager.SetBoosts(Boosts);
			}
		}
		
		DistanceTraveled = transform.localPosition.x;
		HUBManager.SetDistance(DistanceTraveled);
		
		if (transform.localPosition.y < _gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}
	}

	private void FixedUpdate()
	{
		if (_touchingPlatform)
		{
			_rigidbody.AddForce(_acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		_touchingPlatform = true;
	}

	private void OnCollisionExit(Collision other)
	{
		_touchingPlatform = false;
	}
	
	private void OnGameStart()
	{
		Boosts = 0;
		HUBManager.SetBoosts(Boosts);
		DistanceTraveled = 0f;
		HUBManager.SetDistance(DistanceTraveled);
		transform.localPosition = _startPosition;
		_renderer.enabled = true;
		_rigidbody.isKinematic = false;
		enabled = true;
	}

	private void OnGameOver()
	{
		_renderer.enabled = false;
		_rigidbody.isKinematic = true;
		enabled = false;
	}

	public static void AddBoost()
	{
		Boosts++;
		HUBManager.SetBoosts(Boosts);
	}
}
