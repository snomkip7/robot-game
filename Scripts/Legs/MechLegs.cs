using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechLegs : Legs
{
	private float speed = 10; // temp
	private float movementSnapping = .95f; // for the lerp in velocity
	private float rotationSpeed = 10f; // speed of rotating to velocity
	private Rigidbody legs;
	private Player player;


	// Start is called before the first frame update
	void Start()
	{
		legs = GetComponent<Rigidbody>();
		if(!GameObject.Find("Player").TryGetComponent<Player>(out player)) // gets player
		{
			print("oh shoot legs didn't find player");
		}
	}

	/*// Update is called once per frame
	void Update()
	{
		
	}*/

	public override void move()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), legs.velocity.y, Input.GetAxis("Vertical"));
		movement.Normalize();
		movement.x *= speed;
		movement.z *= speed;

		/*Quaternion rot = cam.transform.rotation;
		Vector3 rotv3 = rot.eulerAngles;
		Quaternion newone = Quaternion.Euler(new Vector3(0, rotv3.y, 0));*/
		movement = Quaternion.Euler(new Vector3(0, player.camLastRotation.eulerAngles.y, 0)) * movement;
		//rotates movement by the camera's rotation
		legs.velocity = Vector3.Lerp(legs.velocity, movement, movementSnapping);

		//rotate to velocity point
		if (Mathf.Abs(legs.velocity.x)>.05f && Mathf.Abs(legs.velocity.z) > .05f)
		{
			Quaternion targetRotation = Quaternion.LookRotation(legs.velocity, Vector3.up);
			targetRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));
			//print(targetRotation.eulerAngles);
			legs.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		}
		

	}

    public override void jump()
    {
		return; // do it
    }

    public override void down()
    {
		return; // no effect
    }
}
