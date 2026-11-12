using UnityEngine;
using System.Collections;
using System;

public class NetworkAnimsStates : MonoBehaviour 
{
	public Animations CurrentAnim = Animations.idle;
	public GameObject ThirdPersonPlayer;

	
	// get the current animation of the player.
	void Update () {
		ThirdPersonPlayer.animation.CrossFade(Enum.GetName(typeof(Animations), CurrentAnim));
	}
	
	// couses the player animatiin and body move at the same speed
	public void SyncAnimation(string AnimName, float Speed)
	{
		CurrentAnim = (Animations)Enum.Parse(typeof(Animations), AnimName);
		ThirdPersonPlayer.animation[CurrentAnim.ToString()].speed = Speed;
	}
}

public enum Animations
{
	idle,
	run,
	walk,
	jump_pose
}