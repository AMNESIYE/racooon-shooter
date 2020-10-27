using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float _speed = 5.0f;

	[SerializeField]
	private GameObject _animationPrefab;
	
	void Start ()
	{
		transform.position = newRandomPosition();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if (transform.position.y < -6.3f)
		{
			transform.position = newRandomPosition();
		}
	}

	Vector3 newRandomPosition()
	{
		Vector3 position = new Vector3(Random.Range(-8.0f, 8.0f), 8.8f, 0.0f);
		return position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.Damaged();
			}
			Instantiate(_animationPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else if (other.tag == "Laser")
		{
			Instantiate(_animationPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			
			if (other.transform.parent != null)
			{
				Destroy(other.transform.parent.gameObject);
			}
			Destroy(other.gameObject);
		}
	}
}
