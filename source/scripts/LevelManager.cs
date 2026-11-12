using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance;
	//public List<Transform> SpawnPoints = new List<Transform>();
	public GameObject[] SpawnPoints;
	
	// Use this for initialization 
	void Start () {
		Instance = this;
		SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
	
	}
}
