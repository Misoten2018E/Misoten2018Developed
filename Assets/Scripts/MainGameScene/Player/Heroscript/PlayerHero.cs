using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : PlayerBase{

    private enum PLAYER_TEST_STA
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
        PLAYER_TEST_STA_MAX
    }

    MultiInput input;
    PLAYER_TEST_STA player_Hero_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;

    const int Player_Hero_MoveSpeed = 5;
    const int Player_Hero_RotationSpeed = 750;
    const int Player_Hero_ActionRotationSpeed = 500;
    const int Player_Hero_ATTACK = 1;

    //Use this for initialization
    void Start()
    {
        //使わない方向で
    }

    // Update is called once per frame
    void Update()
    {
        //使わない方向で
    }

    override
    public void Playerinit(int playerno)
    {
        no = playerno;
        input = GetComponent<MultiInput>();
        input.PlayerNo = playerno;
        CharCon = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Model = transform.Find("BaseModel_Hero").transform;
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();

        MoveSpeed = Player_Hero_MoveSpeed;
        RotationSpeed = Player_Hero_RotationSpeed;
        player_Hero_sta = PLAYER_TEST_STA.NORMAL;
        PlayerSta = (int)player_Hero_sta;
        ComboFlg = false;
        Attack = Player_Hero_ATTACK;

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
            case PLAYER_TEST_STA.STRONGATTACK_START:
                StrongActionStart();
                break;
            case PLAYER_TEST_STA.STRONGATTACK_END:
                StrongActionEnd();
                break;
            case PLAYER_TEST_STA.SPECIAL_START:
                SpecialActionStart();
                break;
            case PLAYER_TEST_STA.SPECIAL_END:
                SpecialActionEnd();
                break;
            case PLAYER_TEST_STA.DAMAGE:
                DamageAction();
                break;
        }

        SetAnimatorData();
    }

    public override void PlayerDamage()
    {
        player_Hero_sta = PLAYER_TEST_STA.DAMAGE;
        PlayerSta = (int)player_Hero_sta;
        ModelTransformReset();
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0, 1, 0);

        if (input.GetButtonSquareTrigger())
        {
            aniendFlg = false;
            player_Hero_sta = PLAYER_TEST_STA.WEAKATTACK1;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationWeakattack1();
            ModelTransformReset();
        }

        if (input.GetButtonTriangleTrigger())
        {
            aniendFlg = false;
            player_Hero_sta = PLAYER_TEST_STA.STRONGATTACK_START;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationStrongattack();
            ModelTransformReset();
        }

        if (input.GetButtonCircleTrigger())
        {
            aniendFlg = false;
            player_Hero_sta = PLAYER_TEST_STA.SPECIAL_START;
            PlayerSta = (int)player_Hero_sta;
            HitAnime.HitAnimationSpecial();
            ModelTransformReset();
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
                player_Hero_sta = PLAYER_TEST_STA.WEAKATTACK2;
                PlayerSta = (int)player_Hero_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack2();
            }
            else
            {
                player_Hero_sta = PLAYER_TEST_STA.NORMAL;
                PlayerSta = (int)player_Hero_sta;
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
                player_Hero_sta = PLAYER_TEST_STA.WEAKATTACK3;
                PlayerSta = (int)player_Hero_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack3();
            }
            else
            {
                player_Hero_sta = PLAYER_TEST_STA.NORMAL;
                PlayerSta = (int)player_Hero_sta;
            }

            ModelTransformReset();
        }
    }

    private void NormalAction3()
    {
        RotationCharacter();

        if (CheckAnimationEND("Combo3"))
        {
            player_Hero_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void StrongActionStart()
    {
        RotationCharacter();
        
        if (CheckAnimationEND("StrongAttack_start"))
        {
            player_Hero_sta = PLAYER_TEST_STA.STRONGATTACK_END;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void StrongActionEnd()
    {
        RotationCharacter();

        if (CheckAnimationEND("StrongAttack_end"))
        {
            player_Hero_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void SpecialActionStart()
    {
        RotationCharacter();

        if (CheckAnimationEND("Special_start"))
        {
            player_Hero_sta = PLAYER_TEST_STA.SPECIAL_END;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void SpecialActionEnd()
    {
        RotationCharacter();

        if (CheckAnimationEND("Special_end"))
        {
            player_Hero_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Hero_sta = PLAYER_TEST_STA.NORMAL;
            PlayerSta = (int)player_Hero_sta;
            ModelTransformReset();
        }
    }

    
}
