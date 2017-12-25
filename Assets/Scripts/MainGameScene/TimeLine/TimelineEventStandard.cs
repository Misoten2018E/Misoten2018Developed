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
	/// イベント開始時間
	/// </summary>
	public float time {
		get { return StartTime; }
	}



	//========================================================================================
	//                                    protected
	//========================================================================================

	/// <summary>
	/// フォーカス開始
	/// </summary>
	/// <param name="trs"></param>
	protected void SetFocus(Transform trs) {

		if (isFocusEvent) {
			CameraManager.Instance.FocusCamera.AddTarget(trs);
			StartCoroutine(FucusTimeUpdate(trs));
		}
	}

	/// <summary>
	/// フォーカス時間更新
	/// </summary>
	/// <param name="trs"></param>
	/// <returns></returns>
	protected IEnumerator FucusTimeUpdate(Transform trs) {

		print(gameObject.name + " : event開始");
		yield return new WaitForSeconds(FucusTime);
		CameraManager.Instance.FocusCamera.DeleteTarget(trs);
	}

	/// <summary>
	/// 開始演出起動
	/// </summary>
	protected void AwakeProduceStart() {

		var p = GetComponents<ProduceEventBase>();
		produceEvents.AddRange(p);

		ProduceEventBase.StartProduceLists(produceEvents, ProduceEventBase.EventType.Start);
	}

	/// <summary>
	/// 終了演出起動
	/// </summary>
	protected void AwakeProduceEnd() {

		ProduceEventBase.StartProduceLists(produceEvents, ProduceEventBase.EventType.End);
	}

	//========================================================================================
	//                                    virtual
	//========================================================================================


	/// <summary>
	/// イベント開始
	/// </summary>
	public virtual void EventStart() {

		AwakeProduceStart();
	}

	/// <summary>
	/// イベント終了
	/// </summary>
	public virtual void EventEnd() {

		AwakeProduceEnd();
	}

	/// <summary>
	/// 終了したかどうか
	/// </summary>
	public virtual bool IsEnd {
		protected set { }
		get { return true; }
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


	/// <summary>
	/// 演出リスト
	/// </summary>
	private List<ProduceEventBase> produceEvents = new List<ProduceEventBase>();
}

