using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyEgg : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private AnimationCurve AnimYCurve;

	[SerializeField] private float Height = 4f;
	
	//========================================================================================
	//                                     public
	//========================================================================================

	public void EventStart(Vector3 StartPos, Vector3 EndPos, float time) {

		EndPos.y += 0.3f;
		Vec3Comp.Initialize(StartPos, EndPos);

		transform.position = StartPos;

		StartCoroutine(GameObjectExtensions.LoopMethod(time, EventMove, EndEventMove));
	}

	/// <summary>
	/// イベント終了
	/// </summary>
	public void EndEvent() {

		var fog = ResourceManager.Instance.Get<ParticleSupport>(ConstDirectry.DirParticleEdo, ConstEffects.SublimitedFog);

		var f = Instantiate(fog);
		f.transform.position = this.transform.position;
		f.Play();
		StartCoroutine(GameObjectExtensions.LoopMethod(0.5f, ScaleChange, EndProduceEvent));

	}


	System.Action _EndCallback;
	public System.Action EndCallback {
		set { _EndCallback = value; }
		get { return _EndCallback; }
	}
      

	//========================================================================================
	//                                    override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	/// <summary>
	/// サイズ変更
	/// </summary>
	/// <param name="rate"></param>
	void ScaleChange(float rate) {

		transform.localScale = new Vector3(rate, rate, rate);
	}

	/// <summary>
	/// 演出イベント終了時
	/// </summary>
	void EndProduceEvent() {

		Destroy(gameObject);

	}

	/// <summary>
	/// 移動
	/// </summary>
	/// <param name="rate"></param>
	private void EventMove(float rate) {

		Vector3 oldPos = transform.position;

		Vector3 pos = Vec3Comp.CalcPosition(rate);
		pos.y += AnimYCurve.Evaluate(rate) * Height;
		transform.position = pos;

		Vector3 dir = pos - oldPos;
		dir.Normalize();

		RotateTarget(transform.position + dir);
	}

	/// <summary>
	/// 方向指定
	/// </summary>
	/// <param name="target"></param>
	private void RotateTarget(Vector3 target) {

		transform.LookAt(target);
	}

	/// <summary>
	/// 移動終了時
	/// </summary>
	private void EndEventMove() {

		transform.position = Vec3Comp.CalcPosition(1f);
		transform.rotation = Quaternion.identity;

		if (EndCallback != null) {
			EndCallback();
			EndCallback = null;
		}
	}

	Vector3Complession Vec3Comp = new Vector3Complession();
}
