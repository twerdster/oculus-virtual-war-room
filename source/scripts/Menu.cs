using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public static Menu Instance;
	private string CurMenu;
	public string Name;
	public string MatchName;
	public int Players;
	public bool oculus_state = false;

	// Use this for initializing the main menu
	// to each player
	void Start () {
		Instance = this;
		CurMenu = "Main";
		Name = PlayerPrefs.GetString("Player name");
	}
	
	// change the current menu to the one named at the string
	void ToMenu(string menu)
	{
		CurMenu = menu;
	}
	
	// call the current menu function & GUI
	void OnGUI()
	{
		if("Main" == CurMenu)
			Main();
		if("Host" == CurMenu)
			Host();
		if("Lobby" == CurMenu)
			Lobby();
		if("List" == CurMenu)
			MatchList();
	}
	///////////////////////
	// The main menu GUI //
	///////////////////////
	
	private void Main()
	{
		// at the start of the game (had problems to inserted this if)
		if(Time.time > 0.1)
		{
			// define the "Host a server" button
			if(GUI.Button(new Rect(Screen.width/10, Screen.height/10, Screen.width/6, Screen.height/8), "Host Server"))
			// define "Host a server" behavior
				ToMenu("Host");
			// define a text box
			Name = GUI.TextField(new Rect(Screen.width/3, Screen.height/10, Screen.width/6, Screen.height/12), Name);
			// define a text lable
			GUI.Label(new Rect(Screen.width/3, Screen.height/20, Screen.width/6, Screen.height/8), "User's name:");
			// define the "Save" button behavior
			if(GUI.Button(new Rect(Screen.width/2, Screen.height/10, Screen.width/6, Screen.height/12), "Save name"))
				PlayerPrefs.SetString("Player name",Name);
			// define the "Server list" button
			if(GUI.Button(new Rect(Screen.width/10, Screen.height/4, Screen.width/6, Screen.height/8), "Server List"))
			// define the "Server list" button behavior
				ToMenu("List");
			// define a button that will Transfer us to coculus mode - light the flag up
			if(GUI.Button(new Rect(Screen.width/10, Screen.height*4/10, Screen.width/6, Screen.height/16), "Oculus Mode"))
				oculus_state = true;
			// define a button that will Transfer us to regular palyer mode - shut the flag down
			if(GUI.Button(new Rect(Screen.width/3, Screen.height*4/10, Screen.width/6, Screen.height/16), "Regular Mode"))
				oculus_state = false;
		}
	}
	/////////////////////////
	//  The host menu GUI  //
	/////////////////////////
	
	private void Host()
	{
		// define the "Start" button
		if(GUI.Button(new Rect(Screen.width/10, Screen.height/10, Screen.width/6, Screen.height/8), "Start"))
		{
			//check for empty string name (not a legal server name)
			if("" != MatchName)
			{
				// create the server 
				NetworkManager.Instance.StartServer(MatchName, Players);
				ToMenu("Lobby");
			}
		}
		// define the "Start" button and it's functionality
		if(GUI.Button(new Rect(Screen.width/10, Screen.height/4, Screen.width/6, Screen.height/8), "Back"))
			ToMenu("Main");
		
		MatchName = GUI.TextField(new Rect(Screen.width/3, Screen.height/10, Screen.width/6, Screen.height/12), MatchName);
		// limit number of players
		Players = Mathf.Clamp(Players, 0, 10); 
		// define a text lable
		GUI.Label(new Rect(Screen.width/3, Screen.height/20, Screen.width/6, Screen.height/8), "Server name:");
		// define a text lable
		GUI.Label(new Rect(Screen.width/3, Screen.height/5, Screen.width/6, Screen.height/12), "Max Users: " + (Players + 1).ToString());
		// define the "+" & "-" buttons and their functionality
		if(GUI.Button(new Rect(Screen.width/3, Screen.height*3/11, Screen.width/30, Screen.width/30), "-"))
				Players--;
		if(GUI.Button(new Rect(Screen.width*11/30, Screen.height*3/11, Screen.width/30, Screen.width/30), "+"))
			Players++;
	}
	/////////////////////////
	// The lobby menu GUI  //
	/////////////////////////
	
	private void Lobby()
	{
		// id the server exist and defined
		if(Network.isServer)
		{
			// define a "Start"  button
			if(GUI.Button(new Rect(Screen.width-Screen.width/6, Screen.height-Screen.height/4, Screen.width/6, Screen.height/8), "Start"))
			{
				// sent to all players a RPC call to start the game
				NetworkManager.Instance.networkView.RPC("LoadLevel", RPCMode.All);
			}
		}
		
		// define a "Back"  button
		if(GUI.Button(new Rect(Screen.width-Screen.width/6, Screen.height-Screen.height/8, Screen.width/6, Screen.height/8), "Back"))
		{
			// return to tha main menu
			ToMenu("Main");
			// disconnect from the server that created
			Network.Disconnect();
		}
		
		// define an area - where the participants names will appear
		GUILayout.BeginArea(new Rect(0,0,Screen.width/2,Screen.height),"","box");
		
		// for each one of the players
		foreach(Player pl in NetworkManager.Instance.PlayerList)
		{
			// if it's me
			if(pl == NetworkManager.Instance.MyPlayer)
			{
				//print my name in yellow
				GUI.color = Color.yellow;
			}
			// else rint the name in white
			GUILayout.Label(pl.PlayerName);
			GUI.color = Color.white;
		}
		GUILayout.EndArea();
	}
	////////////////////////////////
	// The servers list menu GUI  //
	////////////////////////////////
	
	private void MatchList()
	{
		// define the "Refresh" button
		if(GUI.Button(new Rect(Screen.width/10, Screen.height/10, Screen.width/6, Screen.height/8), "Refresh"))
			Debug.Log (NetworkManager.Instance.HostServerName);
			// ask from the master server of unity the nmes of all available servers
			MasterServer.RequestHostList(NetworkManager.Instance.HostServerName);
		
		// define the "Refresh" button
		if(GUI.Button(new Rect(Screen.width/10, Screen.height/4, Screen.width/6, Screen.height/8), "Back"))
			// Iinvoke the main menu
			ToMenu("Main");
		
		// define a label
		GUI.Label(new Rect(Screen.width*3/10, Screen.height/12, Screen.width*7/10, Screen.height), "Server list:");
		// define an area
		GUILayout.BeginArea(new Rect(Screen.width*3/10, Screen.height/8, Screen.width*7/10, Screen.height-Screen.height/8), "", "box");
		// for each server in rhe list
		foreach(HostData hd in MasterServer.PollHostList())
		{
			// write horizontal
			GUILayout.BeginHorizontal();
			// write the game name
			GUILayout.Label(hd.gameName);
			// if connect was pressed
			if(GUILayout.Button("Connect"))
			{
				// connect to the server
				Network.Connect(hd);
				// invoke the lobby menu
				ToMenu("Lobby");
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	}
	
}
