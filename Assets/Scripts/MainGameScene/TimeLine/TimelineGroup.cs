using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineGroup : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("起動する順番")]
	[SerializeField] private uint OrderId = 0;
	[Tooltip("ウェーブ終了までの猶予時間(秒)")]
	[SerializeField] private float GraceTime = 15f;

	//========================================================================================
	//                                    public
	//========================================================================================

	private void Awake() {
		isEnd = false;
		isActive = false;

		var p = GetComponents<ProduceEventBase>();
		produceEvents.AddRange(p);
	}

	/// <summary>
	/// スタート時
	/// </summary>
	public void TimeLineStart() {

		isActive = true;
		isEnd = false;

		NextEventId = 0;
		SearchChildrenEvents();

		ProduceEventBase.StartProduceLists(produceEvents, ProduceEventBase.EventType.Start);
	}

	/// <summary>
	/// タイムライン終了時
	/// </summary>
	public void TimeLineEnd() {

		isActive = false;
		isEnd = true;

		ProduceEventBase.StartProduceLists(produceEvents, ProduceEventBase.EventType.End);
	}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {

		if (isActive == false) {
			return;
		}

		TimelineTime += Time.deltaTime;

		CheckStartEvent();
	}

	//========================================================================================
	//                                    private
	//========================================================================================


	float _TimelineTime;
	/// <summary>
	/// このグループ開始からの経過時間
	/// </summary>
	public float TimelineTime {
		private set { _TimelineTime = value; }
		get { return _TimelineTime; }
	}


	bool _isActive;
	/// <summary>
	/// 起動中ならtrue
	/// </summary>
	public bool isActive {
		private set { _isActive = value; }
		get { return _isActive; }
	}


	bool _isEnd;
	public bool isEnd {
		private set { _isEnd = value; }
		get { return _isEnd; }
	}
      

	int _NextEventId;
	private int NextEventId {
		set { _NextEventId = value; }
		get { return _NextEventId; }
	}

	/// <summary>
	/// 演出リスト
	/// </summary>
	private List<ProduceEventBase> produceEvents = new List<ProduceEventBase>();
     
	/// <summary>
	/// 起動が行われる順番
	/// </summary>
	public uint OrderID {
		get { return OrderId; }
	}

	List<TimelineEventStandard> EventLists = new List<TimelineEventStandard>();
	
	/// <summary>
	/// 子供にあるイベント群を取得し、開始順に並べる
	/// </summary>
	void SearchChildrenEvents() {

		var c = GetComponentsInChildren<TimelineEventStandard>();

		if (c.Length > 0) {
			EventLists.AddRange(c);
		}

		EventLists.Sort(TimelineEventStandard.Compare);
	}

	/// <summary>
	/// イベント開始チェック
	/// </summary>
	void CheckStartEvent() {

		int max = EventLists.Count;
		for (int i = NextEventId; i < max; i++) {

			// 時間が来てないなら
			if (EventLists[i].time > TimelineTime) {
				break;
			}
			// イベントが行われたので次のIDへ
			EventLists[i].EventStart();
			NextEventId = i + 1;
		}

		// 終了チェック
		if ((max <= NextEventId) && (isActive)) {
			//TimeLineEnd();
			isActive = false;
			StartCoroutine(IETimelineEndEventCheck());
		}
	}

	bool IsAllEventEnd {
		get {

			for (int i = 0; i < EventLists.Count; i++) {

				if (!EventLists[i].IsEnd) {
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>
	/// イベント終了チェック開始
	/// </summary>
	/// <returns></returns>
	IEnumerator IETimelineEndEventCheck() {

		float time = 0f;

		while (true) {

			if ((time >= GraceTime) || IsAllEventEnd) {
				break;
			}

			time += Time.deltaTime;
			yield return null;
		}

		TimeLineEnd();
	}

	/// <summary>
	/// 比較関数
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	static public int Compare(TimelineGroup a, TimelineGroup b) {

		if (a.OrderId > b.OrderId) {
			return 1;
		}
		else if (a.OrderId < b.OrderId) {
			return -1;
		}
		return 0;
	}
}
