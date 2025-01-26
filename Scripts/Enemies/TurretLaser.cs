using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
	private float lifespan = 2f;
	private float speed = 500f;
	private Rigidbody body;
	void Start()
	{
		body = GetComponent<Rigidbody>();
		body.velocity = transform.forward * speed;
	}

	// Update is called once per frame
	void Update()
	{
		lifespan -= Time.deltaTime;
		if (lifespan <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		print("sizzle");
		if (other.gameObject.GetComponent<Player>() != null) {
			other.gameObject.GetComponent<Player>().takeDmg(15);
		} else if(other.gameObject.GetComponent<Legs>() != null)
		{
			other.gameObject.GetComponent<Legs>().player.takeDmg(15);
		}
		Destroy(gameObject);
	}
}
