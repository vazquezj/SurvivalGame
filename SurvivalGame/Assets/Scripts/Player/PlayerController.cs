using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CompleteProject
{
	public class PlayerController : MonoBehaviour
	{
		public float translationSpeed;
		public float rotationSpeed;
		public float jumpSpeed;
		bool onGround = true;
		//private ItemDatabase database;
		//private Inventory inventory;
		//public List<Item> inventory = new List<Item> ();

		/*public float startingHunger;
		public float currentHunger;
		PlayerHunger playerHunger;
		public float startingThirst;
		public float currentThirst;
		PlayerThirst playerThirst;*/

		/*void Awake ()
		{
			playerHunger = GetComponent <PlayerHunger> ();
			playerThirst = GetComponent<PlayerThirst> ();
		}*/

		void Start ()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		void Update ()
		{
			float translation = Input.GetAxis ("Vertical") * translationSpeed;
			float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate (rotation, 0, translation);

			RaycastHit hit;
			Vector3 physicsCenter = this.transform.position + this.GetComponent<CapsuleCollider> ().center;

			//Debug.DrawRay (physicsCenter, Vector3.down*3.1f, Color.red, 1.0f);
			if (Physics.Raycast (physicsCenter, Vector3.down, out hit, 3.1f))
			{
				if (hit.transform.gameObject.tag != "Player")
				{
					onGround = true;
				}
			}
			else
			{
				onGround = false;
			}
			//Debug.Log (onGround);

			if (Input.GetKeyDown ("space") && onGround)
			{
				this.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpSpeed);
			}

			if (Input.GetKeyDown ("escape"))
			{
				Cursor.lockState = CursorLockMode.None;
			}
		}

		/*void OnTriggerEnter (Collider other)
		{
			if (other.tag == "Food")
			{
				inventory [0] = database.items [0];
				//playerHunger.currentHunger += 30;
				Destroy (other.gameObject);
			}
			if (other.tag == "Water")
			{
				inventory [1] = database.items [1];
				//playerThirst.currentThirst += 25;
				Destroy (other.gameObject);
			}
		}*/
	}
}