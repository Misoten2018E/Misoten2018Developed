using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FocusCamera : PauseSupport ,IFGameEndEvent ,IFGameEndProduceCheck{


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float OrthoMinSize = 5;

	[SerializeField] private AnimationCurve MoveEvent;

	[SerializeField] private AnimationCurve CameraLimitArea;

	[SerializeField] private float MaxOrthoSize = 25;

	[SerializeField] private List<Transform> TargetTransform;

	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// ターゲットの追加
	/// </summary>
	/// <param name="trs"></param>
	public void AddTarget(Transform trs) {

		if (trs != null) {
		//	print("ターゲット追加");
			TargetTransform.Add(trs);
		}
	}

	/// <summary>
	/// ターゲットの削除
	/// </summary>
	/// <param name="trs"></param>
	public void DeleteTarget(Transform trs) {

		if (trs != null) {
		//	print("ターゲット削除");
			TargetTransform.Remove(trs);
		}
	}


	Camera _MainCamera;
	public Camera MainCamera {
		private set { _MainCamera = value; }
		get {
			if (_MainCamera == null) {
				_MainCamera = GetComponent<Camera>();
			}
			return _MainCamera;
		}
	}

	/// <summary>
	/// 特殊なポーズ処理
	/// </summary>
	public override void OnPause() {
		this.enabled = false;
	}

	/// <summary>
	/// 特殊なポーズ処理
	/// </summary>
	public override void OnResume() {
		this.enabled = true;
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {
		OffsetPos = transform.position;

		NormalUpdate();

		DebugInit();

		EventManager<IFGameEndEvent>.Instance.SetObject(this);
		EventManager<IFGameEndProduceCheck>.Instance.SetObject(this);
	}

	int DebugId = -1;
	void DebugInit() {
		DebugId = DebugLog.ChaseLog("");
	}

	void DebugUpdate() {
		string s = "位置" + NowFrameData.Position.ToString() + "カメラサイズ" + NowFrameData.OrthoSize.ToString();
		DebugLog.ChaseLog(s, DebugId);
	}

	// Update is called once per frame
	void Update() {

		// 現フレームのデータ計算
		NowFrameData.square = MostFarTransformPosition();
		NowFrameData.Position = NowFrameData.square.Center3Dxz;
		NowFrameData.OrthoSize = CalcCameraOrthoSize(NowFrameData.square);

		DebugUpdate();

		//　そもそも大きく動くイベント中なら
		if (isEventActive) {

			// 大きく動くべきターゲットと現状を比較して更に移動が必要なら
			if (isEventStart(NowFrameData,EventTargetData)) {
			//	DebugLog.log("イベント中に更に移動する");
				CreateEventData(NowFrameData);
			}
			// イベントの続行
			else {
				EventUpdate();
			}
		}
		else {

			// 前フレームと比較して大きく動く必要が有るかどうかをチェック
			if (isEventStart(NowFrameData,OldData)) {
			//	DebugLog.log("イベント開始");
				CreateEventData(NowFrameData);
			}
			else {

				// 大きく動く必要が無いなら微更新
				NormalUpdate();
			}
		}

		// 過去データ保持
		OldData = NowFrameData;
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	// 初期カメラ位置の保持
	private Vector3 OffsetPos = new Vector3();

	private NecessaryBuffData OldData = new NecessaryBuffData();

	private NecessaryBuffData EventTargetData = new NecessaryBuffData();

	private NecessaryBuffData NowFrameData = new NecessaryBuffData();

	bool isEventActive = false;

	private float DiffLength = 1f;
	/// <summary>
	/// 特殊更新の必要が有るかどうかをチェック
	/// </summary>
	/// <returns></returns>
	bool isEventStart(NecessaryBuffData nowData ,NecessaryBuffData Target) {

		if (Mathf.Abs(Target.OrthoSize - nowData.OrthoSize) > DiffLength) {
			return true;
		}

		if (Mathf.Abs((Target.Position - nowData.Position).magnitude) > DiffLength) {
			return true;
		}

		return false;
	}


	void CreateEventData(NecessaryBuffData nowData) {

		EventTargetData = nowData;

		// 位置補完データ作成
		PositionComp.Initialize(transform.position - OffsetPos, nowData.Position);

		// サイズ補完データ作成
		SizeComp.Initialize(CameraComp.orthographicSize, nowData.OrthoSize);

		isEventActive = true;
		ElapsedTime = 0f;
	}

	/// <summary>
	/// 通常の微更新
	/// </summary>
	void NormalUpdate() {

		OrthographicSizeUpdate(NowFrameData.square);
		transform.position = NowFrameData.square.Center3Dxz + OffsetPos;
	}

	/// <summary>
	/// イベント時更新
	/// </summary>
	void EventUpdate() {

		float rate = ElapsedTime / EventTime;
		
		if (rate >= 1f) {
			rate = 1f;
			isEventActive = false;
		//	DebugLog.log("イベント終了");
		}
		rate = MoveEvent.Evaluate(rate);

		Vector3 pos = PositionComp.CalcPosition(rate);
		float size = SizeComp.CalcFloat(rate);

		SetOrthoSize(size);
		transform.position = pos + OffsetPos;

		ElapsedTime += Time.deltaTime;
	}

	/// <summary>
	/// カメラOrthographicサイズ更新
	/// </summary>
	/// <param name="mostFar"></param>
	private void OrthographicSizeUpdate(Square mostFar) {

		float size = CalcCameraOrthoSize(mostFar);
		SetOrthoSize(size);
	}

	/// <summary>
	/// 平行投影サイズのセット
	/// </summary>
	/// <param name="size"></param>
	private void SetOrthoSize(float size) {

		CameraComp.orthographicSize = size;
		if (CameraComp.orthographicSize < OrthoMinSize) {
			CameraComp.orthographicSize = OrthoMinSize;
		}
	}

	private const float EventTime = 1.5f;
	private float ElapsedTime;

	/// <summary>
	/// 中心点計算
	/// </summary>
	/// <returns></returns>
	Vector3 CalcTargetCenterPoint() {

		Vector3 pos = new Vector3();

		for (int i = 0; i < TargetTransform.Count; i++) {
			pos += TargetTransform[i].transform.position;
		}

		pos /= TargetTransform.Count;
		return pos;
	}

	/// <summary>
	/// 四方の最大位置検索
	/// </summary>
	/// <returns></returns>
	Square MostFarTransformPosition() {

		Square sq = new Square();

		for (int i = 0; i < TargetTransform.Count; i++) {

			var pos = TargetTransform[i].position;

			if (sq.xzPlus.x < pos.x) {
				sq.xzPlus.x = pos.x;
			}else if (sq.xzMinus.x > pos.x) {
				sq.xzMinus.x = pos.x;
			}

			if (sq.xzPlus.y < pos.z) {
				sq.xzPlus.y = pos.z;
			}
			else if (sq.xzMinus.y > pos.z) {
				sq.xzMinus.y = pos.z;
			}
		}
		return sq;
	}

	/// <summary>
	/// OrthoGraphicのサイズ計算
	/// </summary>
	/// <param name="sq"></param>
	/// <returns></returns>
	float CalcCameraOrthoSize(Square sq) {

		float lenX = sq.xzPlus.x - sq.xzMinus.x;
		float lenZ = sq.xzPlus.y - sq.xzMinus.y;
		// 一応予定の数値を算出
		float len = ((lenX > lenZ) ? (lenX) : (lenZ)) * 0.5f;

		// カーブによる特殊補正を追加
		len += CameraLimitArea.Evaluate(len / MaxOrthoSize) * MaxOrthoSize;
		return len >= MaxOrthoSize ? MaxOrthoSize : len;
	}

	/// <summary>
	/// ゲーム終了時処理
	/// </summary>
	public void GameEnd() {

		TargetTransform.Clear();
		TargetTransform.Add(City.Instance.transform);

		startOrthoSize = CameraComp.orthographicSize;
		endOrthoSize = 1.8f;
		isEndProduceActive = true;

		StartCoroutine(GameObjectExtensions.LoopMethod(2f, ProduceLoop, ProduceEnd));
	}

	float startOrthoSize, endOrthoSize;

	/// <summary>
	/// 終了演出のループ処理
	/// </summary>
	/// <param name="rate"></param>
	void ProduceLoop(float rate) {

		float size = startOrthoSize + (endOrthoSize - startOrthoSize) * rate;
		CameraComp.orthographicSize = size;
	}

	/// <summary>
	/// 演出完全終了時
	/// </summary>
	void ProduceEnd() {
		CameraComp.orthographicSize = endOrthoSize;
		isEndProduceActive = false;
		enabled = false;
	}

	bool isEndProduceActive;

	public bool IsEndProduce() {

		return (!isEndProduceActive);
	}

	Vector3Complession PositionComp = new Vector3Complession();
	floatComplession SizeComp = new floatComplession();
	

	Camera _CameraComp;
	public Camera CameraComp {
		get {
			if (_CameraComp == null) {
				_CameraComp = GetComponent <Camera>();
			}
			return _CameraComp;
		}
	}
      


	/// <summary>
	/// 受け渡し
	/// </summary>
	public struct Square {
		public Vector2 xzPlus, xzMinus;

		public Vector2 Center {
			get {
				return (xzPlus + xzMinus) / 2;
			}
		}
		
		public Vector3 Center3Dxz {
			get {
				var v = Center;
				return new Vector3(v.x, 0, v.y);
			}
		}
	}

	public struct NecessaryBuffData {

		public Vector3 Position;
		public Square square;
		public float OrthoSize;
	}
}
