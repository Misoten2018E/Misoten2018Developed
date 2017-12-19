using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitSoundEffectSupport : MonoBehaviour {

	[SerializeField] protected SoundManager.SEType HitSE;

	protected void PlaySE() {

		SoundManager.Instance.PlaySE(HitSE, transform.position);

	}
}
