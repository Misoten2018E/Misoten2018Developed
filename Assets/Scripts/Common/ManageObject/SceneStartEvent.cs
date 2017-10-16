using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム開始時管理オブジェクト
/// </summary>
public abstract class SceneStartEvent : PauseSupport, Edo.Base.IFSceneStartInterface {

	/// <summary>
	/// 自身のものだけの初期化
	/// </summary>
	public virtual void SceneMyInit() { isInitialized = true; }

	/// <summary>
	/// 必ず他のオブジェクトの自身の初期処理が終わっている状態で呼ばれる
	/// 他のものとの関連初期化処理
	/// </summary>
	public virtual void SceneOtherInit() { }

	/// <summary>
	/// 上記のイベントは終わっていることが前提となる
	/// 遅延しての初期化処理
	/// </summary>
	public virtual void SceneDelayInit() { }


	bool _isInitialized;
	/// <summary>
	/// 初期化されているかどうか
	/// </summary>
	public bool isInitialized {
		protected set { _isInitialized = value; }
		get { return _isInitialized; }
	}

}

namespace Edo.Base {

	/// <summary>
	/// 元となるインターフェース
	/// </summary>
	interface IFSceneStartInterface {

		void SceneMyInit();

		void SceneOtherInit();

		void SceneDelayInit();
	}
}
