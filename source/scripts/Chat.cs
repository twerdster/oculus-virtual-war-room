using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Chat : MonoBehaviour {
	public string InputSting = "";
	private bool show;
	public List<ChatMessage> Messages = new List<ChatMessage>();
	Vector2 ChatPos = new Vector2(Screen.width/3, Screen.height/3);
	
	// Update is called once per frame
	void Update () 
	{
		// if the chat button was pressed and the scene is on 
		// can't be called from inside the chat
		if(Input.GetButtonDown("Chat") && NetworkManager.Instance.MatchStarted)
		{
			// inverse the apperance status
			show = (!show);
			// and display empty line in the text box 
			InputSting = "";
		}
	}
	////////////////////
	//  The chat GUI  //
	////////////////////
	
	void OnGUI()
	{
		// in case the chat is on, and we are not in oculus state
		if(show && !Menu.Instance.oculus_state)
		{
			// refers to specific area at the screen where the chat appears
			GUI.BeginGroup(new Rect(0, Screen.height*2/3, Screen.width/3, Screen.height/3));
			// define a scrollable box and a text box
			ChatPos = GUILayout.BeginScrollView(ChatPos, "box", GUILayout.Width(Screen.width/3),GUILayout.Height(Screen.height/4));
			// for each of the players
			foreach(ChatMessage cm in Messages)
			{
				// print the message list with the name of the sender
				GUILayout.Label(cm.Sender.PlayerName + ": " + cm.Message);
			}
			// finish the chat area
			GUILayout.EndScrollView();
			
			// refer to specific box as a "input" box, from now on
			GUI.SetNextControlName("input");
			// in case Enter was pressed and there is something to send
			if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.Return) && (InputSting != "")) 
			{ 
				// send the message to all other users
				SendMessage(InputSting, NetworkManager.Instance.MyPlayer.PlayerName);
				// and position the scroller on the bottom of the "chat"
				ChatPos.y = Mathf.Infinity;
				// adn clear the "input" area
				InputSting = "";
				
			}
			
			// if the "chat" button was pressed
			// can't be called from outside the chat
			if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.LeftControl)) 
			{ 
				// inverse the appearance status
				show = (!show);
			}
			// focus at the "input" box
			GUI.FocusControl("input");
			// define the text field
			InputSting = GUI.TextField(new Rect(0,Screen.height/4,Screen.width/3,Screen.height/12), InputSting);
			GUI.EndGroup();
		}
	}
	
	////////////////////////////////////////////////////////////////
	//														   	  //
	//  This method allow all the users to see the sent message.  //
	//  sending the message to all players by RPC call.           //
	//  													      //
	////////////////////////////////////////////////////////////////
	public void SendMessage(string message, string Sender)
	{
		networkView.RPC("Client_SendMessage", RPCMode.All, message, Sender);
	}
	
	//////////////////////////////////////////////////////////////
	//															//
	//  This method send//get message from user with specific   //
	//  name and message slots convert it to a message			//
	//  and add it to the message list.							//
	//															//
	//////////////////////////////////////////////////////////////
	[RPC]
	void Client_SendMessage(string message, string Sender)
	{
		//Convert(message, Sender);
		
		ChatMessage cm = new ChatMessage(Sender, message);
		Messages.Add(cm);
	}
}


////////////////////////////////////////////////////////////////
//														   	  //
//  This method allow all the users to see the sent message.  //
//  sending the message to all players by RPC call.           //
//  													      //
////////////////////////////////////////////////////////////////
[System.Serializable]

public class ChatMessage
{
	// constructor
	public ChatMessage(string SenderName, string message)
	{
		Sender = NetworkManager.GetPlayer(SenderName);
		Message = message;
	}
	public Player Sender;
	public string Message;
}
