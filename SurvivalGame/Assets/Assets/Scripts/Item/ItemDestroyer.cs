using UnityEngine;
using System.Collections;

public class ItemDestroyer : MonoBehaviour {

	public float destroyTime;

	void Start ()
	{
		Destroy (gameObject, destroyTime);
	}
}