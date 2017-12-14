using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour {

	private System.Action m_resultHandler;        // キャプチャーが完了したときに呼び出すハンドラー
	private Texture2D m_texture;              // テクスチャー
	private bool m_isSavedScreenCapture; // キャプチャー画像を保存済みか？
	private int m_ignoreLayer;          // 無視するレイヤー

	//-------------------------------------------------------------
	//! 画面キャプチャー後の処理を設定します.
	//-------------------------------------------------------------
	public void SetResultHandler(System.Action _action) {
		m_resultHandler = _action;
	}

	//-------------------------------------------------------------
	//! 画面キャプチャー対象外のレイヤーを設定します.
	//-------------------------------------------------------------
	public void SetIgnoreLayer(int _ignoreLayer) {
		m_ignoreLayer = _ignoreLayer;
	}

	//-------------------------------------------------------------
	//! キャプチャー画像を保存済みか確認します
	//-------------------------------------------------------------
	public bool IsSavedScreenCapture() {
		return m_isSavedScreenCapture;
	}

	//-------------------------------------------------------------
	//! キャプチャーした画像のテクスチャを取得します.
	//-------------------------------------------------------------
	public Texture2D GetTexture() {
		return m_texture;
	}

	//-------------------------------------------------------------
	//! 起動.
	//-------------------------------------------------------------
	public virtual void Awake() {
		m_resultHandler = null;
		m_texture = null;
		m_isSavedScreenCapture = false;
	}

	//-------------------------------------------------------------
	//! 更新.
	//-------------------------------------------------------------
	public virtual void Update() {
		if (m_isSavedScreenCapture) {
			return;
		}

		// キャプチャー
		Take();

		// 通知
		if (m_resultHandler != null) {
			m_resultHandler();
		}

		// 破棄
		//      Destroy(this);
	}

	//-------------------------------------------------------------
	//! 画面をキャプチャーします.
	//-------------------------------------------------------------
	private void Take() {
		Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);

		int cameraNum = Camera.allCamerasCount;
		for (int index = 0; index < cameraNum; index++) {
			Camera cam = Camera.allCameras[index];

			if ((cam.cullingMask & m_ignoreLayer) == 0) {
				continue;
			}

			RenderTexture prev = cam.targetTexture;
			cam.targetTexture = rt;
			cam.Render();
			cam.targetTexture = prev;
		}

		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
		screenShot.Apply();

		m_texture = screenShot;
		m_isSavedScreenCapture = true;
	}

	const string LayerMaskStrings = "UI";

	/// <summary>
	/// カメラからテクスチャ生成
	/// </summary>
	/// <param name="cam"></param>
	/// <returns></returns>
	static public Texture2D Capture(Camera cam ,int texWidth,int texHeight) {

		Texture2D screenShot = new Texture2D(texWidth, texHeight, TextureFormat.RGB24, false);
		RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);

		RenderTexture prev = cam.targetTexture;
		cam.targetTexture = rt;
		int mask = LayerMask.GetMask(LayerMaskStrings);
		cam.cullingMask = 0xffff ^ mask;
		cam.Render();
		cam.targetTexture = prev;

		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
		screenShot.Apply();

		return screenShot;
	}


	/// <summary>
	/// カメラからテクスチャ生成
	/// </summary>
	/// <param name="cam"></param>
	/// <returns></returns>
	static public Texture2D Capture(Camera cam) {

		return Capture(cam, Screen.width, Screen.height);
	}
}
