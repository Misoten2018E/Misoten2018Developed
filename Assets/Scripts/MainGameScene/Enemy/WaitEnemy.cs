using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaitEnemy : EnemyTypeBase {

	/// <summary>
	/// 一定時間移動を止めるflagを立てる処理
	/// </summary>
	/// <param name="MaxTime"></param>
	public void StopMove(float MaxTime, System.Action EndCallback = null) {

		if (ieStop != null) {
			StopCoroutine(ieStop);
		}

		ieStop = IEStopMove(MaxTime,EndCallback);
		StartCoroutine(ieStop);
	}

	/// <summary>
	/// 移動強制許可
	/// </summary>
	public void EnableMove() {

		IsStop = false;
		if (ieStop != null) {
			StopCoroutine(ieStop);
			ieStop = null;
		}
	}

	IEnumerator ieStop;
	IEnumerator IEStopMove(float MaxTime ,System.Action endCallback) {

		IsStop = true;
		float time = 0f;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			yield return null;
		}

		IsStop = false;
		ieStop = null;

		if (endCallback != null) {
			endCallback();
		}
	}


	bool _IsStop;
	public bool IsStop {
		protected set { _IsStop = value; }
		get { return _IsStop; }
	}
      
}
