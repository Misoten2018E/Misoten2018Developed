using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : PlayerBase
{
    private enum PLAYER_TEST_STA
    {
        NORMAL = 0,
        WEAKATTACK1,
        WEAKATTACK2,
        WEAKATTACK3,
        STRONGATTACK,
        SPECIAL,
        DAMAGE,
        PLAYER_TEST_STA_MAX
    }

    MultiInput input;
    PLAYER_TEST_STA player_test_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;

    const int Player_test_MoveSpeed = 5;
    const int Player_test_RotationSpeed = 750;
    const int Player_test_MAXHP = 100;
    const int Player_test_ATTACK = 1;

    //Use this for initialization
    void Start () {
		//使わない方向で
	}
	
	// Update is called once per frame
	void Update () {
		//使わない方向で
	}

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
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();

        MoveSpeed = Player_test_MoveSpeed;
        RotationSpeed = Player_test_RotationSpeed;
        player_test_sta = PLAYER_TEST_STA.NORMAL;
        PlayerSta = (int)player_test_sta;
        ComboFlg = false;
        Attack = Player_test_ATTACK;

        HitAnime.Initialize(this);
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;
        
        switch (player_test_sta)
        {
            case PLAYER_TEST_STA.NORMAL:
                Normal();
                break;
            case PLAYER_TEST_STA.WEAKATTACK1:
                NormalAction1();
                break;
            case PLAYER_TEST_STA.WEAKATTACK2:
                NormalAction2();
                break;
            case PLAYER_TEST_STA.WEAKATTACK3:
                NormalAction3();
                break;
            case PLAYER_TEST_STA.STRONGATTACK:
                StrongAction();
                break;
            case PLAYER_TEST_STA.SPECIAL:
                SpecialAction();
                break;
            case PLAYER_TEST_STA.DAMAGE:
                DamageAction();
                break;
        }

        NoDameCnt();
        SetAnimatorData();
    }

    public override void PlayerDamage(HitObjectImpact damage)
    {
        if (nodamageflg) return;

        player_test_sta = PLAYER_TEST_STA.DAMAGE;
        PlayerSta = (int)player_test_sta;
        damage.DamageHp(HP);
        animator.SetTrigger("Damage");
        nodamageflg = true;
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0,1,0);

        if(input.GetButtonSquareTrigger())
        {
            player_test_sta = PLAYER_TEST_STA.WEAKATTACK1;
            PlayerSta = (int)player_test_sta;
            HitAnime.HitAnimationWeakattack1();
        }

        if (input.GetButtonTriangleTrigger())
        {
            player_test_sta = PLAYER_TEST_STA.STRONGATTACK;
            PlayerSta = (int)player_test_sta;
            HitAnime.HitAnimationStrongattack();
        }

        if (input.GetButtonCircleTrigger())
        {
            player_test_sta = PLAYER_TEST_STA.SPECIAL;
            PlayerSta = (int)player_test_sta;
            HitAnime.HitAnimationSpecial();
        }

        MoveCharacter();
    }

    private void NormalAction1()
    {
        RotationCharacter();

        if (input.GetButtonSquareTrigger() && ComboFlg == false)
        {
            ComboFlg = true;
        }

        if (CheckAnimationEND("NormalAction1"))
        {
            if(ComboFlg)
            {
                player_test_sta = PLAYER_TEST_STA.WEAKATTACK2;
                PlayerSta = (int)player_test_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack2();
            }
            else
            {
                player_test_sta = PLAYER_TEST_STA.NORMAL;
                PlayerSta = (int)player_test_sta;
            }
        }
    }

    private void NormalAction2()
    {
        RotationCharacter();

        if (input.GetButtonSquareTrigger() && ComboFlg == false)
        {
            ComboFlg = true;
        }

        if (CheckAnimationEND("NormalAction2"))
        {
            if(ComboFlg)
            {
                player_test_sta = PLAYER_TEST_STA.WEAKATTACK3;
                PlayerSta = (int)player_test_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack3();
            }
            else
            {
                player_test_sta = PLAYER_TEST_STA.NORMAL;
                PlayerSta = (int)player_test_sta;
            }
        }
    }

    private void NormalAction3()
    {
        RotationCharacter();

        if (CheckAnimationEND("NormalAction3"))
        {
            player_test_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_test_sta;
        }
    }

    private void StrongAction()
    {
        RotationCharacter();

        if (CheckAnimationEND("StrongAction"))
        {
            player_test_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_test_sta;
        }
    }

    private void SpecialAction()
    {
        RotationCharacter();

        if (CheckAnimationEND("SpecialAction"))
        {
            player_test_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_test_sta;
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_test_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_test_sta;
        }
    }

}
