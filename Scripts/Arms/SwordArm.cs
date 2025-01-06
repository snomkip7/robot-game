using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordArm : Arm
{
	public float timer;
	private float cooldown = 1f;
	private Animator animator;
	private BoxCollider hitbox;

	public override void init()
	{
		animator = GetComponent<Animator>();
		hitbox= GetComponent<BoxCollider>();
		timer = cooldown;
	}

	public void Update()
	{
		if (attacking)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				hitbox.enabled = false;
				attacking = false;
				timer = cooldown;
			}
			else if(timer < cooldown - 0.2f)
			{
				hitbox.enabled = true;
			} 
		}
	}
	public override void attack()
	{
		attacking = true;
		print("swoosh");
		animator.Play("armThing");
	}

	public void OnTriggerEnter(Collider other)
	{
		//other.gameObject.GetComponent<Enemy>().takeDmg();
		print("omg hit smth");
	}
}
