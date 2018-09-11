using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	private const string ANIMATOR_TRIGGER_START_GAME = "StartGame";
	private const string ANIMATOR_TRIGGER_GAME_OVER = "GameOver";
	private const string ANIMATOR_TRIGGER_IDLE = "Idle";
	private const string ANIMATOR_TRIGGER_EXIT = "Exit";
	
	[SerializeField] private Animator _animator;
	private bool _enabled;

	private void Start ()
	{
		GameEventManager.GameStart += OnGameStart;
		GameEventManager.GameOver += OnGameOver;
		
		_animator.SetTrigger(ANIMATOR_TRIGGER_START_GAME);

		_enabled = true;
	}	

	private void Update()
	{
		if (!_enabled)
		{
			return;
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void OnGameStart()
	{
		_enabled = false;
		_animator.SetTrigger(ANIMATOR_TRIGGER_EXIT);
	}
	
	private void OnGameOver()
	{
		_enabled = true;
		_animator.SetTrigger(ANIMATOR_TRIGGER_GAME_OVER);
	}
}
