using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
	public class PlayerHunger : MonoBehaviour
	{
		public float startingHunger;
		public float currentHunger;
		public Slider hungerSlider;
		float hunger = 0.24f;
		PlayerHunger playerHunger;

		//Animator anim;                        
		PlayerController playerController;                         
		bool isDead;

		void Awake ()
		{
			//anim = GetComponent <Animator> ();
			playerController = GetComponent <PlayerController> ();
			playerHunger = GetComponent <PlayerHunger> ();
			currentHunger = startingHunger;
		}

		void Update ()
		{
			if(playerHunger.currentHunger > 0)
			{
				playerHunger.TakeDamage (hunger * Time.deltaTime);
			}
			if (playerHunger.currentHunger > 100)
			{
				currentHunger = startingHunger;
			}
		}

		public void TakeDamage (float amount)
		{
			currentHunger -= amount;
			hungerSlider.value = currentHunger;
			if(currentHunger <= 0 && !isDead)
			{
				Death ();
			}
		}

		void Death ()
		{
			isDead = true;
			//anim.SetTrigger ("Die");
			playerController.enabled = false;
		}

		/*public void RestartLevel ()
		{
			SceneManager.LoadScene (0);
		}*/
	}
}