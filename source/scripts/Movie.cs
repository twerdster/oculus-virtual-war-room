using UnityEngine;
using System.Collections;

public class Movie : MonoBehaviour {
    public MovieTexture movTexture;
    
	// this method called when the scene starts
	void Start() {
		// screen the movie at the object, which the script assosiated with
        renderer.material.mainTexture = movTexture;
        // play the movie
		movTexture.Play();
		// in loop
		movTexture.loop = true;
    }
}