using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private float lifespan = 2f;
	private float speed = 300f;
	private Rigidbody body;
	private GameObject target;
	private float targetRadius = 50;
	private float maxAngle = 90;
	private float rotationSnapping = .1f;
	private Quaternion initialRotation;

	[SerializeField]
	public LayerMask targetMask;
	[SerializeField]
	public LayerMask obstacleMask;

	//private bool foundTarget = false;
	void Start()
	{
		body = GetComponent<Rigidbody>();
		initialRotation = transform.rotation;
		body.velocity = transform.forward * speed;
	}

	void FixedUpdate()
	{
		lifespan -= Time.fixedDeltaTime;
		if (lifespan <= 0)
		{
			Destroy(gameObject);
		}

		if (target == null)
		{
			Collider[] objects = Physics.OverlapSphere(transform.position, targetRadius, targetMask);
			if (objects.Length != 0)
			{
				target = objects[0].gameObject;
				
				goToTarget();
			}
		}
		else
		{
			goToTarget();
		}

		//find next enemy & move velocity to hit it
	}

	public void goToTarget()
	{
		//print(target.name);
		Vector3 targetDirection = (target.transform.position - transform.position).normalized;
		//print(targetDirection);
		Debug.DrawLine(transform.position, transform.position + targetDirection * 20, Color.magenta);
		//print(Vector3.Angle(transform.forward, targetDirection));
		if (Vector3.Angle(transform.forward, targetDirection) < maxAngle)
		{
			//print("angle good");
			float targetDistance = Vector3.Distance(transform.position, target.transform.position);
			Ray ray = new(transform.position, targetDirection);
			RaycastHit hit = new();
			if(Physics.Raycast(ray, out hit, targetDistance, obstacleMask)) // if obstacle aim for up as well
			{
				targetDirection = (target.transform.position - transform.position + Vector3.up * 2).normalized;
				print(hit.collider.name);
			}
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection, Vector3.up), rotationSnapping);
			//transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			body.velocity = transform.forward * speed/2;
		}
		else
		{
			body.velocity = transform.forward * speed;
		}
		
	}

	public void OnTriggerEnter(Collider other)
	{
		Enemy hit = other.gameObject.GetComponent<Enemy>();
		if(hit != null)
		{
			hit.takeDmg(22);
			print("DIRECT HIT!");
		}
		else
		{
			Collider[] explosion = Physics.OverlapSphere(transform.position, 30f, targetMask);
			foreach(Collider casualty in explosion){
				Enemy casualtyScript = casualty.gameObject.GetComponent<Enemy>();
				if (casualtyScript != null)
				{
					casualtyScript.takeDmg(12);
					print("hit an enemy");
				}
			}
		}
		//smth smth particles

		Destroy(gameObject);
	}
}
