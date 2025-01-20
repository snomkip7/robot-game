using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class CarEnemy : Enemy
{
	private float health = 50;
	private Player player;
	private Vector3 targetPoint;
	private NavMeshAgent move;
	private float stunTimer = 5;
	private bool stunned = false;
	private bool hit = false;
	private bool didDmg = false;
	private float dmgTimer = 2;

	[SerializeField] private LayerMask layermask;
	public void Start()
	{
		player = FindObjectOfType<Player>();
		targetPoint = player.transform.position;
		move = GetComponent<NavMeshAgent>();
		//move.CalculatePath(targetPoint, move.path);
		//move.SetDestination(targetPoint);
	}


	public void FixedUpdate()
	{
		hit = false;
		if (stunned)
		{
			stunTimer -= Time.fixedDeltaTime;
			if (stunTimer <= 0)
			{
				stunned = false;
				move.isStopped = false;
			}
		}

		if (didDmg)
		{
			dmgTimer -= Time.fixedDeltaTime;
			if (dmgTimer <= 0)
			{
				didDmg = false;
			}
		}

		if (Mathf.Abs(player.transform.position.x - transform.position.x) > 200 && Mathf.Abs(player.transform.position.z - transform.position.z) > 200)
		{
			targetPoint = transform.position;
			//print("too far");
		}
		else if (Mathf.Abs(player.transform.position.x-transform.position.x)<12 && Mathf.Abs(player.transform.position.z - transform.position.z) < 12)
		{
			targetPoint = transform.position + transform.forward * 20;
			//print("too close");
		}
		else
		{
			//targetPoint = player.transform.position*2-transform.position;
			Ray ray = new(player.transform.position+Vector3.up*10, Vector3.down);
			RaycastHit hit = new();
			Physics.Raycast(ray, out hit, 100, layermask);
			Vector3 playerPos = hit.point;

			targetPoint = playerPos;

			if (Mathf.Abs(player.transform.position.x - transform.position.x) < 20 && Mathf.Abs(player.transform.position.z - transform.position.z) < 20)
			{
				targetPoint = playerPos + (playerPos-transform.position)*2;
			}

			
			//print("gonna getcha");
		}
		Debug.DrawLine(transform.position, targetPoint, Color.green);
		move.SetDestination(targetPoint);
		//print(Mathf.Abs(player.transform.position.x - transform.position.x) + " " + Mathf.Abs(player.transform.position.z - transform.position.z));
		//print(player.transform.position+" -> "+targetPoint);
	}

	public override void takeDmg(float amount)
	{
		if (hit)
		{
			return;
		}
		hit = true;
		health -= amount;
		print("car got smacked for " + amount + " now at "+health+" hp");
		if (health <= 0)
		{
			print("car ded");
			Destroy(gameObject);
		}
	}

	public override void stun()
	{
		print("car got stunned");
		move.isStopped = true;
		stunned = true;
		stunTimer = 10;
	}

	public void OnTriggerEnter(Collider other)
	{
		//print(other.gameObject.name);
		if (didDmg)
		{
			return;
		}
		Player hitThing = other.gameObject.GetComponent<Player>();

		if(hitThing != null)
		{
			hitThing.takeDmg(25);
			didDmg = true;
			dmgTimer = 2;
		}

		Legs hitLeg = other.gameObject.GetComponent<Legs>();
		if (hitLeg != null)
		{
			hitLeg.player.takeDmg(25);
			didDmg = true;
			dmgTimer = 2;
		}
	}
}
