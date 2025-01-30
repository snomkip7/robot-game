using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : Enemy
{
	private float shotTimer;
	private float cooldown = 2f;
	private bool canShoot = true;
	private float stunTimer = 5;
	private bool stunned = false;
	private float health = 85;
	private bool hit;
	public Player player;
	public GameObject barrel;
	public float maxAngle = 360;

	private void Start()
	{
		player = FindObjectOfType<Player>();
	}

	private void FixedUpdate()
	{
		if (!canShoot)
		{
			shotTimer -= Time.fixedDeltaTime;
			if (shotTimer <= 0)
			{
				canShoot = true;
			}
		}
		
		if (stunned)
		{
			stunTimer -= Time.fixedDeltaTime;
			if (stunTimer <= 0)
			{
				stunned = false;
			}
			return;
		}
		hit = false;

		
		Vector3 targetDirection = (player.transform.position+Vector3.up*5 - transform.position).normalized;


		barrel.transform.rotation = Quaternion.Lerp(barrel.transform.rotation, Quaternion.LookRotation(targetDirection, Vector3.up), .3f);
		//barrel.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);

		if (!canShoot)
		{
			return;
		}

		Ray ray = new(barrel.transform.position, barrel.transform.forward);
		Debug.DrawLine(ray.origin, ray.origin+ray.direction*80, Color.red);
		
		RaycastHit rayHit = new();
		if (Physics.Raycast(ray, out rayHit, 80))
		{
			//print("hit " + rayHit.collider.name);
			if (rayHit.collider.gameObject.GetComponent<Player>() != null || rayHit.collider.gameObject.GetComponent<Legs>() != null)
			{
				print("sphshoom");
				GameObject laser = Instantiate(Resources.Load("turretLaser", typeof(GameObject))) as GameObject;
				laser.transform.rotation = barrel.transform.rotation;
				laser.transform.position = barrel.transform.position;
				canShoot = false;
				shotTimer = cooldown;
			}
		}
			
		
	}

	public override void stun()
	{
		print("turret stunned");
		stunned = true;
		stunTimer = 10;
	}

	public override void takeDmg(float amount)
	{
		if (hit)
		{
			return;
		}
		hit = true;
		health -= amount;
		print("turret down by " + amount + " now at " + health + " hp");
		if (health <= 0)
		{
			print("rip turret");
			Destroy(gameObject);
		}
	}
}
