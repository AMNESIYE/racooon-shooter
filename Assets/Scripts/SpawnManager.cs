using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

	[SerializeField] private GameObject _enemy;
	[SerializeField] private GameObject[] _powerups;
	
	void Start ()
	{
		StartCoroutine(EnemySpawnRoutine());
		StartCoroutine(PowerupsSpawnRoutine());
	}

	private IEnumerator EnemySpawnRoutine()
	{
		while (true)
		{
			Instantiate(_enemy, new Vector3(Random.Range(-8.0f, 8.0f), 8.8f, 0), Quaternion.identity);
			yield return new WaitForSeconds(4.0f);
		}
	}
	
	private IEnumerator PowerupsSpawnRoutine()
	{
		while (true)
		{
			int hasard = Random.Range(0, _powerups.Length);
			Instantiate(_powerups[hasard], new Vector3(Random.Range(-8.0f, 8.0f), 8.8f, 0), Quaternion.identity);
			yield return new WaitForSeconds(7.0f);
		}
	}

}
