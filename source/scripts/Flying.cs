using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour {
	
	public CharacterMotor con_script;
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
		// set the player fall speed to zero
		con_script.movement.maxFallSpeed = 0;
		// and while his y position (hight) is less than # 
		if(transform.position.y <40f)
		{
			// print the position to the log (monitoring)
			Debug.Log(transform.position);
			// and change it y position by 0.5f - raise him up
			transform.position = new Vector3 (transform.position.x, transform.position.y+0.5f, transform.position.z);
		}
		// and sasble his ability to control the movement keys
		con_script.canControl = false;
	}
	else // if fly = false
	{
		// drop the player down 
		con_script.movement.maxFallSpeed = 20;
		// and reurn him the control at the momement keys
		con_script.canControl = true;
	}
	}
}
