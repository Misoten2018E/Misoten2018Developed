﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTypeBase : MonoBehaviour {

	/// <summary>
	/// 敵初期化
	/// </summary>
	public abstract void InitEnemy(UsedInitData InitData);

	

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

		var endQt = Quaternion.LookRotation(target.position - transform.position);

		transform.rotation = Quaternion.Slerp(transform.rotation, endQt, (angle));
	}

	protected void MoveForward(float speed) {

		Vector3 v = transform.forward;
		v.y = 0f;
		v.Normalize();
		transform.position += v * speed * Time.deltaTime;

	}
}

public struct UsedInitData {
	public Transform BasePosition;
}