using UnityEngine;
using System.Collections;

public class transparent : MonoBehaviour {
	public float trans_par = 0.5f;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
			Color color = renderer.material.color;
			color.a = trans_par;
			renderer.material.color = color;
		
	}
}
