using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
		static Health instance;
		public static Health Instance { get { return instance; } }
		bool canTakeDamage = true;

		public float maxHealth = 100f;
		public float currentHealth = 0;

		public float invulnerabilityTime = 0.5f;




		Animator anim;

		public GameObject GameOver;


		void Start ()
		{
			currentHealth = maxHealth;
			anim = GetComponent<Animator>();
			SimpleHealthBar.UpdateBar( "Health", currentHealth, maxHealth );
		}

		void Update ()
		{
			SimpleHealthBar.UpdateBar( "Health", currentHealth, maxHealth );
			if( currentHealth <= 0 )
			{
				currentHealth = 0;
				anim.Play ("Death");
			}
		}
		

		public void TakeDamage ( float damage )
		{
			currentHealth -= damage;

			anim.Play ("Hurt");

			SimpleHealthBar.UpdateBar( "Health", currentHealth, maxHealth );
		}

		public void Burn ( float damage )
		{
			currentHealth -= damage;
			SimpleHealthBar.UpdateBar( "Health", currentHealth, maxHealth );
		}


		public void Death ()
		{
			//SceneManager.LoadScene("GameOver");//ints and such also work.
			//Destroy(gameObject);
		}


	}