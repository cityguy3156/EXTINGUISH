using UnityEngine;
using System.Collections;

public class Movement: MonoBehaviour {

	//MOVEMENT VARIABLES
	public bool facingRight = true;
	public float maxSpeed = 10f;
	float groundRadius =0.2f;


	float playerHealth;

	GameCamera camera;

	//JUMPING VARIABLES
	public bool grounded = true;
	public bool blocking = true;

	enum State{
		Idle,
		Blocking,
		Attacking,
		Hurt,
		Dead
	}
	State state;

	GameObject GameOver;

	public float jumpForce = 700f;
	public float rollForce = 20f;

	float move;

	public Transform groundcheck;
	public LayerMask whatIsGround;

	bool canMove;
	//	//STATUS EFFECTS
	//	bool nearFire = false;
	//	bool inMud = false;
	//	bool onIce = false;
	//	public LayerMask NearFire;
	//	public LayerMask InMud;
	//	public LayerMask OnIce;

	//ANIMATOR
	Animator anim;



	public int civilians_SAVED;

	public float Resistance;
	public float slidingSpeed = 100f;

	// Use this for initialization
	void Start () {
		camera = Camera.main.GetComponent<GameCamera> ();
		anim = GetComponent<Animator>();
		state = State.Idle;

		civilians_SAVED = 0;

		canMove = true;

		GameOver = GameObject.Find ("Game Over");
		GameOver.SetActive (false);
	}

	void Update () 
	{
//		//float FLOOR_Resistance = boss_jar.resistance;
		grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsGround);


		playerHealth = GetComponent<Health> ().currentHealth;

		if (playerHealth <= 0) {
			state = State.Dead;
		}


//		Move ();
//		Jump ();
//		Blocking ();
		//DodgeRoll ();
		switch (state) {
			case State.Idle:
				Move ();
				//Jump ();
				camera.cameraState = GameCamera.CameraState.follow;

				break;

			case State.Attacking:
				camera.cameraState = GameCamera.CameraState.hilight;
				break;

			case State.Dead:
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Civilian")
		{
			StartCoroutine (camera.DramaticZoom());
			civilians_SAVED += 1;
			coll.gameObject.tag = "Untagged";
		}
	}


	void Move(){
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
		//MOVEMENT
		if (canMove == true)
			move = Input.GetAxis ("Horizontal");
		else
			move = 0;
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

	void Jump(){

		//float FLOOR_Resistance = boss_jar.resistance;

		grounded = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsGround);

		anim.SetBool("Ground", grounded);
		//JUMP
		if(grounded && Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
	}


	void Blocking(){
		// BLOCK
		if (Input.GetKey (KeyCode.B)) {
			anim.SetBool ("Blocking", true);
			canMove = false;
		} else {
			anim.SetBool ("Blocking", false);
			canMove = true;
		}	

		move = Input.GetAxis ("Horizontal");

		if (move > 0 && !facingRight) {
			anim.SetBool ("facingRight", true);
			Flip ();
		} else if (move < 0 && facingRight) {
			anim.SetBool ("facingRight", false);
			Flip ();
		}


		if (move != 0) {
			anim.SetFloat("Speed", Mathf.Abs(move));
		}
	}
//		
	void DodgeRoll(){
		slidingSpeed = 100f;
		while(slidingSpeed > 0)
		{
			transform.position += new Vector3 (transform.localScale.x, 0) * Time.deltaTime; 
			slidingSpeed--;
		}
	}
//	void DodgeRollSliding()
//	{
//
//		if (slidingSpeed > 0)
//			transform.position += new Vector3 (transform.localScale.x, 0) * Time.deltaTime; 
//			slidingSpeed -= 1;
//		//GetComponent<Rigidbody2D>().AddForce(new Vector2(slidingSpeed, 0));
//
//		if (slidingSpeed <= 0) {
//			slidingSpeed = 20f;
//			state = State.Idle;
//		}
//
////		if (slidingSpeed <= 0) {
////			slidingSpeed = 20f;
////			state = State.Idle;
////		}
//	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


//	void OnCollisionEnter2D(Collision2D col){
//		if (col.gameObject.tag == "Checkpoint") {
//			UI.SetActive (true);
//		}
//	}


	IEnumerator Stun()
	{
		canMove = false;
		yield return new WaitForSeconds(3f);
		canMove = true;
	}

}
