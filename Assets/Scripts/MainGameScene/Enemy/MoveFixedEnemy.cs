using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFixedEnemy : EnemyTypeBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<Transform> TargetTransform;

	[SerializeField] private float MoveSpeed = 1f;

	//========================================================================================
	//                                    public
	//========================================================================================

	public void AddTarget(Transform target, bool isClear = false) {

		if (isClear) {
			TargetTransform.Clear();
		}

		TargetTransform.Add(target);

	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {
		TargetIndex = 0;
		CityTargeted = false;
		NextTargetSearch();
	}
	
	// Update is called once per frame
	void Update () {

		if (NowTarget == null) {
			return;
		}

		if (!Damaged.isHitted) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
		}

		HitLog.CheckEnd();
	}

	public override void InitEnemy(UsedInitData InitData) {

		transform.position = InitData.BasePosition.position;
		transform.rotation = InitData.BasePosition.rotation;
		TargetIndex = 0;
		CityTargeted = false;
	}

	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag(ConstTags.EnemyCheckPoint)) {

			TargetIndex++;
			NextTargetSearch();

		}

		if (other.CompareTag(ConstTags.City)) {
			NextTargetSearch();
		}

		if (other.CompareTag(ConstTags.PlayerAttack)) {

			bool isHit = !HitLog.CheckLog(other.GetComponent<HitObject>());

			if (isHit) {

				var v = transform.position - other.transform.position;
				Damaged.HittedTremble(transform, v.normalized);
				print("hit");
			}
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	bool CityTargeted; // 街をターゲットしているかどうか
	int TargetIndex;
	Transform NowTarget;

	/// <summary>
	/// 次の獲物へ向かう
	/// </summary>
	void NextTargetSearch() {

		if (CityTargeted) {

			// 街に着いたということなので
			// 一定時間待機状態へ
		}


		// 次のターゲットが存在しない場合
		if (((TargetIndex) >= TargetTransform.Count)) {

			CityTargeted = true;
			NowTarget = City.Instance.transform;
		}
		else {
			NowTarget = TargetTransform[TargetIndex];
		}

	}

	
}
