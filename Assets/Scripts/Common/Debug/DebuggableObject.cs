using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DebuggableObject {

	void Debug(bool isDebugMode);

}


public static class GameObjectExtensions {
	/// <summary>
	/// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
	/// </summary>
	public static List<T> FindObjectOfInterface<T>() where T : class {

		List<T> lists = new List<T>();

		foreach (var n in GameObject.FindObjectsOfType<Component>()) {
			var component = n as T;
			if (component != null) {
				lists.Add(component);
			}
		}
		return lists;
	}

	/// <summary>
	/// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
	/// </summary>
	public static List<T> FindChildOfInterface<T>(GameObject parent) where T : class {

		List<T> lists = new List<T>();

		foreach (var n in parent.GetComponentsInChildren<T>()) {
			var component = n as T;
			if (component != null) {
				lists.Add(component);
			}
		}
		return lists;
	}

	/// <summary>
	/// フレーム毎にループする処理
	/// </summary>
	/// <param name="MaxTime"></param>
	/// <param name="act"></param>
	/// <param name="EndCallback"></param>
	/// <returns></returns>
	public static IEnumerator LoopMethod(float MaxTime, System.Action<float> act, System.Action EndCallback = null) {

		float time = 0f;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			act(time / MaxTime);

			yield return null;
		}

		if (EndCallback != null) {
			EndCallback();
		}
	}

	/// <summary>
	/// 遅延して行われる
	/// </summary>
	/// <param name="MaxTime"></param>
	/// <param name="EndCallback"></param>
	/// <returns></returns>
	public static IEnumerator DelayMethod(float MaxTime, System.Action EndCallback) {

		yield return new WaitForSeconds(MaxTime);

		EndCallback();
	}
}