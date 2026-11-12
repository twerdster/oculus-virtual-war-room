using UnityEngine;
using System.Collections;
using System;

public class sol_anime : MonoBehaviour 
{
	public Animation_sol CurrentAnim = Animation_sol.walk_sol;
	public GameObject ThirdPersonPlayer;

	
	// get the current animation of the player.
	void Update () {
		ThirdPersonPlayer.animation.CrossFade(Enum.GetName(typeof(Animation_sol), CurrentAnim));
	}
	
	// couses the player animatiin and body move at the same speed
	public void SyncAnimation(string AnimName, float Speed)
	{
		CurrentAnim = (Animation_sol)Enum.Parse(typeof(Animation_sol), AnimName);
		ThirdPersonPlayer.animation[CurrentAnim.ToString()].speed = Speed;
	}
}

public enum Animation_sol
{
//	idle,
//	run,
	walk_sol
//	jump_pose
}