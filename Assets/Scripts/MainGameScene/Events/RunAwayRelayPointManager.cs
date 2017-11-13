using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayRelayPointManager : MonoBehaviour {

	//========================================================================================
	//                                    inspector
	//========================================================================================
	
	//========================================================================================
	//                                     public
	//========================================================================================

	/// <summary>
	/// 最も近くのpointを返す
	/// </summary>
	/// <param name="position"></param>
	/// <returns></returns>
	public Transform GetNearPoint(Vector3 position) {

		var point = GetNearRelayPoint(position);
		return point.transform;
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Use this for initialization
	void Start() {

		PointList = GetComponentsInChildren<RelayPoint>();

	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private RelayPoint GetNearRelayPoint(Vector3 pos) {

		float length = 9999f;
		RelayPoint p = null;

		for (int i = 0; i < PointList.Length; i++) {

			float l = (PointList[i].transform.position - pos).magnitude;
			if (l < length) {
				length = l;
				p = PointList[i];
			}
		}
		return p;
	}

	RelayPoint[] PointList;

}
