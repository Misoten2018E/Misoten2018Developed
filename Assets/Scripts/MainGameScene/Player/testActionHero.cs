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
		//Action.Activate();//初期化出来ないためコメントアウト
	}

	public override void ActionCross() {
        //Action.Activate();//初期化出来ないためコメントアウト
    }

    public override void ActionSquare() {
        //ActionSuction.Activate();//初期化出来ないためコメントアウト
    }

    public override void ActionTriangle() {
        //ActionSuction.Activate();//初期化出来ないためコメントアウト
    }

    public override void Destruct() {
		
	}

	public override void Initialize(Player player) {
		myPlayer = player;
	}

	// Use this for initialization
	void Start () {

		var preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.Action);
		Action = Instantiate(preAction);
		//Action.Initialize(myPlayer);//ベースクラスを変えたためコメントアウト

		preAction = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHit, ConstActionHitData.ActionSuction);
		ActionSuction = Instantiate(preAction);
        //ActionSuction.Initialize(myPlayer);//ベースクラスを変えたためコメントアウト
    }

    // Update is called once per frame
    void Update () {
		
	}


	//========================================================================================
	//                                    private
	//========================================================================================


	Player _myPlayer;
	public Player myPlayer {
		private set { _myPlayer = value; }
		get { return _myPlayer; }
	}
      

	HitSeriesofAction Action;
	HitSeriesofAction ActionSuction;
}
