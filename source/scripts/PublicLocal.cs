using UnityEngine;
using System.Collections;

public class PublicLocal : MonoBehaviour {
	public Vector3 Pos;
	public Quaternion Rot;
	public NetworkStates NetworkType = NetworkStates.Local;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(networkView.isMine)
		{
			Pos = transform.position;
			Rot = transform.rotation;
		}
		else
		{
			transform.position = Pos;
			transform.rotation = Rot;
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if(NetworkType == NetworkStates.Public)
		{
			if(stream.isWriting)
			{
				Pos = transform.position;
				Rot = transform.rotation;
				stream.Serialize(ref Pos);
				stream.Serialize(ref Rot);
			}
			else
			{
				stream.Serialize(ref Pos);
				stream.Serialize(ref Rot);
				transform.position = Pos;
				transform.rotation = Rot;
			}
		}
	}
}
			
public enum NetworkStates
{
	Local,
	Public
}