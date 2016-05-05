using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	// Use this for initialization
	void OnGUI () {
        GUI.Box(new Rect(10, 10, 100, 90),"string");
	}
	
	// Update is called once per frame
}
