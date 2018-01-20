using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHittedObjectManager : MonoBehaviour {

	//========================================================================================
	//                                     public
	//========================================================================================

	public bool HitCheck(HitObject obj) {

		// 既にヒット済みなら
		bool isHitted = HitLog.CheckLog(obj);

		if (isHitted) {
			return false;
		}

		CameraChance(obj);

		// (衝撃の方向)
		var impact = (transform.position - obj.transform.position).normalized;
		Damaged.HittedTremble(ChildModelTrans, impact);

		ScorePlus(obj.Damage / 10, obj.ParentHit.PlayerNo);

		return true;
	}

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

		var hit = GetComponentsInChildren<BossHittedObject>();
		HittedList.AddRange(hit);

		for (int i = 0; i < HittedList.Count; i++) {

			HittedList[i].Initialize(this);
		}
	}


	private void Update() {

		if (HitLog != null) {
			HitLog.CheckEnd();
		}

	}

	//========================================================================================
	//                                     private
	//========================================================================================


	/// <summary>
	/// カメラチャンスを知らせる
	/// </summary>
	/// <param name="hito"></param>
	private void CameraChance(HitObject hito) {

		var ph = hito.ParentHit;

		if (ph == null) {
			return;
		}

		CheckProducePhotoCamera.PhotoType type = CheckProducePhotoCamera.PhotoType.Strong;
		switch (ph.actionType) {
			case HitSeriesofAction.ActionType.LightEnd:
				type = CheckProducePhotoCamera.PhotoType.LightEnd;
				break;
			case HitSeriesofAction.ActionType.Strong:
				break;
			default:
				return;
		}

		CheckProducePhotoCamera.Instance.PhotoChance(hito.transform, transform, type, 0/*ph.myPlayer.GetPlayerObj()*/);
	}

	private void ScorePlus(int score,int PlayerNum) {

		Score.instance.AddScore(score, PlayerNum);

	}

	protected Transform ChildModelTrans {
		get { return ChildModelAnim.transform; }
	}

	BossAnimationController _ChildModel;
	public BossAnimationController ChildModelAnim {
		get {
			if (_ChildModel == null) {
				_ChildModel = GetComponentInChildren<BossAnimationController>();
			}
			return _ChildModel;
		}
	}

	HittedDamage _Damaged;
	/// <summary>
	/// ダメージ時の動き
	/// </summary>
	protected HittedDamage Damaged {
		private set { _Damaged = value; }
		get {
			if (_Damaged == null) {
				_Damaged = GetComponent<HittedDamage>();
			}
			return _Damaged;
		}
	}

	List<BossHittedObject> HittedList = new List<BossHittedObject>();

	HitLogList HitLog = new HitLogList();
}
