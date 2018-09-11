using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{
	[SerializeField] private Transform _prefab;
	[SerializeField] private int _numberOfObjects;
	[SerializeField] private float _recycleOffset;
	[SerializeField] private Vector3 _startPosition;
	[SerializeField] private Vector3 _minSize;
	[SerializeField] private Vector3 _maxSize;

	private Vector3 _nextPosition;
	private Queue<Transform> _objectQueue;
	
	private void Start ()
	{
		GameEventManager.GameStart += OnGameStart;
		GameEventManager.GameOver += OnGameOver;
		
		_objectQueue = new Queue<Transform>();

		for (var i = 0; i < _numberOfObjects; i++)
		{
			_objectQueue.Enqueue(Instantiate(
				_prefab,
				new Vector3(0f, 0f, -100f),
				Quaternion.identity));				
		}
		
		_nextPosition = _startPosition;

		for (var i = 0; i < _numberOfObjects; i++)
		{
			Recycle();
		}
	}

	private void Update()
	{
		if (_objectQueue.Peek().localPosition.x + _recycleOffset < Runner.DistanceTraveled)
		{
			Recycle();
		}
	}

	private void Recycle()
	{
		var scale = new Vector3(
			Random.Range(_minSize.x, _maxSize.x),
			Random.Range(_minSize.y, _maxSize.y),
			Random.Range(_minSize.z, _maxSize.z)
			);

		var position = _nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		var t = _objectQueue.Dequeue();
		t.localScale = scale;
		t.localPosition = position;
		_nextPosition.x += scale.x;
		_objectQueue.Enqueue(t);
	}
	
	private void OnGameStart()
	{
		_nextPosition = _startPosition;
		
		for (var i = 0; i < _numberOfObjects; i++)
		{
			Recycle();
		}

		enabled = true;
	}
	
	private void OnGameOver()
	{
		enabled = false;
	}
}
