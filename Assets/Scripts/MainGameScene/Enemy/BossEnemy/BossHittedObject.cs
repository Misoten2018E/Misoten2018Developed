using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BossHittedObject : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private BodyType bodyType;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum BodyType {
		LeftLeg,
		RightLeg,
		Body,
		Tale
	}

	/// <summary>
	/// 初期処理
	/// </summary>
	/// <param name="bossHitMng"></param>
	public void Initialize(BossHittedObjectManager bossHitMng) {

		BossHitMng = bossHitMng;
		var c = MyCollider;
	}

	//========================================================================================
	//                                    override
	//========================================================================================

	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag(ConstTags.PlayerAttack)) {

			var hit = other.GetComponent<HitObject>();
			if (BossHitMng.HitCheck(hit)) {

				CreateHittedEffect(hit, hit.ParentHit.transform.position);
			}
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================


	/// <summary>
	/// ダメージ時エフェクト作成
	/// </summary>
	/// <param name="obj"></param>
	protected void CreateHittedEffect(HitObject obj, Vector3 playerPos) {

		obj.HitEffect(transform.position + Option, playerPos);

	}
	readonly Vector3 Option = new Vector3(0, 1, 0);

	Collider _MyCollider;
	public Collider MyCollider {
		get {
			if (_MyCollider == null) {
				_MyCollider = GetComponent<Collider>();
			}
			return _MyCollider;
		}
	}

	BossHittedObjectManager BossHitMng;
}
