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
			BossHitMng.HitCheck(hit);
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================


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
