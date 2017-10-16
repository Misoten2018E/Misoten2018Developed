using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

	// シングルトンインスタンス
	static City myInstance;
	static public City Instance {
		get {
			return myInstance;
		}
	}


	private void Awake() {

		myInstance = this;

	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}
