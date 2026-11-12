using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserPlayer : MonoBehaviour {
	public Transform FirstPerson;
	public Transform ThirdPerson;
	public Transform FirstPersonCam; // mainly for oculus
	public Player MyPlayer;
	public Vector3 CurPos;
	public Quaternion CurRot;
	private float delay;
	
	
	void Start () {
		// check if it is your palyer
		if(networkView.isMine)
		{
			// rerturn your player instance
			MyPlayer = NetworkManager.GetPlayer(networkView.owner);
			MyPlayer.Manager = this;
		}
		// no need to see your avatar or the camera
		FirstPerson.gameObject.SetActive(false);
		ThirdPerson.gameObject.SetActive(false);
		// keep the instance not to be destroyed between scenes
		DontDestroyOnLoad(gameObject);
	}
	
	// the player GUI
	void OnGUI()
	{   ////////////////////////////////////
		// Define the list of players GUI //
		////////////////////////////////////
		
		// the the "show list" button is pressed
		if(Input.GetButton("Show List"))
		{
			// if we are on oculus mode
			if(Menu.Instance.oculus_state)
			{
				// define an area where the list will appear - for the left eye
				GUILayout.BeginArea(new Rect(Screen.width*20/100,Screen.height*33/100,Screen.width*20/100,Screen.height*38/100),"","box");
				// write each player name on the list 
				foreach(Player pl in NetworkManager.Instance.PlayerList)
				{
					// if it's your player -  yellow
					if(pl == NetworkManager.Instance.MyPlayer)
					{
						GUI.color = Color.yellow;
					}
					// each other name - white
					GUILayout.Label(pl.PlayerName);
					GUI.color = Color.white;
				}
				// finish the list area
				GUILayout.EndArea();
				
				// define an area where the list will appear - for the right eye  (similar to before, only with different coordinates)
				GUILayout.BeginArea(new Rect(Screen.width*60/100,Screen.height*33/100,Screen.width*20/100,Screen.height*38/100),"","box");
				foreach(Player pl in NetworkManager.Instance.PlayerList)
				{
					if(pl == NetworkManager.Instance.MyPlayer)
					{
						GUI.color = Color.yellow;
					}
					GUILayout.Label(pl.PlayerName);
					GUI.color = Color.white;
				}
				GUILayout.EndArea();
			}
			// if we in regular mode
			else
			{
				// define an area where the list will appear
				GUILayout.BeginArea(new Rect(0,0,Screen.width/8,Screen.height/4),"","box");
				foreach(Player pl in NetworkManager.Instance.PlayerList)
				{
					if(pl == NetworkManager.Instance.MyPlayer)
					{
						GUI.color = Color.yellow;
					}
					GUILayout.Label(pl.PlayerName);
					GUI.color = Color.white;
				}
				GUILayout.EndArea();
			}
		}
		if(NetworkManager.Instance.MatchStarted)
		{
			delay += Time.deltaTime;
			if(delay > 1f)
			{
				foreach(Player p in NetworkManager.Instance.PlayerList)
				{
					GUI.color = CustomValues.DefaultColor;
					Vector3 pos = Camera.main.WorldToScreenPoint(p.Manager.CurPos);
					Vector3 pos1 = p.Manager.CurPos - Camera.main.transform.position;
					if((Vector3.Dot(Camera.main.transform.forward,pos1) > 0) && (Mathf.Sqrt(Mathf.Pow(pos1.x,2) + Mathf.Pow(pos1.y,2) + Mathf.Pow(pos1.z,2)) < 20) && (!Menu.Instance.oculus_state))
					{
						GUI.Label(new Rect(pos.x, (Screen.height - pos.y) - pos.z / Camera.main.fieldOfView, 155, 155), p.PlayerName);
					}
					GUI.color = Color.white;
				}
			}
		}
	}
	
	
	[RPC]
	public void RequestPlayer(string Namee)
	{
		networkView.RPC("GiveMyPlayer", RPCMode.OthersBuffered, Namee);
	}
	
	[RPC]
	public void GiveMyPlayer(string Namee)
	{
		StartCoroutine(GivePlayer(Namee));
	}
	
	IEnumerator GivePlayer(string Namee)
	{
		while(!NetworkManager.HasPlayer(Namee))
		{
			yield return new WaitForEndOfFrame();
		}
		MyPlayer = NetworkManager.GetPlayer(Namee);
		MyPlayer.Manager = this;
	}
	
	[RPC]
	// Define the spawn points
	void Spawn()
	{
		// if my player has already created
		MyPlayer.IsAlive = true;
		// an if it is my internet player
		if(networkView.isMine)
		{
			// set my camera active (visible)
			FirstPerson.gameObject.SetActive(true);
			// and my avatar non-active (non-visible)
			ThirdPerson.gameObject.SetActive(false);
		}
		// if it is not my internet player
		else
		{
			// set his camera non-active (non-visible) to me
			FirstPerson.gameObject.SetActive(false);
			// and my avatar non-active (non-visible) to me
			ThirdPerson.gameObject.SetActive(true);
		}
	}
	
	///////////////////////////////////////////////////
	//  Function  which send and recive information  //
	//  about the players location it the game       //
	///////////////////////////////////////////////////
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		// if you are sending information
		if(stream.isWriting)
		{
			// send your current lacation to terver
			CurPos = FirstPersonCam.position;
			stream.Serialize(ref CurPos);
			// sent you current rotation to ther server
			CurRot = FirstPersonCam.rotation;
			stream.Serialize(ref CurRot);
			// sent you current animation to the sever
			//char Ani = (char)GetComponent<NetworkAnimsStates>().CurrentAnim;
			char Ani = (char)GetComponent<sol_anime>().CurrentAnim;
			stream.Serialize(ref Ani);
			
			// if i am the server
			if(Network.isServer)
			{
				// if the info is not mine
				if(!(networkView.isMine))
				{
					// sent it to all other players
					CurPos = ThirdPerson.position;
					stream.Serialize(ref CurPos);
					CurRot = ThirdPerson.rotation;
					stream.Serialize(ref CurRot);
				}
			}
		}
		
		// if you are receiving information
		else
		{
			// recieving the camera location of other players
			stream.Serialize(ref CurPos);
			ThirdPerson.position = CurPos;
			stream.Serialize(ref CurRot);
			ThirdPerson.rotation = CurRot;
			char Ani = (char)0;
			stream.Serialize(ref Ani);
			//GetComponent<NetworkAnimsStates>().CurrentAnim = (Animations)Ani;
			GetComponent<sol_anime>().CurrentAnim = (Animation_sol)Ani;
			// if you are not the server
			if(!(Network.isServer))
			{
				// and the information is not yours
				if(!(networkView.isMine))
				{
					// receive the information about the other players
					stream.Serialize(ref CurPos);
					ThirdPerson.position = CurPos;
					stream.Serialize(ref CurRot);
					ThirdPerson.rotation = CurRot;
				}
			}
		}
	}
}
