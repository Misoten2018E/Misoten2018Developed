using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : PlayerBase{

    private enum PLAYER_HERO_STA
    {
        NORMAL = 0,
        WEAKATTACK1,
        WEAKATTACK2,
        WEAKATTACK3,
        STRONGATTACK_START,
        STRONGATTACK_END,
        SPECIAL_START,
        SPECIAL_END,
        DAMAGE,
        PAUSE,
        PLAYER_TEST_STA_MAX
    }

    MultiInput input;
    PLAYER_HERO_STA player_Hero_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;
    TrailBodyManager TBM;

	[System.Serializable]
	public class CurveData {

		public AnimationCurve JumpSpeedCurve;
		public AnimationCurve JumpYCurve;

		public AnimationCurve StrongMoveForward;
		public float MoveLength;

		public AnimationCurve AttackMoveCurve;
	}

	[SerializeField] CurveData Curves;

    const int Player_Hero_MoveSpeed = 5;
    const int Player_Hero_RotationSpeed = 750;
    const int Player_Hero_ActionRotationSpeed = 500;
    const int Player_Hero_ATTACK = 1;
    const float WarpPos_X = 0.5f;

	// 12/18 江戸　使わないならコメントアウト
    //Use this for initialization
    //void Start()
    //{
    //    //使わない方向で
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //使わない方向で
    //}

    override
    public void Playerinit(GameObject playerobj)
    {
        myPlayer = playerobj;
        Player player = playerobj.GetComponent<Player>();
        no = player.no;
        input = GetComponent<MultiInput>();
        input.PlayerNo = no;
        CharCon = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Model = transform.Find("BaseModel_Hero").transform;
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();
        TBM = GetComponentInChildren<TrailBodyManager>();
        RootPos = Model.Find("Character1_Reference").GetComponent<Transform>().Find("Character1_Hips").GetComponent<Transform>();

        MoveSpeed = Player_Hero_MoveSpeed;
        RotationSpeed = Player_Hero_RotationSpeed;
        player_Hero_sta = PLAYER_HERO_STA.NORMAL;
        PlayerSta = (int)player_Hero_sta;
        ComboFlg = false;
        Attack = Player_Hero_ATTACK;
        nodamageflg = false;
        CharCon.center = new Vector3(0, 10 + no * 3, 0);

        HitAnime.Initialize(this);
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;

        switch (player_Hero_sta)
        {
            case PLAYER_HERO_STA.NORMAL:
                Normal();
                break;
            case PLAYER_HERO_STA.WEAKATTACK1:
                NormalAction1();
                break;
            case PLAYER_HERO_STA.WEAKATTACK2:
                NormalAction2();
                break;
            case PLAYER_HERO_STA.WEAKATTACK3:
                NormalAction3();
                break;
            case PLAYER_HERO_STA.STRONGATTACK_START:
                StrongActionStart();
                break;
            case PLAYER_HERO_STA.STRONGATTACK_END:
                StrongActionEnd();
                break;
            case PLAYER_HERO_STA.SPECIAL_START:
				SpecialActionStart();
                break;
            case PLAYER_HERO_STA.SPECIAL_END:
        //        SpecialActionEnd();
                break;
            case PLAYER_HERO_STA.DAMAGE:
                DamageAction();
                break;
            case PLAYER_HERO_STA.PAUSE:
                PauseAction();
                break;
        }

        NoDameCnt();

        SetAnimatorData();
    }

    public override void PlayerDamage(HitObjectImpact damage)
    {
        if (nodamageflg) return;

        player_Hero_sta = PLAYER_HERO_STA.DAMAGE;
        PlayerSta = (int)player_Hero_sta;
        damage.DamageHp(HP);
        animator.SetTrigger("Damage");
        nodamageflg = true;
        ModelTransformReset();
        TBM.EndTrail();
    }

    public override void PlayerDamageMotion()
    {
        player_Hero_sta = PLAYER_HERO_STA.DAMAGE;
        PlayerSta = (int)player_Hero_sta;
        animator.SetTrigger("Damage");
        ModelTransformReset();
    }

    public override void PlayerPause()
    {
        player_Hero_sta = PLAYER_HERO_STA.PAUSE;
        PlayerSta = (int)player_Hero_sta;
        animator.SetTrigger("Pause");
        ModelTransformReset();
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0, 0, 0);
        RotationSpeed = Player_Hero_RotationSpeed;

        if (input.GetButtonSquareTrigger())
        {
            player_Hero_sta = PLAYER_HERO_STA.WEAKATTACK1;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationWeakattack1(Attack);
            ModelTransformReset();
            TBM.StartTrail(TrailSupport.BodyType.RightArm);
            RotationSpeed = Player_Hero_ActionRotationSpeed;
            SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light1, transform.position);

			// 12/20　江戸　移動の追加
			ieActionCort = IEAttackMove(Curves.AttackMoveCurve, 0.5f);
			StartCoroutine(ieActionCort);
        }

        if (input.GetButtonTriangleTrigger())
        {
            player_Hero_sta = PLAYER_HERO_STA.STRONGATTACK_START;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
            RotationSpeed = Player_Hero_ActionRotationSpeed;
            SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Strong_Jump, transform.position);
        }

        if (input.GetButtonCircleTrigger())
        {
            player_Hero_sta = PLAYER_HERO_STA.SPECIAL_START;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationSpecial();
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_SP_Jump, transform.position);
        }

        MoveCharacter();

        if (CheckAnimationSTART("wait") || CheckAnimationSTART("run"))
        {
            ModelTransformReset();
        }
    }

    private void NormalAction1()
    {
        RotationCharacter();

        if (input.GetButtonSquareTrigger() && ComboFlg == false)
        {
            ComboFlg = true;
        }

        if (CheckAnimationEND("Combo1"))
        {
            if (ComboFlg)
            {
                player_Hero_sta = PLAYER_HERO_STA.WEAKATTACK2;
                PlayerSta = (int)player_Hero_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack2(Attack);
                TBM.EndTrail(TrailSupport.BodyType.RightArm);
                TBM.StartTrail(TrailSupport.BodyType.RightArm); // 12/20 江戸　攻撃の腕が変わっているので変更
                SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light2, transform.position);

				// 12/20　江戸　移動の追加
				ieActionCort = IEAttackMove(Curves.AttackMoveCurve, 0.5f);
				StartCoroutine(ieActionCort);
			}
            else
            {
                player_Hero_sta = PLAYER_HERO_STA.NORMAL;
                PlayerSta = (int)player_Hero_sta;
                TBM.EndTrail(TrailSupport.BodyType.RightArm);
            }

            ModelTransformReset();
        }
    }

    private void NormalAction2()
    {
        RotationCharacter();

        if (input.GetButtonSquareTrigger() && ComboFlg == false)
        {
            ComboFlg = true;
        }

        if (CheckAnimationEND("Combo2"))
        {
            if (ComboFlg)
            {
                player_Hero_sta = PLAYER_HERO_STA.WEAKATTACK3;
                PlayerSta = (int)player_Hero_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack3(Attack);
                TBM.EndTrail(TrailSupport.BodyType.RightArm); // 12/20 江戸　攻撃の腕が変わっているので変更
				TBM.StartTrail(TrailSupport.BodyType.RightLeg);
                SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light3, transform.position);

				// 12/20　江戸　移動の追加
				ieActionCort = IEAttackMove(Curves.AttackMoveCurve, 0.5f);
				StartCoroutine(ieActionCort);
			}
            else
            {
                player_Hero_sta = PLAYER_HERO_STA.NORMAL;
                PlayerSta = (int)player_Hero_sta;
                TBM.EndTrail(TrailSupport.BodyType.LeftArm);
            }

            ModelTransformReset();
        }
    }

    private void NormalAction3()
    {
        RotationCharacter();

        if (CheckAnimationEND("Combo3"))
        {
            player_Hero_sta = PLAYER_HERO_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
            TBM.EndTrail(TrailSupport.BodyType.RightLeg);
        }
    }

    private void StrongActionStart()
    {
        RotationCharacter();
        
        if (CheckAnimationEND("StrongAttack_start"))
        {
            player_Hero_sta = PLAYER_HERO_STA.STRONGATTACK_END;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationStrongattack(Attack);

			// 12/18 江戸追記
			ieActionCort = IEMoveStrongAction();
			StartCoroutine(ieActionCort);
			ModelTransformReset();
        }
    }

    private void StrongActionEnd()
    {
       
        if (CheckAnimationEND("StrongAttack_end"))
        {
            player_Hero_sta = PLAYER_HERO_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void SpecialActionStart()
    {

		//if (CheckAnimationEND("Special_start"))
		if (CheckAnimationENDforRate("Special_start" ,0.7f)) {
            player_Hero_sta = PLAYER_HERO_STA.SPECIAL_END;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();

			// 12/18　江戸コメントアウト
			// 一瞬での移動を止めるため
			//   GameObject obj = PlayerManager.instance.GetLowHPPlayerObj(no);
			//   Vector3 warppos = new Vector3(obj.transform.position.x + WarpPos_X, 0.0f, obj.transform.position.z);
			//   transform.position = warppos;

			GameObject obj = PlayerManager.instance.GetHerotarget(no);
			ieActionCort = IEMoveSpecialAction(obj.transform.position);
			StartCoroutine(ieActionCort);
		}
    }

    private void SpecialActionEnd()
    {
      
        if (CheckAnimationEND("Special_end"))
        {
            player_Hero_sta = PLAYER_HERO_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_SP_Landed, transform.position);
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Hero_sta = PLAYER_HERO_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
			ienumeratorReset();	//12/18 江戸追記
            ModelTransformReset();
        }
    }

    private void PauseAction()
    {
        if (CheckAnimationEND("Pause"))
        {
            player_Hero_sta = PLAYER_HERO_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    public override bool PlayerIsDeath()
    {
        bool res = false;

        if (HP.isDeath && player_Hero_sta != PLAYER_HERO_STA.DAMAGE)
        {
            res = true;
        }

        return res;
    }



	//========================================================================================
	//                                    江戸追加
	//========================================================================================

	IEnumerator ieActionCort;

	/// <summary>
	/// 目標位置への移動
	/// 12/18 江戸 追加
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	private IEnumerator IEMoveSpecialAction(Vector3 target) {

		Vector3Complession Vec3Comp = new Vector3Complession(transform.position, target);
		TBM.StartTrail(TrailSupport.BodyType.Body);

		const float MaxTime = 0.6f;
		float time = 0f;

		const float YPlus = 4f;

		while (true) {

			time += Time.deltaTime;
			if (time >= MaxTime) {
				break;
			}

			float rate = Curves.JumpSpeedCurve.Evaluate(time / MaxTime);

			Vector3 pos = Vec3Comp.CalcPosition(rate);
			pos.y += Curves.JumpYCurve.Evaluate(rate) * YPlus;
			transform.position = pos;
			
			yield return null;
		}

		transform.position = Vec3Comp.CalcPosition(1f);

		TBM.EndTrail(TrailSupport.BodyType.Body);

		player_Hero_sta = PLAYER_HERO_STA.NORMAL;
		PlayerSta = (int)player_Hero_sta;

		ieActionCort = null;
		ModelTransformReset();
	}

	/// <summary>
	/// 強攻撃移動
	/// 12/18 江戸 追加
	/// </summary>
	/// <returns></returns>
	private IEnumerator IEMoveStrongAction() {

		TBM.StartTrail(TrailSupport.BodyType.RightLeg);
		SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Strong_Kick, transform.position);

		const float MaxTime = 0.5f;
		float time = 0f;


		while (true) {

			time += Time.deltaTime;
			if (time >= MaxTime) {
				break;
			}

			Vector3 move = transform.forward * Curves.StrongMoveForward.Evaluate(time / MaxTime) * Curves.MoveLength;
			CharCon.Move(move);

			yield return null;
		}

		ieActionCort = null;
		SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Strong_Explosion, transform.position);
		TBM.EndTrail(TrailSupport.BodyType.RightLeg);
	}

	/// <summary>
	/// 通常攻撃時移動
	/// 12/20 江戸追加
	/// </summary>
	/// <param name="MaxTime"></param>
	/// <returns></returns>
	private IEnumerator IEAttackMove(AnimationCurve curve, float MaxTime) {

		float time = 0f;
		const float Length = 0.1f;

		while (true) {

			time += Time.deltaTime;
			if (time >= MaxTime) {
				break;
			}

			Vector3 move = transform.forward * curve.Evaluate(time / MaxTime) * Length;
			CharCon.Move(move);

			yield return null;
		}
		ieActionCort = null;
	}

	/// <summary>
	/// 途中で攻撃などで中断された場合
	/// </summary>
	private void ienumeratorReset() {

		if (ieActionCort != null) {
			StopCoroutine(ieActionCort);
			ieActionCort = null;

			TBM.EndTrail();

			transform.position += Model.localPosition;
			ModelTransformReset();
		}
	}
}
