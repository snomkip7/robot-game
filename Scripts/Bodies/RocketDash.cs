using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDash : Body
{
	private float cooldown = 2.5f;
	private float timer;
	private Rigidbody body;
	private float dashSpeed = 500;
	private float slowdown = .2f;
	private Player player;
	private float acceleration = .3f;

	public override void init(Player player)
	{
		body = player.player;
		this.player = player;
	}

	void FixedUpdate()
	{
		if (onCooldown)
		{
			timer -= Time.fixedDeltaTime;

			if(timer > 2.35f)
			{
				body.velocity = Vector3.Lerp(body.velocity, body.transform.forward * dashSpeed, acceleration);
			}
			else
			{
				body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, slowdown);
				// update visual?
				if(timer < 2.2f)
				{
					player.canWalk = true;
					if(timer <= 0)
					{
						onCooldown = false;
						// visual?
					}
				}
			}
		}
		
	}
	public override void special()
	{
		body.velocity = Vector3.Lerp(body.velocity, body.transform.forward * dashSpeed, acceleration);
		onCooldown = true;
		timer = cooldown;
		player.canWalk = false;
		print("zoom");
	}
}
