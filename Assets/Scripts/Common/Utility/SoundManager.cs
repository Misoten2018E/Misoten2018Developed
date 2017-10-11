using UnityEngine;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections;


namespace EDO {

	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour {


		//==========================================================
		//						定数定義
		//==========================================================
		public enum BGMType {
			TITLE = 0,
			GAME1,
			GAME2,
			GAME3,
			GAME4,
			GAME5,
			BGMMAX
		}

		public enum SEType {
			OK,
			SEMAX
		}

		const string ROOTNAME = "Sound/";
		const string ROOTNAME_SE = ROOTNAME + "SE/";
		const string ROOTNAME_BGM = ROOTNAME + "BGM/";

		[SerializeField] private SoundName Sound;


		//==========================================================
		//						メンバ
		//==========================================================


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

			Clip_bgm = new List<BGMPlayer>((int)BGMType.BGMMAX);
			Clip_bgm.AddRange(new BGMPlayer[(int)BGMType.BGMMAX]);

			Sound.Copy();

			Init();
		}

		void Init() {

			for (int i = 0; i < (int)SEType.SEMAX; i++) {

				string directry = ROOTNAME_SE + Sound.SoundEffects[i];
				var se = Resources.Load<AudioClip>(directry);
				Clip_se.Add(se);

				if (se == null) {
					Debug.LogWarning("読み込み失敗 : " + directry);
				}
				//else {
				//	print("読み込み成功 : " + directry);
				//}
			}

			for (int i = 0; i < (int)BGMType.BGMMAX; i++) {

				//空でないなら次へ
				if (Clip_bgm[i] != null) {
					continue;
				}

				string directry = ROOTNAME_BGM + Sound.BGM[i];
				var bgm = Resources.Load<AudioClip>(directry);
				Clip_bgm[i] = new BGMPlayer(bgm, transform);

				if (bgm == null) {
					Debug.LogWarning("読み込み失敗 : " + directry);
				}
				//else {
				//	print("読み込み成功 : " + directry);
				//}
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

		public void PlaySE(SEType type) {

			int _type = (int)type;

			if (type < SEType.SEMAX && Clip_se[_type] != null) {

				audioSource.PlayOneShot(Clip_se[_type]);
			}
		}

		public void PlaySELoop(SEType type) {

			int _type = (int)type;

			if (type < SEType.SEMAX && Clip_se[_type] != null) {

				audioSource.PlayOneShot(Clip_se[_type]);
			}
		}

		public IEnumerator PlaySEDelay(float delayTime, SEType seType)	// 7/5 八十川 add
		{
			yield return new WaitForSeconds(delayTime);
			PlaySE(seType);
		}

		public void PlayBGM(BGMType type) {

			int _type = (int)type;

			//異常値でないなら
			if (type < BGMType.BGMMAX) {

				//空なら読み込み
				if (Clip_bgm[_type] == null) {
					Clip_bgm[_type] = new BGMPlayer((AudioClip)Resources.Load(ROOTNAME_BGM + Sound.BGM[_type]), transform);
				}

				if (Clip_bgm[_type] != null) {
					Clip_bgm[_type].playBGM();
				}
			}
		}

		public void PauseBGM(BGMType type) {
			if (type < BGMType.BGMMAX && Clip_bgm[(int)type] != null) {
				Clip_bgm[(int)type].pauseBGM();
			}
		}

		public void StopBGM(BGMType type, float second = 0f) {
			if (type < BGMType.BGMMAX && Clip_bgm[(int)type] != null) {
				Clip_bgm[(int)type].stopBGM(second);
			}
		}


		void destroy() {

			for (int i = 0; i < (int)SEType.SEMAX; i++) {
				//	Destroy(Clip_se[i]);
				Clip_se[i] = null;
			}

			for (int i = 0; i < (int)BGMType.BGMMAX; i++) {
				Clip_bgm[i].destory();
				Clip_bgm[i] = null;
			}

			Destroy(audioSource);
			audioSource = null;
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
	public class SoundName {

		[SerializeField]
		public List<string> BGM = new List<string>(5);

		List<string> soundEffects = new List<string>();
		public List<string> SoundEffects {
			get { return soundEffects; }
		}

		[SerializeField]
		public string TouchStart;

		[SerializeField]
		public string ChangeStage;

		[SerializeField]
		public string TouchJerry;

		[SerializeField]
		public string pressJerry;

		[SerializeField]
		public string releaseJerry;

		[SerializeField]
		public string hitJerry;

		[SerializeField]
		public string creationJerry;

		[SerializeField]
		public string breakJerry;

		[SerializeField]
		public string drawJerry;

		[SerializeField]
		public string clearJerry;

		[SerializeField]
		public string complateJerry;

		[SerializeField]
		public string hitGrass;

		[SerializeField]
		public string areaClear;

		[SerializeField]
		public string stageClear;

		[SerializeField]
		public string stageSlide;	// 7/4 八十川 Add

		[SerializeField]
		public string pauseMenu;	// 7/4 八十川 Add

		[SerializeField]
		public string glassBreak;	// 7/5 八十川 Add

		public void Copy() {
			soundEffects.Add(TouchStart);
			soundEffects.Add(ChangeStage);
			soundEffects.Add(TouchJerry);
			soundEffects.Add(pressJerry);
			soundEffects.Add(releaseJerry);
			soundEffects.Add(hitJerry);
			soundEffects.Add(creationJerry);
			soundEffects.Add(breakJerry);
			soundEffects.Add(drawJerry);
			soundEffects.Add(clearJerry);
			soundEffects.Add(complateJerry);
			soundEffects.Add(hitGrass);
			soundEffects.Add(areaClear);
			soundEffects.Add(stageClear);
			soundEffects.Add(stageSlide);	// 7/4 八十川 Add
			soundEffects.Add(pauseMenu);	// 7/4 八十川 Add
			soundEffects.Add(glassBreak);	// 7/5 八十川 Add
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