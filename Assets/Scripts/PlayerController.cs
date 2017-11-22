using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax,zMin,zMax;
}
public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float tilt;
	public float speed;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire = 0.0F;
	private AudioSource shotAudio;

			


	void Start(){

		rb = GetComponent<Rigidbody>();
		shotAudio = GetComponent<AudioSource>();
	}

	void FixedUpdate(){

	//MOVEMENT
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal * speed,0,moveVertical * speed);

		rb.AddForce(movement);
		rb.position=new Vector3
		(
			Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax), //boundary whitin two values, which get compared to the rb value
			0.0f, // Y. No give up and down.
			Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0,0,rb.velocity.x * -tilt);
	}

	void Update(){
	//SHOOTING
		if((Input.GetButton("Fire1")||Input.GetKeyDown(KeyCode.Space))&&Time.time>nextFire){
			nextFire = Time.time + fireRate;
			GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			shotAudio.Play();
		}
		
	}
}
