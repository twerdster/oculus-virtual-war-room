using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
	public string PlayerName;
	public string HostServerName;
	public string MatchName;
	public static NetworkManager Instance;
	public List<Player> PlayerList = new List<Player>();
	public Player MyPlayer;
	public GameObject NormalPlayer;
	public GameObject OculusPlayer;
	public bool MatchStarted;
	private float delay=0f;

	// Define the hosting sever name
	// and prevent it from destroy when you pass between scenes
	void Start () {
		HostServerName = "Hammal";
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	
	void Update () {
		PlayerName = PlayerPrefs.GetString("Player name");
		
		// start counting time from the beginning of the scene
		if(MatchStarted == true)
		{
			delay += Time.deltaTime;
		}
		
		// define the exit button and his action
		if(Input.GetButton("Exit"))
			Application.Quit();
		
		// when the game has start and the isntace was created
		// the delay use to let the players enough time to create
		if(MatchStarted == true && !MyPlayer.IsAlive && delay > 0.2)
		{
			// send all the players announcement to creat my avatar on the scene
			MyPlayer.Manager.networkView.RPC("Spawn", RPCMode.All);
			// choose a spawn point randomly from a fix range
			int SpawnIndex = Random.Range(0, LevelManager.Instance.SpawnPoints.Length - 1);
			// sync the location of the avatar on the point
			MyPlayer.Manager.FirstPerson.localPosition = LevelManager.Instance.SpawnPoints[SpawnIndex].transform.position;
			MyPlayer.Manager.FirstPerson.localRotation = LevelManager.Instance.SpawnPoints[SpawnIndex].transform.rotation;
			
		}
	}
	
	////////////////////////////////////
	// Initialization of the server   //
	////////////////////////////////////
	public void StartServer(string ServerName, int MaxPlayers)
	{
		Network.InitializeSecurity();
		Network.InitializeServer(MaxPlayers, 42137, !Network.HavePublicAddress());
		MasterServer.RegisterHost(HostServerName, ServerName);
		Debug.Log("Server Initializied");
	}
	
	///////////////////////////////////////
	// When player connect to the scene  //
	///////////////////////////////////////
	
	// announcing to the server
	void OnConnectedToServer()
	{
		networkView.RPC("Server_PlayerJoined", RPCMode.Server, PlayerName, Network.player);
	}
	
	// announcing to the other players
	void OnPlayerConnected(NetworkPlayer id)
	{
		// announce all the players that a new player joined to the scene by RPC all
		foreach(Player pl in PlayerList)
		{
			networkView.RPC("Client_PlayerJoined",id, pl.PlayerName, pl.OnlinePlayer);
		}
	}
	
	////////////////////////////////
	// When the host player join  //
	////////////////////////////////
	void OnServerInitialized()
	{
		Server_PlayerJoined(PlayerName, Network.player);
	}
	
	////////////////////////////////
	// When a player disconnects  //
	////////////////////////////////
	void OnPlayerDisconnected(NetworkPlayer id)
	{
		// announce everyone that you left (so they will delete you)
		networkView.RPC("RemovePlayer", RPCMode.All, id);
		// destroy your player
		Network.DestroyPlayerObjects(id);
		// remove yourself from the server
		Network.RemoveRPCs(id);
	}
	//////////////////////////////////
	// When the server disconnects  //
	//////////////////////////////////
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		// find all the player on the players list
		GameObject[] PlayerArr = GameObject.FindGameObjectsWithTag("Player");
		// and tdestroy them one by one
		foreach(GameObject pl in PlayerArr)
		{
			Destroy(pl);
		}
		PlayerList.Clear();
		// relode the main menu
		Application.LoadLevel("Menu");
		Destroy(gameObject);
	}
	
	
	///////////////
	// RPC calls //
	///////////////
	[RPC]
	// announce all players that a player has joined
	public void Server_PlayerJoined(string Username, NetworkPlayer id)
	{
		/*foreach(Player pl in Instance.PlayerList)//addition
		{
			if(Username == pl.PlayerName && !networkView.isMine)//addition
			{
				Network.CloseConnection(id,true);//addition
			}
		}//addition*/
		networkView.RPC("Client_PlayerJoined", RPCMode.All, Username, id); //original
	}
	
	[RPC]
	////////////////////////////////////////////////////////////
	// Functuin for each player so he create his own instance //
 	// when a player join the game 							  //
	////////////////////////////////////////////////////////////
	public void Client_PlayerJoined(string Username, NetworkPlayer id)
	{
		Player temp = new Player();
		temp.PlayerName = Username;
		temp.OnlinePlayer = id;
		PlayerList.Add(temp);
		
		// if the online user was created
		if(Network.player == id)
		{
			MyPlayer = temp;
			// if we are on oculus mode
			if(Menu.Instance.oculus_state)
			{
				// create oculus player
				GameObject LastPlayer = Network.Instantiate(OculusPlayer, new Vector3(0, 4f, 0), Quaternion.identity, 0) as GameObject;
				LastPlayer.networkView.RPC("RequestPlayer", RPCMode.AllBuffered, Username);
			}
			else
			{
				// create nornal player
				GameObject LastPlayer = Network.Instantiate(NormalPlayer, new Vector3(0, 4f, 0), Quaternion.identity, 0) as GameObject;
				LastPlayer.networkView.RPC("RequestPlayer", RPCMode.AllBuffered, Username);
			}
		}
	}
	
	[RPC]
	////////////////////////////////////////////////////////////////
	// Functuin for each player so he will remove player instance //
 	// when a player discconects from the scene the game 		  //
	////////////////////////////////////////////////////////////////
	public void RemovePlayer(NetworkPlayer id)
	{
		Player temp = new Player();
		// find the player on your instances list
		foreach(Player pl in PlayerList)
		{
			if(pl.OnlinePlayer == id)
			{
				temp = pl;
			}
		}
		// removee the player if he exist
		if(null != temp)
		{
			PlayerList.Remove(temp);
		}
	}	
	
	[RPC]
	///////////////////////////////////////////////////////////////
	// Functuin for each player wich allow him to load the scene //
	///////////////////////////////////////////////////////////////
	public void LoadLevel()
	{
		// load the scene
		Application.LoadLevel("Level");
		// change game status to true
		MatchStarted = true;
	}
	
	////////////////////////////////////////////////////////////
	// Functuin that finds online player on the players list  //
	////////////////////////////////////////////////////////////
	public static Player GetPlayer(NetworkPlayer id)
	{
		foreach(Player pl in Instance.PlayerList)
		{
			if(pl.OnlinePlayer == id)
				return pl;
		}
		return null;
	}
	
	//////////////////////////////////////////////////////////
	// Functuin that find player avatar on the players list //
	//////////////////////////////////////////////////////////
	public static Player GetPlayer(string id)
	{
		foreach(Player pl in Instance.PlayerList)
		{
			if(pl.PlayerName == id)
				return pl;
		}
		return null;
	}
	
	/////////////////////////////////////////////////
	// Functuin that check if player exist ot not  //
	/////////////////////////////////////////////////
	public static bool HasPlayer(string Namee)
	{
		foreach(Player pl in Instance.PlayerList)
		{
			if(pl.PlayerName == Namee)
				return true;
		}
		return false;
	}
}
	
[System.Serializable]
public class Player
{
	public string PlayerName;
	public NetworkPlayer OnlinePlayer;
	public UserPlayer Manager;
	public bool IsAlive;
}
