using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyAttackArea : MonoBehaviour,DebuggableObject {

	/// <summary>
	/// デバッグ実行
	/// </summary>
	/// <param name="isDebugMode"></param>
	public void Debug(bool isDebugMode) {

		var mesh = GetComponent<MeshRenderer>();
		if (mesh != null) {
			mesh.enabled = isDebugMode;
		}
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	public void SetHitEnterCallback(System.Action<Collider> _HitCallback) {
		HitCallBack = _HitCallback;
	}

	public void AddHitEnterCallback(System.Action<Collider> _HitCallback) {
		HitCallBack += _HitCallback;
	}


	Collider _MyCollider;
	public Collider MyCollider {
		private set { _MyCollider = value; }
		get { return _MyCollider; }
	}
      

	//========================================================================================
	//                                    private
	//========================================================================================

	private System.Action<Collider> HitCallBack;

	private void OnTriggerEnter(Collider other) {

		if (HitCallBack != null) {
			HitCallBack(other);
			HitCallBack = null;
		}
	}
}
