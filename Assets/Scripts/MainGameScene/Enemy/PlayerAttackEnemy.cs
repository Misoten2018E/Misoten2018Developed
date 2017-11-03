using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEnemy : MoveTargetEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float PlayerCheckPeriod;


	//========================================================================================
	//                                    public- override
	//========================================================================================

	// Use this for initialization
	void Start () {

		iePlCheck = PlayerCheck();
	}

	public override void InitEnemy(UsedInitData InitData) {

		//transform.position = InitData.BasePosition.position;
		//transform.rotation = InitData.BasePosition.rotation;
		//TargetIndex = 0;
		//CityTargeted = false;
	}

	// Update is called once per frame
	void Update () {
		
	}
	

	//========================================================================================
	//                                    private
	//========================================================================================

	IEnumerator iePlCheck;

	/// <summary>
	/// 一定間隔毎にプレイヤーの位置状況のチェック
	/// </summary>
	/// <returns></returns>
	IEnumerator PlayerCheck() {

		yield return null;

	}
}
