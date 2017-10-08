using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ErrorDialog : MonoBehaviour {

	string _condition = "";
	string _stackTrace = "";
	string _type = "";
	string num = "";
	string res = "";

	private void Start() {

	}

	void OnEnable() {
		Application.logMessageReceived += (HandleLog);

	}

	void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string condition, string stackTrace, LogType type) {
		_condition = condition;
		_stackTrace = stackTrace;
		_type = type.ToString();

		var str = CreateDateString();

#if ERROR_SCREENSHOT
		textSave("condition : " + _condition + "\nstackTrace : " + _stackTrace + "\ntype : " + _type, str);
#endif
		TextureSave(str);
		
	}

	void TextureSave(string texName) {

		var tex = CameraCapture.Capture(Camera.main);
		tex.name = texName;

		// ファイルダイアログの表示.
		string filePath = EditorUtility.SaveFilePanel("Save Texture", "Log/", tex.name + ".png", "png");

		byte[] pngData = tex.EncodeToPNG();   // pngのバイト情報を取得.
		if (filePath.Length > 0) {
			// pngファイル保存.
			File.WriteAllBytes(filePath, pngData);
		}
		Object.DestroyImmediate(tex);
	}

	private void textSave(string txt ,string name) {
		StreamWriter sw = new StreamWriter("Log/" + name + ".txt", true); //true=追記 false=上書き
		sw.WriteLine(txt);
		sw.Flush();
		sw.Close();
	}

	private string CreateDateString() {

		string str;

		// 取得する値: 月
		str = "m" + System.DateTime.Now.Month.ToString();
		// 取得する値: 日
		str += "_d" + System.DateTime.Now.Day.ToString();
		// 取得する値: 時
		str += "_h" + System.DateTime.Now.Hour.ToString();
		// 取得する値: 分
		str += "_m" + System.DateTime.Now.Minute.ToString();
		// 取得する値: 秒
		str += "_s" + System.DateTime.Now.Second.ToString();
		// 取得する値: コンマミリ秒
		//str += System.DateTime.Now.Millisecond.ToString();
		return str;

	}

	void OnGUI() {

		num = GUI.TextField(new Rect(10, 10, 100, 50), num);
		GUI.Label(new Rect(120, 10, 100, 50), res);
		if (GUI.Button(new Rect(10, 70, 150, 50), "test")) {
			res = (10 / int.Parse(num)).ToString();
		}
		GUI.TextArea(new Rect(10, 130, 300, 200),
			"condition : " + _condition + "\nstackTrace : " + _stackTrace + "\ntype : " + _type);
	}
}
