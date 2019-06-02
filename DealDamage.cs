using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

	Animator PLAYER_anim;
	GameObject player;
	GameObject enemy;

	float AXE_damage;

	void Start() {
		player = GameObject.Find("Player");
		PLAYER_anim = player.GetComponent<Animator>();
	}


	void OnCollisionStay2D(Collision2D coll)
	{
		GameObject thePlayer = GameObject.Find("Player");
		Fireaxe axeScript = thePlayer.GetComponent<Fireaxe>();

		AXE_damage = axeScript.damage;

		if (coll.gameObject.tag == "Enemy") {
			enemy = coll.gameObject;
			enemy.GetComponent<EnemyHealth> ().Damage (AXE_damage);
		}

		if (coll.gameObject.tag == "Boundary" && coll.gameObject.GetComponent<shiftCamera>().destroyable == true) {
			Destroy (coll.gameObject);
		}
	}
}
