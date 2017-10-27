using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugLog : MonoBehaviour {

	private static DebugLog myInstance;

	// ログの記録
	private List<string> logMsg = new List<string>();

	// ピンポイントでログを追うためのコンテナ
	private List<StringList> chaseLog = new List<StringList>(20);
	

	public static int ChaseLog(Vector3 v, int id = -1) {

#if UNITY_DEBUG
		if (myInstance != null) {

			if (id < 0) {
				id = myInstance.SearchNonUsedId();
			}
			myInstance._ChaseLog(v, id);
			return id;

		}
		else {
			return -1;
		}
#else
		return -1;
#endif

	}

	public static int ChaseLog(string s, int id = -1) {

#if UNITY_DEBUG
		if (myInstance != null) {
			if (id < 0) {
				id = myInstance.SearchNonUsedId();
			}
			myInstance._ChaseLog(s, id);
			return id;
		}
		else {
			return -1;
		}
#else
		return -1;
#endif
	}



	/// <summary>
	/// 流れるログ
	/// <para>DebugLog.ChaseLog(string.Format("touchCount: {0}",input.touchCount),0);</para>
	/// </summary>
	/// <param name="msg"></param>
	public static void log(string msg) {

#if UNITY_DEBUG
		if (myInstance != null) {
			myInstance._log(msg);
		}
#endif

	}

	private void OnDestroy() {
		myInstance = null;
	}


	/// <summary>
	/// ピンポイントでログ表示(数値などを追う場合はこちら)
	/// <para>DebugLog.ChaseLog(string.Format("touchCount: {0}",input.touchCount),0);</para>
	/// </summary>
	/// <param name="s">文字列</param>
	/// <param name="id">格納されたログのid</param>
	private void _ChaseLog(string s, int id) {

		if (chaseLog.Capacity < id || id < 0) {
			print("container size overflow");
			return;
		}

		while (chaseLog.Count <= id) {
			chaseLog.Add(new StringList(""));
		}
		chaseLog[id].str = s;
	}

	/// <summary>
	/// ピンポイントでログ表示
	/// </summary>
	/// <param name="s">文字列</param>
	/// <param name="id">格納されたログのid</param>
	private void _ChaseLog(Vector3 v, int id) {

		string s = string.Format("x:{0:f3} y:{1:f3} z:{2:f3}", v.x, v.y, v.z);

		ChaseLog(s, id);
	}

	/// <summary>
	/// シーン遷移などのイベント確認系の表示(数値を追う場合はChaseLogで)
	/// </summary>
	/// <param name="msg"></param>
	private void _log(string msg) {
		logMsg.Add(msg);
		// 直近の5件のみ保存する
		if (logMsg.Count > 10) {
			logMsg.RemoveAt(0);
		}
	}

	/// <summary>
	/// 使ってないID検索(無い場合は最新のID)
	/// </summary>
	/// <returns></returns>
	private int SearchNonUsedId() {

		int max = chaseLog.Count;
		for (int i = 0; i < max; i++) {
			if (chaseLog[i].used == false) {
				return i;
			}
		}
		return max;
	}

	private Text text;

	/// <summary>
	/// 開始処理
	/// </summary>
	private void Awake() {

#if UNITY_DEBUG

		if (myInstance == null) {
			myInstance = this;
		//	DontDestroyOnLoad(this);
		}
		else {
			Destroy(this);
			Destroy(gameObject);
			return;
		}
		text = GetComponent<Text>();
		text.text = "";
		gameObject.SetActive(true);

#else
		Destroy(gameObject);

#endif

	}

	/// <summary>
	/// 更新処理
	/// </summary>
	private void Update() {

#if UNITY_DEBUG

		if (logMsg == null) {
			return;
		}

		// 出力された文字列を改行でつなぐ
		string outMessage = "";
		foreach (string msg in logMsg) {
			outMessage += msg + System.Environment.NewLine;
		}

		if (chaseLog == null) {
			return;
		}

		outMessage += System.Environment.NewLine;
		foreach (StringList msg in chaseLog) {
			outMessage += msg.str + System.Environment.NewLine;
		}

		text.text = outMessage;

#endif
	}

	public class StringList {

		string _str;
		public string str {
			set { _str = value; }
			get { return _str; }
		}


		bool _used;
		public bool used {
			set { _used = value; }
			get { return _used; }
		}

		public StringList(string _strdata) {
			_str = _strdata;
			_used = true;
		}
	}

}
