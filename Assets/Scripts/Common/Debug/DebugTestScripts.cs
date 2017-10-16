using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTestScripts : SceneStartEvent {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void SceneDelayInit() {

		SceneEventManager.Instance.GameStart();

	}
}
