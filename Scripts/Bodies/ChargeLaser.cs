//using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLaser : Body
{
	private float cooldown = 30f;
	private float timer;
	private Rigidbody body;
	private Vector3 rayOffset = new Vector3(0, 10.45f, 0);
	private float maxLength = 100f;
	private float defaultLaserLength = 1.8f;
	public GameObject chargeLaser;
	private bool shooting = false;

	[SerializeField] private LayerMask layermask;

	public override void init(Player player)
	{
		body = player.player;
		chargeLaser = transform.GetChild(0).gameObject;
		defaultLaserLength = chargeLaser.transform.localScale.z * transform.localScale.z;
		chargeLaser.SetActive(false);
	}

	public void FixedUpdate()
	{
		chargeLaser.transform.position = transform.position;
		if (shooting)
		{
			Ray ray = new Ray(body.transform.position + rayOffset, body.transform.forward);

			RaycastHit hit = new();
			bool hitSmth = Physics.Raycast(ray, out hit, maxLength, layermask);

			float scaleValue;
			if (hitSmth)
			{
				scaleValue = hit.distance / defaultLaserLength;
			}
			else
			{
				scaleValue = maxLength;
			}

			chargeLaser.transform.localScale = new Vector3(1f, 1f, scaleValue);

			chargeLaser.transform.rotation = body.transform.rotation;
		}
		if (onCooldown)
		{
			timer -= Time.fixedDeltaTime;
			if (shooting && timer <= 25f)
			{
				shooting = false;
				chargeLaser.SetActive(false);
				body.gameObject.GetComponent<Player>().rotateSpeed = 7f;
				print("laser ended");
			}
			if (timer <= 0f)
			{
				onCooldown = false;
				print("laser ready");
			}
		}
	}

	public override void special()
	{
		shooting = true;
		onCooldown = true;
		timer = cooldown;
		body.gameObject.GetComponent<Player>().rotateSpeed = 1f;
		chargeLaser.SetActive(true);
		print("laser started");

		Ray ray = new Ray(body.transform.position + rayOffset, body.transform.forward);
		//print(ray.direction);
		//Debug.DrawRay(ray.origin, ray.direction * camDistance, Color.yellow);

		RaycastHit hit = new();
		bool hitSmth = Physics.Raycast(ray, out hit, maxLength, layermask);

		float scaleValue;
		if (hitSmth)
		{
			scaleValue = hit.distance / defaultLaserLength;
		}
		else
		{
			scaleValue = maxLength;
		}
		chargeLaser.SetActive(true);
		
		chargeLaser.transform.localScale = new Vector3(1f, 1f, scaleValue);

		chargeLaser.transform.rotation = body.transform.rotation;
	}

	public void OnTriggerEnter(Collider other)
	{
		other.gameObject.GetComponent<Enemy>().takeDmg(Time.fixedDeltaTime * 25);
		print("oh shoot it do be collidin");

	}

	public void OnTriggerStay(Collider other)
	{
		other.gameObject.GetComponent<Enemy>().takeDmg(Time.fixedDeltaTime*25);
		print("oh shoot it do be collidin");

	}
}
