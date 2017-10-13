using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDO {

	[DisallowMultipleComponent]
	public class TimeManager : SceneStartEvent , IFGameStartEvent,IFGameEndProduceEvent {


		//========================================================================================
		//                                    public
		//========================================================================================


		//========================================================================================
		//                                    public
		//========================================================================================

		/// <summary>
		/// 現在時間を返す
		/// </summary>
		float _TimeCount;
		public float TimeCount {
			private set { _TimeCount = value; }
			get { return _TimeCount; }
		}

		static public TimeManager Instance {
			get { return myInstance; }
		}

		static TimeManager myInstance;

#if UNITY_DEBUG
		int LogId = -1;
#endif

		//========================================================================================
		//                                    public - override
		//========================================================================================


		public override void SceneMyInit() {
			myInstance = this;
			base.SceneMyInit();
			OnPause();
		}

		public override void SceneOtherInit() {

#if UNITY_DEBUG

			LogId = DebugLog.ChaseLog("", LogId);
#endif
		}

		public void GameStart() {
			OnResume();
			TimeCount = 0f;
		}

		public void EndProduce() {
			enabled = false;
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update() {

			if (!isInitialized) {
				return;
			}

			TimeCount += Time.deltaTime;


			DebugDraw();
		}


		//========================================================================================
		//                                    private
		//========================================================================================

		void DebugDraw() {

#if UNITY_DEBUG

			string str = string.Format("現在の時間 : {0:f2} ",TimeCount);
			DebugLog.ChaseLog(str, LogId);
#endif
		}

		

		bool _GameEnd;
		public bool GameEnd {
			private set { _GameEnd = value; }
			get { return _GameEnd; }
		}
	}

}