using UnityEngine;
using System.Collections;

public class Net_Cont : MonoBehaviour 
{
	public float V;
	public float H;
	public sol_anime States;

	// Update is called once per frame
	void Update () {
		// define the certical & herizontal axis
		V = Input.GetAxis("Vertical");
		H = Input.GetAxis("Horizontal");
		
		// define which animation will be invoked 
		// according to the value of V
		if(V < 0)
		{
			States.SyncAnimation("walk_sol", 1);
		}
		else if(V > 0)
		{
			States.SyncAnimation("walk_sol", -1);
		}
		else
		{
			States.SyncAnimation("walk_sol", 0);
		}
	}
}
