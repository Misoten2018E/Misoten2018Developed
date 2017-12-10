using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MoveTargetEnemy : WaitEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("移動速度")]
	[SerializeField] protected float MoveSpeed = 1f;

	//========================================================================================
	//                                    protected
	//========================================================================================

	/// <summary>
	/// 目標に向かって進む
	/// </summary>
	/// <param name="target"></param>
	/// <param name="speed"></param>
	protected void MoveAdvanceToTarget(Transform target, float speed, float angle = 0.03f) {

		if (target == null) {
			DebugLog.log(gameObject.name + ": target error");
			return;
		}

		RotateToTarget(target, angle);
		MoveForward(speed);
	}


	/// <summary>
	/// 目標への回転
	/// </summary>
	/// <param name="target"></param>
	/// <param name="angle"></param>
	protected void RotateToTarget(Transform target, float angle) {

		Vector3 rot = target.position - transform.position;
		var endQt = Quaternion.LookRotation(rot.normalized);

		transform.rotation = Quaternion.Slerp(transform.rotation, endQt, (angle));
		rot = transform.rotation.eulerAngles;
		rot.x = rot.z = 0f;
		transform.rotation = Quaternion.Euler(rot);
	}

	protected void MoveForward(float speed) {

		Vector3 v = transform.forward;
		v.y = 0f;
		v.Normalize();
		v = transform.position + v * speed * Time.deltaTime;
		v.y = 0f;
		transform.position = v;
	}
}
