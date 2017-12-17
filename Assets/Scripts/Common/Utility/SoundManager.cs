using UnityEngine;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections;



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
		// ジングル
		TitlePlayerOK,
		GameStart,
		StandartWaveStart,
		BossStart,
		WindowPop,
		CityLevelUp,

		// 汎用
		Common_Move,
		Common_CityBuild,
		Common_Avoid,

		// ヒーロー
		Hero_Light1,
		Hero_Light2,
		Hero_Light3,
		Hero_Strong_Jump,
		Hero_Strong_Kick,
		Hero_Strong_Explosion,
		Hero_SP_Jump,
		Hero_SP_Landed,
		Hero_Hit_Panch,
		Hero_Hit_kick,

		// 悪役
		Heel_Light1,
		Heel_Light2,
		Heel_Light3,
		Heel_Strong,
		Heel_SP_Catch,
		Heel_SP_Pull,
		Heel_Hit_Light,
		Heel_Hit_Strong,

		// 演出役
		Specialist_Light1,
		Specialist_Light2,
		Specialist_Light3,
		Specialist_Strong_Set,
		Specialist_Strong_Exp,
		Specialist_SP_Megaphone,
		Specialist_SP_PowerUp,
		Specialist_Hit_Light,
		Specialist_Hit_Strong,

		// 敵系
		Mob_Attack,
		Mob_HitAttack,
		Boss_Attack1,
		Boss_Attack2,
		Boss_Attack3,
		Boss_HitAttack,
		Boss_JumpMove,
		Boss_Landed,
		Boss_Howling,

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

		// ジングル追加
		Clip_se.AddRange(SoundEffect.jingle.CreateList());

		// 汎用系追加
		Clip_se.AddRange(SoundEffect.common.CreateList());

		// ヒーロー系追加
		Clip_se.AddRange(SoundEffect.Hero.CreateList());

		// 悪役系追加
		Clip_se.AddRange(SoundEffect.evil.CreateList());

		// 演出役系追加
		Clip_se.AddRange(SoundEffect.effecter.CreateList());

		// 敵系追加
		Clip_se.AddRange(SoundEffect.enemy.CreateList());

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
	public void PlaySE(SEType type, Vector3 position) {

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


	IEnumerator IEPlaySEDelay(SEType seType, Vector3 position, float delayTime) {
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

	public Player Hero;
	public Evil evil;
	public Effecter effecter;
	public Enemy enemy;
	public Common common;
	public Jingle jingle;

	[System.Serializable]
	public struct Player {

		public AudioClip Attack_Light1;
		public AudioClip Attack_Light2;
		public AudioClip Attack_Light3;
		public AudioClip Attack_Strong_Jump;
		public AudioClip Attack_Strong_Kick;
		public AudioClip Attack_Strong_Explosion;

		public AudioClip Special_Jump;
		public AudioClip Special_Landed;
		public AudioClip HitAttack_Panch;
		public AudioClip HitAttack_Kick;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(Attack_Light1);
			list.Add(Attack_Light2);
			list.Add(Attack_Light3);
			list.Add(Attack_Strong_Jump);
			list.Add(Attack_Strong_Kick);
			list.Add(Attack_Strong_Explosion);
			list.Add(Special_Jump);
			list.Add(Special_Landed);
			list.Add(HitAttack_Panch);
			list.Add(HitAttack_Kick);

			return list;
		}
	}

	[System.Serializable]
	public struct Evil {
		public AudioClip Attack_Light1;
		public AudioClip Attack_Light2;
		public AudioClip Attack_Light3;
		public AudioClip Attack_Strong;

		public AudioClip Special_Catch;
		public AudioClip Special_Pull;
		public AudioClip HitAttack_Light;
		public AudioClip HitAttack_Strong;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(Attack_Light1);
			list.Add(Attack_Light2);
			list.Add(Attack_Light3);
			list.Add(Attack_Strong);
			list.Add(Special_Catch);
			list.Add(Special_Pull);
			list.Add(HitAttack_Light);
			list.Add(HitAttack_Strong);

			return list;
		}
	}

	[System.Serializable]
	public struct Effecter {
		public AudioClip Attack_Light1;
		public AudioClip Attack_Light2;
		public AudioClip Attack_Light3;
		public AudioClip Attack_Strong_Set;
		public AudioClip Attack_Strong_Explosion;

		public AudioClip Special_Megaphone;
		public AudioClip Special_PowerUp;
		public AudioClip HitAttack_Light;
		public AudioClip HitAttack_Strong;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(Attack_Light1);
			list.Add(Attack_Light2);
			list.Add(Attack_Light3);
			list.Add(Attack_Strong_Set);
			list.Add(Attack_Strong_Explosion);
			list.Add(Special_Megaphone);
			list.Add(Special_PowerUp);
			list.Add(HitAttack_Light);
			list.Add(HitAttack_Strong);

			return list;
		}
	}


	[System.Serializable]
	public struct Enemy {
		public AudioClip Mob_Attack;
		public AudioClip Mob_HitAttack;
		public AudioClip Boss_Attack1;
		public AudioClip Boss_Attack2;
		public AudioClip Boss_Attack3;
		public AudioClip Boss_HitAttack;
		public AudioClip Boss_JumpMove;
		public AudioClip Boss_Landed;
		public AudioClip Boss_Howling;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(Mob_Attack);
			list.Add(Mob_HitAttack);
			list.Add(Boss_Attack1);
			list.Add(Boss_Attack2);
			list.Add(Boss_Attack3);

			list.Add(Boss_HitAttack);
			list.Add(Boss_JumpMove);
			list.Add(Boss_Landed);
			list.Add(Boss_Howling);

			return list;
		}
	}

	[System.Serializable]
	public struct Common {
		public AudioClip Run;
		public AudioClip CityBuild;
		public AudioClip Avoidance;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(Run);
			list.Add(CityBuild);
			list.Add(Avoidance);

			return list;
		}
	}

	[System.Serializable]
	public struct Jingle {
		public AudioClip TitlePlayerOK;
		public AudioClip GameStart;
		public AudioClip StandardWaveStart;
		public AudioClip BossStart;
		public AudioClip WindowPop;
		public AudioClip CityLevelUp;

		/// <summary>
		/// リスト作成
		/// </summary>
		/// <returns></returns>
		public List<AudioClip> CreateList() {

			List<AudioClip> list = new List<AudioClip>();

			list.Add(TitlePlayerOK);
			list.Add(GameStart);
			list.Add(StandardWaveStart);
			list.Add(BossStart);
			list.Add(WindowPop);
			list.Add(CityLevelUp);

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

	public BGMPlayer(AudioClip audioClip, Transform parent) {
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