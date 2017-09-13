using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 1.5f;
	public GameObject[] Item;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		int spawnIndex = Random.Range (0, SpawnPoints.Length);
		int objectIndex = Random.Range (0, Item.Length);
		Instantiate (Item[objectIndex], SpawnPoints [spawnIndex].position, SpawnPoints [spawnIndex].rotation);
	}
}