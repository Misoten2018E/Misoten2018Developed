using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {



	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static EnemyManager myInstance;
	static public EnemyManager Instance {
		get {
			return myInstance;
		}
	}

	/// <summary>
	/// 敵のセット管理
	/// </summary>
	/// <param name="enemy"></param>
	public void SetEnemy(EnemyTypeBase enemy) {

		EnemyLists[(int)enemy.MyType].Add(enemy);
		enemy.transform.SetParent(this.transform);
	}

	/// <summary>
	/// nullチェックを行った上で敵リストを返す
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public List<EnemyTypeBase> GetEnemyList(EnemyTypeBase.EnemyType type) {
		return GetEnemyList((int)type);
	}

	/// <summary>
	/// nullチェックを行った上で敵リストを返す
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	public List<EnemyTypeBase> GetEnemyList(int num) {
		ListNullCheck(num);
		return EnemyLists[num];
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;

		for (int i = 0; i < EnemyTypeBase.EnemyTypeMax; i++) {
			EnemyLists.Add(new List<EnemyTypeBase>());
		}
	}

	//private void Update() {

	//	CheckTime.TimeUpdate();

	//	if (CheckTime.IsEnd) {

	//		CheckTime.Initialize(1f);
	//		ListNullCheck(checkIndex);
	//		checkIndex = checkIndex >= (int)EnemyTypeBase.EnemyType.BossPop ? 0 : checkIndex + 1;
	//	}
	//}



	//========================================================================================
	//                                    private
	//========================================================================================

	List<List<EnemyTypeBase>> EnemyLists = new List<List<EnemyTypeBase>>();

	TimeComplession CheckTime = new TimeComplession(1f);

	int checkIndex = 0;

	/// <summary>
	/// 一括チェック
	/// </summary>
	private void ListsNullCheck() {

		for (int i = 0; i < EnemyTypeBase.EnemyTypeMax; i++) {

			ListNullCheck(i);
		}
	}

	/// <summary>
	/// 単体チェック
	/// </summary>
	/// <param name="num"></param>
	private void ListNullCheck(int num) {

		var list = EnemyLists[num];

		for (int i = 0; i < list.Count; i++) {

			if (list[i] == null) {
				list.RemoveAt(i);
				i--;
			}
		}
	}
}
