using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyFixedPop : TimelineEventStandard {

	[SerializeField] private EnemyData[] Enemy;

	public override void EventStart() {
		

	}

	[System.Serializable]
	public class EnemyData {

		[SerializeField] int Route1;
		[SerializeField] int Route2;

	}
}
