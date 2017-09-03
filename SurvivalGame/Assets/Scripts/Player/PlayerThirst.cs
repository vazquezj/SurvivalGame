using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerThirst : MonoBehaviour
    {
        public float startingThirst;
        public float currentThirst;
        public Slider thirstSlider;
        float thirst = 0.36f;
        PlayerThirst playerThirst;

        //Animator anim;
        PlayerController playerController;
        bool isDead;

        void Awake()
        {
            //anim = GetComponent<Animator>();
			playerController = GetComponent<PlayerController>();
            playerThirst = GetComponent<PlayerThirst>();
            currentThirst = startingThirst;
        }

        void Update()
        {
            if (playerThirst.currentThirst > 0)
            {
                playerThirst.TakeDamage(thirst * Time.deltaTime);
            }
			if (playerThirst.currentThirst > 100)
			{
				currentThirst = startingThirst;
			}
        }

        public void TakeDamage(float amount)
        {
            currentThirst -= amount;
            thirstSlider.value = currentThirst;
			if(currentThirst <= 0 && !isDead)
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