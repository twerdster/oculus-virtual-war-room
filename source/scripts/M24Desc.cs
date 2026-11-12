using UnityEngine;
using System.Collections;

public class M24Desc : MonoBehaviour 
{
	
	private bool Appear;
	private int font_size = 15;
	private int default_size;
	// Use this for initialization
	void Start () 
	{
		Appear = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		Appear = true;
	}
	void OnTriggerExit(Collider other)
	{
		Appear = false;
	}
	void OnGUI()
	{
		default_size = GUI.skin.label.fontSize;
		if(Appear == true)
		{
			if(Menu.Instance.oculus_state)
			{
				GUILayout.BeginArea(new Rect(Screen.width*20/100,Screen.height*33/100,Screen.width*20/100,Screen.height*32/100),"","box");
				GUI.skin.label.fontSize = font_size;
				GUI.color = Color.white;
				GUILayout.Label("Type: sniper rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1988-present");
				GUILayout.Label("Weight: 5.4 Kg (unloaded min weight)");
				GUILayout.Label("            7.3 Kg (loaded max weight)");
				GUILayout.Label("Length: 109.2 cm");
				GUILayout.Label("Cartridge: 7.62 NATO");
				GUILayout.Label("Rate of fire: 20 rounds/min");
				GUILayout.EndArea();
				
				GUILayout.BeginArea(new Rect(Screen.width*60/100,Screen.height*33/100,Screen.width*20/100,Screen.height*32/100),"","box");
				GUI.color = Color.white;
				GUILayout.Label("Type: sniper rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1988-present");
				GUILayout.Label("Weight: 5.4 Kg (unloaded min weight)");
				GUILayout.Label("            7.3 Kg (loaded max weight)");
				GUILayout.Label("Length: 109.2 cm");
				GUILayout.Label("Cartridge: 7.62 NATO");
				GUILayout.Label("Rate of fire: 20 rounds/min");
				GUILayout.EndArea();
				GUI.skin.label.fontSize = default_size;
			}
			else
			{
				GUILayout.BeginArea(new Rect(Screen.width-230,0,230,270),"","box");
				GUI.color = Color.white;
				GUILayout.Label("Type: sniper rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1988-present");
				GUILayout.Label("Weight: 5.4 Kg (unloaded min weight)");
				GUILayout.Label("            7.3 Kg (loaded max weight)");
				GUILayout.Label("Length: 109.2 cm");
				GUILayout.Label("Cartridge: 7.62 NATO");
				GUILayout.Label("Rate of fire: 20 rounds/min");
				GUILayout.EndArea();
			}
		}
	}
}

