using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSupport : EffectBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public override void Play(float delayTime = 0) {
		gameObject.SetActive(true);
		for (int i = 0; i < ChildrenList.Count; i++) {
			ChildrenList[i].Play();
		}
	}

	public override void Stop(float delayTime = 0) {
		for (int i = 0; i < ChildrenList.Count; i++) {
			ChildrenList[i].Stop();
			ChildrenList[i].Clear();
		}
	}

	public override void StopGenerate() {

		for (int i = 0; i < ChildrenList.Count; i++) {
			ChildrenList[i].Stop();
		}
	}

	public override bool IsActive {
		get {
			// 0が親となるので
			return ChildrenList[0].isPlaying;
		}
	}

	public override void OnPause() {
		for (int i = 0; i < ChildrenList.Count; i++) {
			ChildrenList[i].Pause();
		}
	}

	public override void OnResume() {
		for (int i = 0; i < ChildrenList.Count; i++) {
			ChildrenList[i].Play();
		}
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	//========================================================================================
	//                                     private
	//========================================================================================

	List<ParticleSystem> _ChildrenList = new List<ParticleSystem>();
	public List<ParticleSystem> ChildrenList {
		get {
			if (_ChildrenList.Count == 0) {
				var c = GetComponentsInChildren<ParticleSystem>();
				_ChildrenList.AddRange(c);
			}
			return _ChildrenList;
		}
	}

	
}
