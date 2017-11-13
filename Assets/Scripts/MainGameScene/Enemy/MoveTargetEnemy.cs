using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MoveTargetEnemy : WaitEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

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
		//rot.x = 0f; // 回転を無しに
		var endQt = Quaternion.LookRotation(rot.normalized);
		//endQt.x = 0f;

		transform.rotation = Quaternion.Slerp(transform.rotation, endQt, (angle));
	}

	protected void MoveForward(float speed) {

		Vector3 v = transform.forward;
		v.y = 0f;
		v.Normalize();
		transform.position += v * speed * Time.deltaTime;

	}
}
