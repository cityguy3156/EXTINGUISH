using UnityEngine;
using System.Collections;
public class Spray : MonoBehaviour
{
	public GameObject spray;
	public float sprayRate = 0.1f;
	public float maxWater = 100f;
	float currentWater = 0f;

	//ANIMATOR
	Animator anim;

	void Start () {
		spray.SetActive(false);
		anim = GetComponent<Animator>();
		currentWater = maxWater;
		SimpleHealthBar.UpdateBar( "Water", currentWater, maxWater );
	}

	void Update () {
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

//		Extinguishing ();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Water" && col.gameObject.name != "spray") {
			currentWater += 1;
		}
	}


//	void Extinguishing(){
//		gameObject.GetComponent<FIRE_SCRIPT>().Extinguishing<>();
//	}


//	void FixedUpdate () {
//		SimpleHealthBar.UpdateBar( "Water", currentWater, maxWater );
//	}
}