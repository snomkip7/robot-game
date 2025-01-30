using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechWheels : Legs
{
	public Rigidbody leg;
	public WheelCollider wheel;
	public Transform wheelMesh;
	private float turnSpeed = 50f;
	private float moveSpeed = 1900f;

	public override void init(Player player)
	{
		this.player = player;
		wheel = GetComponentInChildren<WheelCollider>();
		GetComponentInChildren<HingeJoint>().connectedBody = player.player;
		wheelMesh = transform.Find("Wheel").Find("WheelModel");
		leg = GetComponentInChildren<Rigidbody>();
	}

	public override void move()
	{
		wheel.steerAngle += Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
		if(wheel.steerAngle == 360 || wheel.steerAngle == -360)
		{
			wheel.steerAngle = 0;
		}
		//print("steer angle:"+wheel.steerAngle);

		float direction = Input.GetAxis("Vertical");
		if (direction == 0)
		{
			wheel.brakeTorque = moveSpeed;
			wheel.motorTorque = 0;
		}
		else
		{
			if (wheel.rpm > 320 || wheel.rpm < -320)
			{
				wheel.motorTorque = 0;
			}
			else
			{
				wheel.motorTorque = direction * moveSpeed;
			}
			wheel.brakeTorque = 0;
		}

		//print("motor torque:" + wheel.motorTorque);
		//print(wheel.rpm);

		Vector3 pos;
		Quaternion rotation;
		wheel.GetWorldPose(out pos, out rotation);

		Debug.DrawLine(transform.position, pos, Color.blue);


		wheelMesh.transform.position = pos;
		wheelMesh.transform.rotation = rotation*Quaternion.Euler(new Vector3(90, 90, 0));
		leg.velocity = new Vector3(leg.velocity.x, -10, leg.velocity.z);
	}

}
