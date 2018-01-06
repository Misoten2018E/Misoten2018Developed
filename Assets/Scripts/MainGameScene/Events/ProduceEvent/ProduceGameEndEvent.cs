using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceGameEndEvent : MonoBehaviour,IFGameEndEvent,IFGameEndProduceCheck {


	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                     public
	//========================================================================================


	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

		EventManager<IFGameEndEvent>.Instance.SetObject(this);
		EventManager<IFGameEndProduceCheck>.Instance.SetObject(this);
		isEndCheck = false;
	}

	public void GameEnd() {

		var boss = FindObjectOfType<BossEnemy>();

		CreateDestroyParticle(boss.transform.position);

		CameraManager.Instance.EventCamera.EventCameraStartWithRot(ProduceEventCamera.CameraEvent.BossDestroy, boss.transform, new Vector3(0, 10f, 0), new Vector3(0f, 180f, 0f));

		StartCoroutine(GameObjectExtensions.DelayMethod(2.5f, EndEvent));
	}

	public bool IsEndProduce() {
		return isEndCheck;
	}
	//========================================================================================
	//                                     private
	//========================================================================================

	bool isEndCheck;

	private void EndEvent() {

		isEndCheck = true;

	}

	private void CreateDestroyParticle(Vector3 pos) {

		var despar = ResourceManager.Instance.Get<ParticleSupport>(ConstDirectry.DirParticleEdo, ConstEffects.DestroyBoss);
		var p = Instantiate(despar);
		p.transform.position = pos;
	}
}
