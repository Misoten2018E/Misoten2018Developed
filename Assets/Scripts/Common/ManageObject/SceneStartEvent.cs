using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム開始時管理オブジェクト
/// </summary>
public abstract class SceneStartEvent : MonoBehaviour , Edo.Base.IFSceneStartInterface {

	public void SceneDelayInit() {}

	public void SceneMyInit() {}

	public void SceneOtherInit() {}
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
