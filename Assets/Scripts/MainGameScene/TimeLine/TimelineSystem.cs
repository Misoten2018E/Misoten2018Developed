using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSystem : SceneStartEvent , IFGameEndEvent{

	public override void SceneMyInit() {
		base.SceneMyInit();
	}

	public override void SceneOtherInit() {
		base.SceneOtherInit();
	}

	public void GameEnd() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}



public class TimelineEventBase {

}