using UnityEngine;
using System.Collections;

public class Random_Movement : MonoBehaviour {
	
	private Vector3 offset;
	public Vector3 vel = Vector3.zero;
	public float switchDirection = 1f;
	private float CurTime = 0f;
	public float resetTime = 0f;

	// Use this for initialization
	void Start () 
	{
		// initialization of the start velocity
		SetVel();
		// offset gets the object current position
		offset = transform.position;
	}
	
	//////////////////////////////////////////////////////////////
	//															//
	//  This method set the object new X & Z value randomly.    //
	//															//
	//////////////////////////////////////////////////////////////
	void SetVel()
	{
		// increase the X value  if it less than 0.4f
		if(Random.value > 0.4f) {
			vel.x = .2f * Random.value;
		}
		// decrease the X value  if it less than 0.4f
		else{
			vel.x = -.2f * Random.value;
		}
		// increase the Z value  if it less than 0.4f
		if(Random.value > 0.4f) {
			vel.z = .2f * Random.value;
		}
		// decrease the Z value  if it less than 0.4f
		else{
			vel.z = -.2f * Random.value;
		}
	}
	
	//////////////////////////////////////////////////////////////
	//															//
	//  This method determines the object direction changes     //
	//  Every once in a 20f it reset the object to it's start   //
	//  point.													//
	//															//
	//////////////////////////////////////////////////////////////
	void Update () 
	{
		// count the time since the activaton of the method 
		resetTime += Time.deltaTime;
		
		// if it greater than 20f 
		if(resetTime > 20f)
		{
			// reset the position and the time counter
			transform.position = offset;
			resetTime = 0f;
		}
		
		// if the time is less than the change direction time
		if(CurTime < switchDirection)
		{
			// continue raising the time couner
			CurTime += Time.deltaTime;
		}
		// if it is greater than the change direction time
		else{
			// determine a random velocity to X & Z axis
			SetVel();
			// there is 50% chance
			if(Random.value > .5)
			{
				// the switchdirection time is increased in a randome value
				switchDirection += Random.value;
			}
			else{
				// the switchdirection time is dencreased in a randome value
				switchDirection -= Random.value;
			}
			// the switchDirection must be bigger than 1 sec
			if(switchDirection < 1) {
				switchDirection = 1 + Random.value;
			}
			// and we reset the time counter
			CurTime = 0;
		}
	}
	
	// happens every fixed time (about 0.02 sec)
	void FixedUpdate()
	{
		// set the object volocity to the chosen X & Z velocities
		gameObject.rigidbody.velocity = vel;
	}
}
