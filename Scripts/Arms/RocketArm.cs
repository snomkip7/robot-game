using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class RocketArm : Arm
{
	public float timer;
	public float cooldown = 1;

	public override void init()
	{
		timer = cooldown;
		print("rocket arm initiated");
	}

	public void FixedUpdate()
	{
		if (attacking)
		{
			timer -= Time.fixedDeltaTime;
			if (timer <= 0)
			{
				attacking = false;
			}
		}
	}

	public override void attack()
	{
		attacking = true;
		print("boom");
		GameObject rocket = Instantiate(Resources.Load("rocket", typeof(GameObject))) as GameObject;
		Player player = FindObjectOfType<Player>();
		rocket.transform.rotation = player.transform.rotation;
		rocket.transform.position = transform.position;
		timer = cooldown;
	}
}
