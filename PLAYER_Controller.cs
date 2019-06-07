using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_Controller : MonoBehaviour
{
//	static PLAYER_Controller instance;
//	public static PLAYER_Controller Instance { get { return instance; } }


	Animator anim;


	//public float rollForce = 20f;

	GameObject GameOver;
	public int civilians_SAVED;

	enum State{
		Idle,
		Blocking,
		Attacking,
		Hurt,
		Dead
	}
	State state;

	void Start () {
		camera = Camera.main.GetComponent<GameCamera> ();
		anim = GetComponent<Animator>();
		state = State.Idle;

		civilians_SAVED = 0;


		GameOver = GameObject.Find ("Game Over");
		GameOver.SetActive (false);
	}
	void Update () {
		//MELEE ATTACKS

		//		Extinguishing ();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Water" && col.gameObject.name != "spray") {
			currentWater += 1;
		}
	}


	// MOVE
	[Header( "MOVEMENT" )]
	public bool facingRight = true;
	public float maxSpeed = 10f;
	float groundRadius =0.2f;
	float move;
	public Transform groundcheck;
	public LayerMask whatIsGround;
	public float Resistance;
	public float slidingSpeed = 100f;
	void Move(){
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
		move = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ((move * maxSpeed)-Resistance, GetComponent<Rigidbody2D> ().velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(move));
		float speed = anim.GetFloat ("Speed");	

		if (move > 0 && !facingRight) {
			anim.SetBool ("facingRight", true);
			Flip ();
		} else if (move < 0 && facingRight) {
			anim.SetBool ("facingRight", false);
			Flip ();
		}
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}



	// SHOOT
	[Header( "SPRAY WATER" )]
	//	VARIABLES
	public GameObject spray;
	public float sprayRate = 0.1f;
	public float maxWater = 100f;
	float currentWater = 0f;
	//	FUNCTION
	void Spray(){
		if (Input.GetKey (KeyCode.W) && currentWater > 0) {
			anim.SetBool ("Shooting", true);
			spray.SetActive (true);
			currentWater -= sprayRate;
		} 
		if (Input.GetKeyUp (KeyCode.W) || currentWater <= 0) {
			anim.SetBool ("Shooting", false);
			spray.SetActive (false);
		}
		if (currentWater <= 0) {
			currentWater = 0;
		}
		if (currentWater >= maxWater) {
			currentWater = maxWater;
		}
		SimpleHealthBar.UpdateBar( "Water", currentWater, maxWater );
	}


	[Header( "COMBO SYSTEM" )]
	DealDamage playerScript;
	public float damage = 1f;
	void Melee(){
		// NOT Attacking
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			anim.SetBool ("Attacking", false);
		}

		//Light Attack
		if (Input.GetKeyDown (KeyCode.X)) {
			anim.SetBool ("Attacking", true);
			anim.Play ("X");
			damage = 1f;
		}
		//Heavy Attack
		if (Input.GetKeyDown (KeyCode.Y)) {
			anim.SetBool ("Attacking", true);
			anim.Play ("Y");
			damage = 8f;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("X")) {
			if (Input.GetKeyDown (KeyCode.X)) {
				anim.Play ("XX");
				damage = 3f;
			}
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("XX")) {
			if (Input.GetKeyDown (KeyCode.X)) {
				anim.Play ("XXX");
				damage = 5f;				
			}
		}
	}
}