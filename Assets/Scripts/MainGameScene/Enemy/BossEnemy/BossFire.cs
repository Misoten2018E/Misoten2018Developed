using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : PauseSupport {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private AnimationCurve YAnim;

	[SerializeField] private float MaxLandingTime = 3f;

	[SerializeField] private float Height = 14f;

	//========================================================================================
	//                                     public
	//========================================================================================

	/// <summary>
	/// 炎発射
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="TargetPos"></param>
	public void	StartFire(Vector3 startPos, Vector3 TargetPos) {

		transform.position = startPos;
		Vec3Comp = new Vector3Complession(startPos, TargetPos);

		var hit = GetComponentInChildren<HitSeriesofAction>();
		hit.Initialize(this.gameObject);
		hit.Activate();

		FireUpdateCort = GameObjectExtensions.LoopMethod(MaxLandingTime, PositionMove, PositionMoveEnd);
		StartCoroutine(FireUpdateCort);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	Vector3Complession Vec3Comp;

	IEnumerator FireUpdateCort;

	/// <summary>
	/// 移動
	/// </summary>
	/// <param name="rate"></param>
	void PositionMove(float rate) {

		Vector3 v = Vec3Comp.CalcPosition(rate);
		v.y = YAnim.Evaluate(rate) * Height;
		transform.position = v;
	}

	/// <summary>
	/// 移動終了
	/// </summary>
	void PositionMoveEnd() {

		Destroy(gameObject);
	}
}
