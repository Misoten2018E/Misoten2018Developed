using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectSupport : MonoBehaviour {

	static public void Follow(EffectBase effect ,Vector3 pos ,Vector3 forward) {

		FollowPosition(effect, pos);
		FollowRotation(effect, forward);
	}


	static public void FollowPosition(EffectBase effect ,Vector3 position) {

		effect.transform.position = position;
	}

	static public void FollowRotation(EffectBase effect, Vector3 forward) {

		effect.transform.LookAt(effect.transform.position + forward);

	}
}
