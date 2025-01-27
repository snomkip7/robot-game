using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserArm : Arm
{
	public float timer;
	private float cooldown = .4f;
	//private Animator animator;

	public override void init()
	{
		//animator = GetComponent<Animator>();
		timer = cooldown;
		print("laser arm initiated");
	}

	public void Update()
	{
		if (attacking)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				attacking = false;
				timer = cooldown;
			}
		}
	}
	public override void attack()
	{
		attacking = true;
		print("pew");
		//animator.Play("armThing");
		GameObject laser = Instantiate(Resources.Load("laser", typeof(GameObject))) as GameObject;
		laser.transform.rotation = transform.rotation;
		laser.transform.position = transform.position;

	}
}
