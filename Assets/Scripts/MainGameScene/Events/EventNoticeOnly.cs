using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNoticeOnly : TimelineEventStandard {

	/// <summary>
	/// イベント開始
	/// </summary>
	public override void EventStart() {

		base.SetFocus(this.transform);

		base.EventStart();

		EventEnd();
	}

	bool _isEnd;
	public override bool IsEnd {
		protected set { _isEnd = value; }
		get { return true; }
	}
}
