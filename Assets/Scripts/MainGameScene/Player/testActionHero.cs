using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testActionHero : testActionBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	

	//========================================================================================
	//                                    public override
	//========================================================================================

	public override void ActionCircle() {
		Action.Activate();
	}

	public override void ActionCross() {
		Action.Activate();
	}

	public override void ActionSquare() {
		ActionSuction.Activate();
	}

	public override void ActionTriangle() {
		ActionSuction.Activate();
	}

	public override void Destruct() {
		
	}

	public override void Initialize(testPlayer player) {
		myPlayer = player;
	}

	// Use this for initialization
	void Start () {

		var preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.Action);
		Action = Instantiate(preAction);
		Action.Initialize(myPlayer);

		preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.ActionSuction);
		ActionSuction = Instantiate(preAction);
		ActionSuction.Initialize(myPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//========================================================================================
	//                                    private
	//========================================================================================


	testPlayer _myPlayer;
	public testPlayer myPlayer {
		private set { _myPlayer = value; }
		get { return _myPlayer; }
	}
      

	HitSeriesofAction Action;
	HitSeriesofAction ActionSuction;
}
