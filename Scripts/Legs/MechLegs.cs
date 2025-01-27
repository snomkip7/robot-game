using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechLegs : Legs
{
	private float speed = 30; // temp
	private float movementSnapping = .95f; // for the lerp in velocity
	private float rotationSpeed = 10f; // speed of rotating to velocity
	public Rigidbody legs;
	public Animator animator;

	public override void init(Player player)
	{
		/*if (!GameObject.Find("Player").TryGetComponent<Player>(out player)) // gets player
		{
			print("oh shoot legs didn't find player");
		}
		else
		{
			GetComponent<HingeJoint>().connectedBody = player.GetComponent<Rigidbody>();
			print("legs connected to player");
		}*/
		legs = GetComponent<Rigidbody>();
		GetComponent<HingeJoint>().connectedBody = player.player;
		this.player = player;
		animator = GetComponent<Animator>();
	}

	/*// Update is called once per frame
	void Update()
	{
		
	}*/

	public override void move()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movement.Normalize();
		movement.x *= speed;
		movement.z *= speed;

		/*Quaternion rot = cam.transform.rotation;
		Vector3 rotv3 = rot.eulerAngles;
		Quaternion newone = Quaternion.Euler(new Vector3(0, rotv3.y, 0));*/
		movement = Quaternion.Euler(new Vector3(0, player.camLastRotation.eulerAngles.y, 0)) * movement;
		//rotates movement by the camera's rotation
		if (player.canWalk)
		{
			movement.y = -10;
			legs.velocity = Vector3.Lerp(legs.velocity, movement, movementSnapping);
			animator.enabled = false;
		}

		//rotate to velocity point
		if (Mathf.Abs(legs.velocity.x)>.05f && Mathf.Abs(legs.velocity.z) > .05f)
		{
			Quaternion targetRotation = Quaternion.LookRotation(legs.velocity, Vector3.up);
			targetRotation = Quaternion.Euler(new Vector3(90, targetRotation.eulerAngles.y, 90));
			//print(targetRotation.eulerAngles);
			legs.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
			animator.enabled = true;
			animator.Play("walking");
		}
		else
		{
			animator.enabled = false;
		}

	}

}
