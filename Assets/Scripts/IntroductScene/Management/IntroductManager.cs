using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductManager : MonoBehaviour {



	// シングルトンインスタンス
	static IntroductManager myInstance;
	static public IntroductManager Instance {
		get {
			return myInstance;
		}
	}


	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
