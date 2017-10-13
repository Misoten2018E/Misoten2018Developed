using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 複数同一のものが存在し、リポップするオブジェクトを管理する
/// これで管理することが出来るオブジェクトは
///
/// <para>・クラスであること</para>
/// <para>・RecycleInterfaceを実装していること</para>
/// <para>・デフォルトコンストラクタを定義していること</para>
/// 
/// 上記条件を満たす必要が有る
/// </summary>
/// <typeparam name="type"></typeparam>
public class RecycleClsManager<type> where type : class, RecycleInterface ,new() {

	private List<type> list;

	/// <summary>
	/// デフォルトコンストラクタ
	/// </summary>
	public RecycleClsManager() {
		list = new List<type>();
	}

	~RecycleClsManager() {
		list.Clear();
		list = null;
	}

	/// <summary>
	/// コンストラクタ
	/// 初期キャパシティ拡張
	/// 利用量が或る程度分かっているならこちらを使うと効率的
	/// </summary>
	public RecycleClsManager(int DefaultCapacity) {
		list = new List<type>(DefaultCapacity);
	}


	// 未使用オブジェクトを返す
	// 存在しない場合は新規作成
	public type GetUnusedObject() {

		type obj = Recycle();

		if(obj != null) {
			return obj;
		}
		else {

			// オブジェクトの新規作成
			obj = new type();
			list.Add(obj);
			return obj;
		}
	}

	// 未使用オブジェクトを探す
	private type Recycle() {

		int size = list.Count;
		for(int i = 0; i < size; i++) {
			if (!(list[i].isUsed)) {
				return list[i];
			}
		}
		return null;
	}
}


/// <summary>
/// 複数同一のものが存在し、リポップするオブジェクトを管理する
/// これで管理することが出来るオブジェクトは
///
/// <para>・クラスであること</para>
/// <para>・RecycleInterfaceを実装していること</para>
/// <para>・デフォルトコンストラクタを定義していること</para>
/// 
/// 上記条件を満たす必要が有る
/// </summary>
/// <typeparam name="type"></typeparam>
public class RecycleObjManager<type> where type : MonoBehaviour, RecycleInterface {

	private List<type> list;

	/// <summary>
	/// デフォルトコンストラクタ
	/// </summary>
	public RecycleObjManager() {
		list = new List<type>();
	}

	~RecycleObjManager() {
		list.Clear();
		list = null;
	}

	/// <summary>
	/// コンストラクタ
	/// 初期キャパシティ拡張
	/// 利用量が或る程度分かっているならこちらを使うと効率的
	/// </summary>
	public RecycleObjManager(int DefaultCapacity) {
		list = new List<type>(DefaultCapacity);
	}


	// 未使用オブジェクトを返す
	// 存在しない場合は新規作成
	public type GetUnusedObject(type prefab) {

		type obj = Recycle();

		if (obj != null) {
			return obj;
		}
		else {

			// オブジェクトの新規作成
			obj = GameObject.Instantiate<type>(prefab);
			list.Add(obj);
			return obj;
		}
	}

	// 未使用オブジェクトを探す
	private type Recycle() {

		int size = list.Count;
		for (int i = 0; i < size; i++) {

			if (list[i] == null) {
				list.RemoveAt(i);
				i--;
				continue;
			}

			if (!(list[i].isUsed)) {
				return list[i];
			}
		}
		return null;
	}
}

/// <summary>
/// クラス用リサイクルインタフェース
/// 生成時にはisUsedがfalseを返すようにしておくこと
/// <para>Activateされて初めて動くオブジェクトとする</para>
/// </summary>
public interface RecycleInterface {

	// 利用されているかどうか
	bool isUsed { get; }

	// 起動
	void Activate();
}
