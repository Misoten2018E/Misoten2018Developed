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

		//if (transform.position.y < 0.4f) {
		//	transform.position += new Vector3(0, 0.1f, 0);
		//}
		//else if (transform.position.y > 0.6f) {
		//	transform.position -= new Vector3(0, 0.1f, 0);
		//}

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

			var hito = other.GetComponent<HitObject>();
			hito.DamageHp(MyHp);

			bool isHit = !HitLog.CheckLog(hito);
			if (isHit) {

				SwtichHitted(hito);
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

	void SwtichHitted(HitObject obj) {

		// (衝撃の方向)
		var impact = (transform.position - obj.transform.position).normalized;
		Damaged.HittedTremble(ChildModel.transform, impact);

		if (MyHp.isDeath && ieDeath == null) {
			ieDeath = IEDeath(0.5f);
			StartCoroutine(ieDeath);
		}

		switch (obj.hitType) {
			case HitObject.HitType.Impact:

				var HitImpact = obj as HitObjectImpact;
				HitImpact.Impact(Damaged, impact);
				print("hitImpact");

				break;
			case HitObject.HitType.BlowOff:

				var HitBlow = obj as HitObjectBlowOff;

				break;
			case HitObject.HitType.Suction:

				var HitSuction = obj as HitObjectSuction;
				HitSuction.Sucion(Damaged);
				print("hitSuction");
				break;

			default:
				break;
		}

	}

	IEnumerator ieDeath;
	IEnumerator IEDeath(float MaxTime) {

		float time = 0f;

		while (true) {

			time += Time.deltaTime;
			if (time >= MaxTime) {
				break;
			}
			yield return null;
		}

		Destroy();
	}

	/// <summary>
	/// 死亡
	/// </summary>
	public void Destroy() {

		Destroy(this.gameObject);
	}


	MeshRenderer _ChildModel;
	public MeshRenderer ChildModel {
		get {
			if (_ChildModel == null) {
				_ChildModel = GetComponentInChildren<MeshRenderer>();
			}
			return _ChildModel;
		}
	}
      
}
