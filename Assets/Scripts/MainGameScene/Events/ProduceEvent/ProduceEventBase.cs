using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProduceEventBase : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] protected EventType eventType = ProduceEventBase.EventType.End;

	public enum EventType {
		Start,
		End
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	public abstract void ProduceStart();

	/// <summary>
	/// リストを渡すとイベントを開始してくれる
	/// <para>タイプを渡すとそのタイプだけを開始させる</para>
	/// <para>何も渡さないならすべてが起動</para>
	/// </summary>
	/// <param name="baselist"></param>
	/// <param name="actionType"></param>
	static public void StartProduceLists(List<ProduceEventBase> baselist) {

		for (int i = 0; i < baselist.Count; i++) {
			baselist[i].ProduceStart();
		}
	}

	/// <summary>
	/// リストを渡すとイベントを開始してくれる
	/// <para>タイプを渡すとそのタイプだけを開始させる</para>
	/// <para>何も渡さないならすべてが起動</para>
	/// </summary>
	/// <param name="baselist"></param>
	/// <param name="actionType"></param>
	static public void StartProduceLists(List<ProduceEventBase> baselist ,EventType actionType) {

		for (int i = 0; i < baselist.Count; i++) {

			if (baselist[i].eventType == actionType) {
				baselist[i].ProduceStart();
			}	
		}
	}
}
