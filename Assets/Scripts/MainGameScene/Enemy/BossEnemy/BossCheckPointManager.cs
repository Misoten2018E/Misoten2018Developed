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

	/// <summary>
	/// 点から点へ移動して1週する際のかかる時間を渡すと、その時間で終えるための速度を返す
	/// </summary>
	/// <param name="TakeRoundTime"></param>
	/// <returns></returns>
	public float CalcSpeedPointToPoint(float TakeRoundTime = 60) {

		int pointNum = TransList.Count;
		float oneRootTime = TakeRoundTime / pointNum;

		var len = TransList[0].transform.position - TransList[1].transform.position;
		return (len.magnitude / oneRootTime);
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
