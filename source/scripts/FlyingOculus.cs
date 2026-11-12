using UnityEngine;
using System.Collections;

public class FlyingOculus : MonoBehaviour {
	
	public OVRPlayerController con_script;
	public bool fly = false;

	
	// Update is called once per frame
	void Update () {
	// if the defined button for flying has pressed
	if(Input.GetButtonDown("Flying"))
	{
		// inverse the fly state (false->true , true -> false)
		fly = !fly;
	}
		
	// then check the state, and if true
	if(fly)
	{
		// change the gravity to zero
		con_script.GravityModifier = 0;
		// and while his y position (hight) is less than # 
		if(transform.position.y <40f)
		{
			// and change it y position by 0.5f - raise him up
			transform.position = new Vector3 (transform.position.x, transform.position.y+0.5f, transform.position.z);
			// print the position to the log (monitoring)
			Debug.Log(transform.position);
		}
	}
	else   // if fly = false
	{
		// change the gravity back and drop the player
		con_script.GravityModifier = 0.379f;
	}
	}
}
