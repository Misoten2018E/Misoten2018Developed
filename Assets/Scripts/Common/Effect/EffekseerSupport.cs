using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffekseerEmitter))]
public class EffekseerSupport : EffectBase {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public override void Play(float delayTime = 0) {
		gameObject.SetActive(true);
		effekseer.Play();
	}

	public override void Stop(float delayTime = 0) {
		effekseer.Stop();
	}

	public override void StopGenerate() {
		effekseer.StopRoot();
	}

	public override bool IsActive {
		get {
			return effekseer.exists;
		}
	}

	public override void OnPause() {
		effekseer.paused = true;
	}

	public override void OnResume() {
		effekseer.paused = false;
	}


	//========================================================================================
	//                                 public - override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================


	EffekseerEmitter _effekseer;
	public EffekseerEmitter effekseer {
		private set { _effekseer = value; }
		get {
			if (_effekseer == null) {
				_effekseer = GetComponent<EffekseerEmitter>();
			}
			return _effekseer;
		}
	}

	
}
