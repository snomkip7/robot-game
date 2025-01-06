using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpBody : Body
{
	private float cooldown = 20f;
	private float timer;
	public Light halo;
	public Light back;
	public Light left;
	public Light right;
	private float empRadius = 70f;
	private bool isDone = false;
	private bool exploded = false;
	public override void init(Player player)
	{
		halo = GetComponent<Light>();
		halo.enabled = false;
		back.enabled = false;
		right.enabled = false;
		left.enabled = false;
	}

	public void FixedUpdate()
	{
		if (onCooldown)
		{
			timer -= Time.fixedDeltaTime;

			if (timer <= 0)
			{
				onCooldown = false;
				print("emp ready");
			} 
			else if (isDone)
			{
				// do be chillin
			}
			else if (timer >= 19f)
			{
				RenderSettings.haloStrength += Time.fixedDeltaTime * .3f;
				halo.intensity += Time.fixedDeltaTime * 1.1f;
				back.intensity += Time.fixedDeltaTime * 1.1f * 3;
				right.intensity += Time.fixedDeltaTime * 1.1f * 2;
				left.intensity += Time.fixedDeltaTime * 1.1f * 2;
				//print("up");
			}
			else if (timer < 19f && timer > 18.7f)
			{
				//print("exploding");
				if (!exploded)
				{
					explode();
				}
			}
			else if (timer <= 18.7f && timer > 15f)
			{
				RenderSettings.haloStrength -= Time.fixedDeltaTime * .2f;
				halo.intensity -= Time.fixedDeltaTime * .9f;
				back.intensity -= Time.fixedDeltaTime * .9f * 5;
				right.intensity -= Time.fixedDeltaTime * .9f * 5;
				left.intensity -= Time.fixedDeltaTime * .9f * 5;
				//print("down");
			}
			else
			{
				RenderSettings.haloStrength = .5f;
				halo.enabled = false;
				halo.intensity = 1.35f;
				back.enabled = false;
				back.intensity = 5f;
				right.enabled = false;
				right.intensity = 3f;
				left.enabled = false;
				left.intensity = 3f;
				isDone = true;
			}

			
		}
	}
	public override void special()
	{
		print("powering up");
		/*Collider[] hits = Physics.OverlapSphere(transform.position, empRadius);
		foreach(Collider collider in hits)
		{
			Enemy enemy = collider.gameObject.GetComponent<Enemy>();
			if(enemy != null)
			{
				enemy.stun();
			}
		}*/
		onCooldown = true;
		timer = cooldown;
		RenderSettings.haloStrength = .2f;
		halo.intensity = .35f;
		halo.enabled = true;
		back.intensity = .35f;
		back.enabled = true;
		right.intensity = .35f;
		right.enabled = true;
		left.intensity = .35f;
		left.enabled = true;
		isDone = false;
		exploded = false;
	} 

	private void explode()
	{
		print("kaboom");
		Collider[] hits = Physics.OverlapSphere(transform.position, empRadius);
		foreach (Collider collider in hits)
		{
			Enemy enemy = collider.gameObject.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.stun();
			}
		}
		exploded = true;
	}
}
