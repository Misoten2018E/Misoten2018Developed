using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのプレイヤー操作開始時に呼ばれる
/// </summary>
interface IFGameStartEvent {

	void GameStart();
}

/// <summary>
/// ゲームのプレイヤー操作終了時に呼ばれる
/// </summary>
interface IFGameEndEvent {

	void GameEnd();
}

/// <summary>
/// ゲーム終了演出時にtrueとなる
/// セットされたオブジェクト全てがtrueであれば次のフェーズへ移行
/// </summary>
interface IFGameEndProduceCheck {

	bool IsEndProduce();
}

public class EventManager<T> {

	static EventManager<T> _Instance;
	static public EventManager<T> Instance {
		private set { _Instance = value; }
		get {
			if (_Instance == null) {
				_Instance = new EventManager<T>();
			}
			return _Instance;
		}
	}

	List<T> EventList = new List<T>();

	/// <summary>
	/// イベントを行うオブジェクトセット
	/// </summary>
	/// <param name="obj"></param>
	public void SetObject(T obj) {
		EventList.Add(obj);
	}

	public void MethodStart(System.Action<T> EvMethod) {

		for (int i = 0; i < EventList.Count; i++) {
			EvMethod(EventList[i]);
		}
	}

	public bool MethodCheck(System.Func<T, bool> EvMethod) {

		for (int i = 0; i < EventList.Count; i++) {

			// trueでないものが存在した時点でfalse
			if (!EvMethod(EventList[i])) {
				return false;
			}
		}
		return true;
	}
}