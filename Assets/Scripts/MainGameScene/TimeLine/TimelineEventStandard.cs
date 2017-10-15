using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimelineEventStandard : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] protected float StartTime;


	//========================================================================================
	//                                    public
	//========================================================================================

	
	/// <summary>
	/// イベント開始
	/// </summary>
	public virtual void EventStart() {



	}

	public float Time {
		get { return StartTime; }
	}

	/// <summary>
	/// 比較関数
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	static public int Compare(TimelineEventStandard a ,TimelineEventStandard b) {

		if (a.StartTime > b.StartTime) {
			return 1;
		}
		else if (a.StartTime < b.StartTime) {
			return -1;
		}
		return 0;
	}
}

