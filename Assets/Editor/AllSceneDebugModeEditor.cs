using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class ScriptableGameObject : ScriptableObject {
	public GameObject prefab;
}

[ExecuteInEditMode]
public class AllSceneDebugModeEditor : EditorWindow {
	static bool isDebug = false;

	static GUIStyle style = new GUIStyle();

	[MenuItem("Tools/全てのシーンのデバッグモード変更 #%&c")]
	public static void ShowWindow() {
		var window = GetWindow(typeof(AllSceneDebugModeEditor), true, "デバッグモード変更");
		window.minSize = new Vector2(400, 100);

		// set gui style
		style.alignment = TextAnchor.MiddleCenter;
		style.fontStyle = FontStyle.Bold;
		style.border = new RectOffset(5, 5, 5, 5);
		style.normal.textColor = Color.green;
	}

	/// <summary>
	/// プレハブかどうかを判定。インスタンスかされていないプロジェクトビュー上のプレハブはこれで検知できる
	/// </summary>
	bool IsPrefab(Object obj) {
		return PrefabUtility.GetPrefabParent(obj) == null && PrefabUtility.GetPrefabObject(obj) != null;
	}

	void OnGUI() {

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Debug Mode");
		isDebug = EditorGUILayout.Toggle(isDebug, GUILayout.Width(16));
		GUILayout.EndHorizontal();


		if (GUILayout.Button("全てのシーンの情報変更")) {

			// 今のシーンが編集中の場合、他のシーンを開くと変更が破棄されてしまうため、セーブしておく
			if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
				Debug.Log("Process cancelled.");
				return;
			}

			// 今開いているシーンを覚えておく
			var currentScenePath = EditorSceneManager.GetActiveScene().path;

			// 全てのシーンに対応
			var scenes = EditorBuildSettings.scenes;
			var copiedCount = 0;
			foreach (var scene in scenes) {
				EditorSceneManager.OpenScene(scene.path);

				var objs = GameObjectExtensions.FindObjectOfInterface<DebuggableObject>();
				for (int i = 0; i < objs.Count; i++) {
					objs[i].Debug(isDebug);
				}

				// 生成してセーブ
				copiedCount++;
				EditorSceneManager.MarkAllScenesDirty();
				EditorSceneManager.SaveOpenScenes();
			}

			Debug.LogFormat("'{0}' 個のシーンを変更, シーン合計:{1}", copiedCount, scenes.Length);

			// 最後に、実行前に開いていたシーンを開く
			EditorSceneManager.OpenScene(currentScenePath);
		}
	}
}