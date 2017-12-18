using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopPositionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var poses = GetComponentsInChildren<BossPopPosition>();
		PositionList.AddRange(poses);
		AccessNum = 0;
	}

	/// <summary>
	/// 一度アクセスされる度に次のものを返す
	/// </summary>
	/// <returns></returns>
	public BossPopPosition BossPopEnemyPosition() {

		if (AccessNum >= PositionList.Count) {
			print("設定異常");
			DebugLog.log("設定異常");
			return PositionList[0];
		}

		AccessNum++;
		return PositionList[AccessNum - 1];

	}

	int AccessNum = 0;

	List<BossPopPosition> PositionList = new List<BossPopPosition>();
}
