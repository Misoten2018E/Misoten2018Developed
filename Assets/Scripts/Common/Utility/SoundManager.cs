using UnityEngine;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections;


namespace EDO {

	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour {


		//========================================================================================
		//                                    inspector
		//========================================================================================

		[SerializeField] AudioClip[] StageBGM = new AudioClip[AUDIO_MAX];

		[SerializeField] SE SoundEffect;

		[SerializeField] AudioSource SamplePrefab;

		//==========================================================
		//						定数定義
		//==========================================================
		public enum BGMType {
			TITLE = 0,
			GAME_TUTORIAL,
			GAME_MAIN,
			GAME_BOSS,
			RESULT,
			BGMMAX
		}

		const int AUDIO_MAX = (int)BGMType.BGMMAX;

		public enum SEType {
			GameStart,
			BossStart,
			SEMAX
		}

		const string ROOTNAME = "Sound/";
		const string ROOTNAME_SE = ROOTNAME + "SE/";
		const string ROOTNAME_BGM = ROOTNAME + "BGM/";
		const string ROOTNAME_JINGLE = ROOTNAME + "Jingle/";


		//==========================================================
		//						メンバ
		//==========================================================

		List<AudioSource> sourceList = new List<AudioSource>();

		//========================================================================================
		//                                    public
		//========================================================================================

		static public SoundManager Instance { get { return myInstance; } }

		AudioSource audioSource;
		List<AudioClip> Clip_se;
		List<BGMPlayer> Clip_bgm;

		/// <summary>
		/// 初期処理
		/// </summary>
		void Start() {

			Create(this);
			audioSource = GetComponent<AudioSource>();
			Clip_se = new List<AudioClip>();
			//Clip_se.AddRange(new AudioClip[(int)SEType.SEMAX]);

			Clip_bgm = new List<BGMPlayer>();
			Clip_bgm.AddRange(new BGMPlayer[AUDIO_MAX]);

			Init();
		}

		void Init() {

			var list = SoundEffect.systemSE.CreateList();

			// ジングル追加
			for (int i = 0; i < list.Count; i++) {
				Clip_se.Add(list[i]);
			}

			for (int i = 0; i < AUDIO_MAX; i++) {

				//空でないなら次へ
				if (Clip_bgm[i] != null) {
					continue;
				}

				Clip_bgm[i] = new BGMPlayer(StageBGM[i], transform);

				if (StageBGM[i] == null) {
					Debug.LogWarning("読み込み失敗 : " + i);
				}
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update() {
			for (int i = 0; i < (int)BGMType.BGMMAX; i++) {
				if (Clip_bgm[i] != null) {
					Clip_bgm[i].update();
				}
			}
		}

		/// <summary>
		/// 音を鳴らす
		/// 位置を渡すとそこを起点として鳴る
		/// </summary>
		/// <param name="type"></param>
		/// <param name="position"></param>
		public void PlaySE(SEType type ,Vector3 position) {

			int _type = (int)type;

			if (type < SEType.SEMAX && Clip_se[_type] != null) {
				var s = GetNonusedAudioSource();
				s.transform.position = position;
				s.PlayOneShot(Clip_se[_type]);
			}
		}

		/// <summary>
		/// 遅延してSEを鳴らす
		/// </summary>
		/// <param name="seType"></param>
		/// <param name="delayTime"></param>
		public void PlaySE(SEType seType, Vector3 position, float delayTime) {

			StartCoroutine(IEPlaySEDelay(seType, position, delayTime));
		}

		/// <summary>
		/// 音を鳴らす
		/// 位置を渡すとそこを起点として鳴る
		/// </summary>
		/// <param name="type"></param>
		/// <param name="position"></param>
		//public void PlaySELoop(SEType type, Vector3 position, float looptime) {

		//	int _type = (int)type;

		//	if (type < SEType.SEMAX && Clip_se[_type] != null) {

		//		var s = GetNonusedAudioSource();
		//		s.transform.position = position;
		//		s.PlayOneShot(Clip_se[_type]);
		//	}
		//}


		IEnumerator IEPlaySEDelay(SEType seType , Vector3 position, float delayTime)
		{
			yield return new WaitForSeconds(delayTime);
			PlaySE(seType, position);
		}

		public void PlayBGM(BGMType type) {

			int _type = (int)type;

			//異常値でないなら
			if (type < BGMType.BGMMAX) {

				if (Clip_bgm[_type] != null) {
					Clip_bgm[_type].playBGM();
				}
			}
		}

		/// <summary>
		/// 一時停止
		/// </summary>
		/// <param name="type"></param>
		public void PauseBGM(BGMType type) {

			int _type = (int)type;
			if (type < BGMType.BGMMAX && Clip_bgm[_type] != null) {
				Clip_bgm[_type].pauseBGM();
			}
		}

		/// <summary>
		/// 終了
		/// 秒数を渡すとその秒数分猶予を残す
		/// </summary>
		/// <param name="type"></param>
		/// <param name="second"></param>
		public void StopBGM(BGMType type, float second = 0f) {

			int _type = (int)type;
			if (type < BGMType.BGMMAX && Clip_bgm[_type] != null) {
				Clip_bgm[_type].stopBGM(second);
			}
		}


		void destroy() {

			for (int i = 0; i < (int)SEType.SEMAX; i++) {
				Clip_se[i] = null;
			}

			for (int i = 0; i < AUDIO_MAX; i++) {
				Clip_bgm[i].destory();
				Clip_bgm[i] = null;
			}

			Destroy(audioSource);
			audioSource = null;
		}

		/// <summary>
		/// 使用されてないオーディオを受け取る
		/// </summary>
		/// <returns></returns>
		AudioSource GetNonusedAudioSource() {

			for (int i = 0; i < sourceList.Count; i++) {

				// 起動していないなら
				if (!sourceList[i].isPlaying) {
					return sourceList[i];
				}
			}

			var s = Instantiate(SamplePrefab);
			s.transform.SetParent(transform);
			sourceList.Add(s);
			return s;
		}

		//========================================================================================
		//                                    private
		//========================================================================================

		static private SoundManager myInstance;

		private SoundManager() {

		}

		static bool Create(SoundManager obj) {
			if (myInstance == null) {
				myInstance = obj;
				DontDestroyOnLoad(obj);
				return true;
			}
			else {
				Destroy(obj);
				Destroy(obj.gameObject);
				return false;
			}
		}

		static void Desctruct() {
			myInstance.destroy();
			myInstance = null;
		}
	}

	[System.Serializable]
	public struct SE {

		public Player player;
		public Evil evil;
		public Effecter effecter;
		public Common common;
		public Jingle systemSE;

		[System.Serializable]
		public struct Player {

			public AudioClip Attack_Light1;

			/// <summary>
			/// リスト作成
			/// </summary>
			/// <returns></returns>
			public List<AudioClip> CreateList() {

				List<AudioClip> list = new List<AudioClip>();

				list.Add(Attack_Light1);

				return list;
			}
		}

		[System.Serializable]
		public struct Evil {
			public AudioClip Attack_Light1;

			/// <summary>
			/// リスト作成
			/// </summary>
			/// <returns></returns>
			public List<AudioClip> CreateList() {

				List<AudioClip> list = new List<AudioClip>();

				list.Add(Attack_Light1);

				return list;
			}
		}

		[System.Serializable]
		public struct Effecter {
			public AudioClip Attack_Light1;

			/// <summary>
			/// リスト作成
			/// </summary>
			/// <returns></returns>
			public List<AudioClip> CreateList() {

				List<AudioClip> list = new List<AudioClip>();

				list.Add(Attack_Light1);

				return list;
			}
		}

		[System.Serializable]
		public struct Common {
			public AudioClip Run;

			/// <summary>
			/// リスト作成
			/// </summary>
			/// <returns></returns>
			public List<AudioClip> CreateList() {

				List<AudioClip> list = new List<AudioClip>();

				list.Add(Run);

				return list;
			}
		}

		[System.Serializable]
		public struct Jingle {
		//	public AudioClip Decide;
			public AudioClip GameStart;
			public AudioClip BossStart;

			/// <summary>
			/// リスト作成
			/// </summary>
			/// <returns></returns>
			public List<AudioClip> CreateList() {

				List<AudioClip> list = new List<AudioClip>();

		//		list.Add(Decide);
				list.Add(GameStart);
				list.Add(BossStart);

				return list;
			}
		}
	}

	class BGMPlayer {
		// State
		class State {
			protected BGMPlayer bgmPlayer;
			public State(BGMPlayer bgmPlayer) {
				this.bgmPlayer = bgmPlayer;
			}
			public virtual void playBGM() { }
			public virtual void pauseBGM() { }
			public virtual void stopBGM() { }
			public virtual void update() { }
		}

		//待機中遷移クラス
		class Wait : State {

			public Wait(BGMPlayer bgmPlayer) : base(bgmPlayer) { }

			public override void playBGM() {
				if (bgmPlayer.fadeInTime > 0.0f)
					bgmPlayer.state = new FadeIn(bgmPlayer);
				else
					bgmPlayer.state = new Playing(bgmPlayer);
			}
		}

		//フェードイン遷移クラス
		class FadeIn : State {

			float t = 0.0f;

			public FadeIn(BGMPlayer bgmPlayer) : base(bgmPlayer) {
				bgmPlayer.source.Play();
				bgmPlayer.source.volume = 0.0f;
			}

			public override void pauseBGM() {
				bgmPlayer.state = new Pause(bgmPlayer, this);
			}

			public override void stopBGM() {
				bgmPlayer.state = new FadeOut(bgmPlayer);
			}

			public override void update() {
				t += Time.deltaTime;
				bgmPlayer.source.volume = t / bgmPlayer.fadeInTime;
				if (t >= bgmPlayer.fadeInTime) {
					bgmPlayer.source.volume = 1.0f;
					bgmPlayer.state = new Playing(bgmPlayer);
				}
			}
		}

		//再生中遷移クラス
		class Playing : State {

			public Playing(BGMPlayer bgmPlayer) : base(bgmPlayer) {
				if (bgmPlayer.source.isPlaying == false) {
					bgmPlayer.source.volume = 1.0f;
					bgmPlayer.source.Play();
				}
			}

			public override void pauseBGM() {
				bgmPlayer.state = new Pause(bgmPlayer, this);
			}

			public override void stopBGM() {
				bgmPlayer.state = new FadeOut(bgmPlayer);
			}
		}

		//ポーズ中遷移クラス
		class Pause : State {

			State preState;

			public Pause(BGMPlayer bgmPlayer, State preState) : base(bgmPlayer) {
				this.preState = preState;
				bgmPlayer.source.Pause();
			}

			public override void stopBGM() {
				bgmPlayer.source.Stop();
				bgmPlayer.state = new Wait(bgmPlayer);
			}

			public override void playBGM() {
				bgmPlayer.state = preState;
				bgmPlayer.source.Play();
			}
		}

		//フェードアウト遷移クラス
		class FadeOut : State {
			float initVolume;
			float t = 0.0f;

			public FadeOut(BGMPlayer bgmPlayer) : base(bgmPlayer) {
				initVolume = bgmPlayer.source.volume;
			}

			public override void pauseBGM() {
				bgmPlayer.state = new Pause(bgmPlayer, this);
			}

			public override void update() {
				t += Time.deltaTime;
				bgmPlayer.source.volume = initVolume * (1.0f - t / bgmPlayer.fadeOutTime);
				if (t >= bgmPlayer.fadeOutTime) {
					bgmPlayer.source.volume = 0.0f;
					bgmPlayer.source.Stop();
					bgmPlayer.state = new Wait(bgmPlayer);
				}
			}
		}


		GameObject obj;
		AudioSource source;
		State state;
		float fadeInTime = 0.0f;
		float fadeOutTime = 0.0f;

		public BGMPlayer() { }

		public BGMPlayer(AudioClip audioClip,Transform parent) {
			AudioClip clip = audioClip;
			if (clip != null) {
				obj = new GameObject("BGMPlayer");
				source = obj.AddComponent<AudioSource>();
				source.clip = clip;
				source.loop = true;
				state = new Wait(this);

				if (parent) {
					obj.transform.SetParent(parent);
				}
			}
		}

		public void destory() {
			if (source != null)
				GameObject.Destroy(obj);
		}

		public void playBGM() {
			if (source != null) {
				state.playBGM();
			}
		}

		public void playBGM(float fadeTime) {
			if (source != null) {
				this.fadeInTime = fadeTime;
				state.playBGM();
			}
		}

		public void pauseBGM() {
			if (source != null)
				state.pauseBGM();
		}

		public void stopBGM(float fadeTime = 0f) {
			if (source != null) {
				fadeOutTime = fadeTime;
				state.stopBGM();
			}
		}

		public void update() {
			if (source != null)
				state.update();
		}



	}

}