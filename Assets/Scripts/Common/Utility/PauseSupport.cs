using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class PauseSupport : MonoBehaviour {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	/// <summary>
	/// ポーズを許すかどうか
	/// </summary>
	[Tooltip("常にポーズされない状態の時[false]となる")]
	[SerializeField] bool isPausePermit = true;


	Behaviour[] pauseBehavs = null; // ポーズ対象のコンポーネント

	//==========================================================
	//						ポーズされたとき
	//==========================================================
	public virtual void OnPause() {

		// 既にポーズされてるか、またはポーズを許さない時
		if (pauseBehavs != null || (isPausePermit == false)) {
			return;
		}

		// 有効なBehaviourを取得
		var array = GetComponentsInChildren<Behaviour>();
		pauseBehavs = Array.FindAll(array, (obj) => { return (obj != null) ? obj.enabled : false; });

		//更新無効化
		for (int i = 0; i < pauseBehavs.Length; i++) {
			pauseBehavs[i].enabled = false;
		}
	}

	//==========================================================
	//					ポーズ解除されたとき
	//==========================================================
	public virtual void OnResume() {

		// ポーズされていないか、またはポーズを許さない時
		if (pauseBehavs == null || (isPausePermit == false)) {
			return;
		}

		// ポーズ前の状態にBehaviourの有効状態を復元
		for (int i = 0; i < pauseBehavs.Length; i++) {
			pauseBehavs[i].enabled = true;
		}

		pauseBehavs = null;
	}

}
