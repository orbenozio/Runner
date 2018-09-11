using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour 
{
	public static float DistanceTraveled;

	[SerializeField] private float _acceleration;
	[SerializeField] private Vector3 _jumpVelocity;
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
		if (_touchingPlatform && Input.GetButtonDown("Jump"))
		{
			_rigidbody.AddForce(_jumpVelocity, ForceMode.VelocityChange);
			_touchingPlatform = false;
		}
		
		DistanceTraveled = transform.localPosition.x;
		
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
		DistanceTraveled = 0f;
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
}
