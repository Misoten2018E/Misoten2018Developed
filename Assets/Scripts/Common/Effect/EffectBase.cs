using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EffectBase : PauseSupport {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] protected float LocalScale = 1f;

	protected virtual void Awake() {
		ScaleChange(LocalScale);
	}

	/// <summary>
	/// 起動する
	/// </summary>
	/// <param name="delayTime"></param>
	public abstract void Play(float delayTime = 0);

	/// <summary>
	/// 即時に止まる
	/// </summary>
	/// <param name="delayTime"></param>
	public abstract void Stop(float delayTime = 0);

	/// <summary>
	/// 生成を止める
	/// 段々と消えていく
	/// </summary>
	public abstract void StopGenerate();

	/// <summary>
	/// 生存しているかどうか
	/// </summary>
	public abstract bool IsActive {
		get;
	}

	public virtual void ScaleChange(float rate) {
		LocalScale = rate;
		transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
	}
}
