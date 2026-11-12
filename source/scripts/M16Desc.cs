using UnityEngine;
using System.Collections;

public class M16Desc : MonoBehaviour 
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
				GUILayout.BeginArea(new Rect(Screen.width*20/100,Screen.height*33/100,Screen.width*20/100,Screen.height*38/100),"","box");
				GUI.skin.label.fontSize = font_size;
				GUI.color = Color.white;
				GUILayout.Label("Type: assult rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1962-present");
				GUILayout.Label("Designer: Eugene Stoner & L James Sullivan");
				GUILayout.Label("Weight: 3.26 Kg (unloaded)");
				GUILayout.Label("            4 Kg (loaded)");
				GUILayout.Label("Length: 100 cm");
				GUILayout.Label("Cartridge: 5.56 NATO");
				GUILayout.Label("Rate of fire: 45-60 rounds/min (semi-automatic)");
				GUILayout.EndArea();
				
				GUILayout.BeginArea(new Rect(Screen.width*60/100,Screen.height*33/100,Screen.width*20/100,Screen.height*38/100),"","box");
				GUI.color = Color.white;
				GUILayout.Label("Type: assult rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1962-present");
				GUILayout.Label("Designer: Eugene Stoner & L James Sullivan");
				GUILayout.Label("Weight: 3.26 Kg (unloaded)");
				GUILayout.Label("            4 Kg (loaded)");
				GUILayout.Label("Length: 100 cm");
				GUILayout.Label("Cartridge: 5.56 NATO");
				GUILayout.Label("Rate of fire: 45-60 rounds/min (semi-automatic)");
				GUILayout.EndArea();
				GUI.skin.label.fontSize = default_size;
			}
			else
			{
				GUILayout.BeginArea(new Rect(Screen.width-230,0,230,270),"","box");
				GUI.color = Color.white;
				GUILayout.Label("Type: assult rifle");
				GUILayout.Label("Place of origin: USA");
				GUILayout.Label("In service: 1962-present");
				GUILayout.Label("Designer: Eugene Stoner & L James Sullivan");
				GUILayout.Label("Weight: 3.26 Kg (unloaded)");
				GUILayout.Label("            4 Kg (loaded)");
				GUILayout.Label("Length: 100 cm");
				GUILayout.Label("Cartridge: 5.56 NATO");
				GUILayout.Label("Rate of fire: 45-60 rounds/min (semi-automatic)");
				GUILayout.EndArea();
			}
		}
	}
}
