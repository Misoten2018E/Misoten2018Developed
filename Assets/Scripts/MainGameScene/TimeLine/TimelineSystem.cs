using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSystem : SceneStartEvent , IFGameStartEvent{


	//========================================================================================
	//                                    inspector
	//========================================================================================

	
	
	//========================================================================================
	//                                    public - override
	//========================================================================================

	public override void SceneMyInit() {
		base.SceneMyInit();
		NextEventId = 0;
	}

	public override void SceneOtherInit() {

		//SearchChildrenEvents();

		//for (int i = 0; i < GroupLists.Count; i++) {

		//	if (LastEventId < GroupLists[i].OrderID) {
		//		LastEventId = GroupLists[i].OrderID;
		//	}
		//}
		EventManager<IFGameStartEvent>.Instance.SetObject(this);
	}


	/// <summary>
	/// ゲーム開始
	/// </summary>
	public void GameStart() {

		SearchChildrenEvents();

		for (int i = 0; i < GroupLists.Count; i++) {

			if (LastEventId < GroupLists[i].OrderID) {
				LastEventId = GroupLists[i].OrderID;
			}
		}

		NextEventId = 0;
		SearchNextEventGroup();
		enabled = true;
	}

	// Update is called once per frame
	void Update() {

		if (!isInitialized) {
			return;
		}

		CheckStartEvent();

		if (isAllEventEnd) {

			// クリア
		}

	}

	


	//========================================================================================
	//                                    private
	//========================================================================================

	List<TimelineGroup> GroupLists = new List<TimelineGroup>();
	List<TimelineGroup> NowEventGroup = new List<TimelineGroup>();

	uint _NextEventId;
	public uint NextEventId {
		private set { _NextEventId = value; }
		get { return _NextEventId; }
	}


	uint _LastEventId;
	public uint LastEventId {
		private set { _LastEventId = value; }
		get { return _LastEventId; }
	}
       
	public List<TimelineGroup> Group {
		get { return GroupLists; }
	}

	/// <summary>
	/// 子供にあるイベント群を取得し、開始順に並べる
	/// </summary>
	void SearchChildrenEvents() {

		var c = GetComponentsInChildren<TimelineGroup>();

		if (c.Length > 0) {
			GroupLists.AddRange(c);
		}

		GroupLists.Sort(TimelineGroup.Compare);
	}

	/// <summary>
	/// イベント開始チェック
	/// </summary>
	void CheckStartEvent() {

		if (CheckNowEventEnd()) {

			NextEventId++;
			SearchNextEventGroup();
		}
	}

	public bool isAllEventEnd {
		get { return NextEventId >= LastEventId; }
	}

	/// <summary>
	/// 全て終了してたらtrue
	/// </summary>
	/// <returns></returns>
	bool CheckNowEventEnd() {

		// 空なら終わっている扱い
		if (NowEventGroup.Count == 0) {
			return true;
		}

		for (int i = 0; i < NowEventGroup.Count; i++) {
			if (!NowEventGroup[i].isEnd) {
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// 次のイベントグループ探索
	/// </summary>
	void SearchNextEventGroup() {

		// 初期化
		NowEventGroup.Clear();

		// 探索
		for (int i = 0; i < GroupLists.Count; i++) {
			if (GroupLists[i].OrderID == NextEventId) {
				NowEventGroup.Add(GroupLists[i]);
				GroupLists[i].TimeLineStart();
			}
		}
	}
}
