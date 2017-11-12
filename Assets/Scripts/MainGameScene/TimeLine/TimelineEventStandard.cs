using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimelineEventStandard : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] protected float StartTime;

	[SerializeField] protected bool isFocusEvent = false;

	[SerializeField] protected float FucusTime = 2f;

	//========================================================================================
	//                                    public
	//========================================================================================

	
	/// <summary>
	/// イベント開始
	/// </summary>
	public virtual void EventStart() {

		

	}

	public float time {
		get { return StartTime; }
	}

	public virtual bool IsEnd {
		protected set { }
		get { return true; }
	}

	protected void SetFocus(Transform trs) {

		if (isFocusEvent) {
			CameraManager.Instance.FocusCamera.AddTarget(trs);
			StartCoroutine(FucusTimeUpdate(trs));
		}
	}

	protected IEnumerator FucusTimeUpdate(Transform trs) {

		print("event開始");
		yield return new WaitForSeconds(FucusTime);
		CameraManager.Instance.FocusCamera.DeleteTarget(trs);
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

