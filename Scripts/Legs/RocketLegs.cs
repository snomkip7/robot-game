using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketLegs : Legs
{
	public Rigidbody legs;
	private float speed = 100;
	private float verticality = 40;
	private float acceleration = .1f;
	private float gravityThreshold = 4f;
	private float gravity = -60;
	private Vector3 gravVector;
	private ParticleSystem particles;
	private bool playParticles = false;

	public override void init(Player player)
	{
		legs = GetComponent<Rigidbody>();
		GetComponent<HingeJoint>().connectedBody = player.player;
		this.player = player;
		verticality -= gravity;
		particles = GetComponentInChildren<ParticleSystem>();
		particles.Stop();
	}
	public override void move()
	{
		legs.rotation = player.transform.rotation * Quaternion.Euler(-90, 180, -90);
		playParticles = false;

		Vector3 target = Vector3.zero;
		float friction = 1;
		if (player.canWalk)
		{
			Debug.DrawLine(transform.position, transform.position - Vector3.up * gravityThreshold, Color.green);
			if (Physics.Raycast(transform.position, Vector3.down, gravityThreshold))
			{
				//print("grounded");
				friction = 1.3f;
				target += transform.forward * gravity; // gravity
			}
			else
			{
				//print("airborne");
				target += transform.right * -speed * Input.GetAxis("Vertical"); // forwards backwards
				target += transform.up * -speed * Input.GetAxis("Horizontal"); // right left
				target += transform.forward * gravity; // gravity
				if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal")!=0)
				{
					playParticles = true;
				}
			}

			if (Input.GetKey(KeyCode.Space))
			{
				target += transform.forward * verticality;
				playParticles = true;
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				target += transform.forward * -speed;
				playParticles = true;
			}

			
		}
		
		legs.velocity = Vector3.Lerp(legs.velocity, target, acceleration*friction);

		if (playParticles){
			particles.Play();
		}
		else
		{
			particles.Stop();
		}

		/*float straight = Input.GetAxis("Vertical");
		if (straight > 0)
		{
			//legs.velocity = transform.right * speed;
			legs.velocity = Vector3.Lerp(legs.velocity, new Vector3(legs.velocity.x, legs.velocity.y, speed), acceleration);
			// set x rotation to -70
		}
		else if (straight < 0)
		{
			//legs.velocity = transform.right * -speed;
			legs.velocity = Vector3.Lerp(legs.velocity, new Vector3(legs.velocity.x, legs.velocity.y, -speed), acceleration);
		}
		else
		{
			//legs.velocity = Vector3.zero;
			legs.velocity = Vector3.Lerp(legs.velocity, new Vector3(0, legs.velocity.y, 0), deceleration);
		}*/
		//print(" velocity: "+legs.velocity);
	}

}
