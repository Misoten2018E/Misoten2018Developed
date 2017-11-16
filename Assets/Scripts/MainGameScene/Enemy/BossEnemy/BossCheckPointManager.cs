using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPointManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private RelayPoint StartTransform;

	//========================================================================================
	//                                     public
	//========================================================================================

	/// <summary>
	/// 次のリレーポイントを返す
	/// </summary>
	/// <param name="point"></param>
	/// <param name="isClockwise"></param>
	/// <returns></returns>
	public RelayPoint GetNextRelayPoint(RelayPoint point,bool isClockwise = false) {

		// もしnullなら最初のエリアを渡す
		if (point == null) {
			return StartTransform;
		}

		int max = TransList.Count;
		for (int i = 0; i < max; i++) {

			if (point == TransList[i]) {

				int next = isClockwise ? i + 1 : i - 1;
				if (next < 0) {
					next = max - 1;
				}else if(next >= max) {
					next = 0;
				}
				return TransList[next];
			}
		}

		print("同一のものが見つからない異常値");
		return null;
	}

	/// <summary>
	/// 一周していたらtrue
	/// </summary>
	/// <param name="nextArea"></param>
	/// <returns></returns>
	public bool IsEndCheck(RelayPoint nextArea) {

		return (nextArea == StartTransform);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	void Start() {

		var c = GetComponentsInChildren<RelayPoint>();
		TransList.AddRange(c);
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	List<RelayPoint> TransList = new List<RelayPoint>();

}
