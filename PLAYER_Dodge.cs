using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_Dodge : MonoBehaviour
{
	//ANIMATOR
	Animator anim;

	public float slidingSpeed = 20f;

	public float timeStamp;
	public float coolDownPeriodInSeconds;



    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

		// BLOCK
		if (Input.GetKey (KeyCode.B)) {
			anim.SetBool ("Blocking", true);
		} else {
			anim.SetBool ("Blocking", false);
		}	


//		if (slidingSpeed > 0 && timeStamp <= Time.time) {
//			transform.position += new Vector3 (transform.localScale.x, 0) * Time.deltaTime * slidingSpeed; 
//			slidingSpeed -= 1;
//		}
//		if (anim.GetBool("Blocking")==true && speed != 0) {						
//			
//		}
    }
}
