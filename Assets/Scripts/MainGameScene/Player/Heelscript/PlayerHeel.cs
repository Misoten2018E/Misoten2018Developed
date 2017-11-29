using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeel : PlayerBase{

    private enum PLAYER_HEEL_STA
    {
        NORMAL = 0,
        WEAKATTACK1,
        WEAKATTACK2,
        WEAKATTACK3,
        STRONGATTACK,
        SPECIAL_START,
        SPECIAL_END,
        DAMAGE,
        PAUSE,
        PLAYER_STA_MAX
    }

    MultiInput input;
    PLAYER_HEEL_STA player_Heel_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;

    const int Player_Heel_MoveSpeed = 5;
    const int Player_Heel_RotationSpeed = 750;
    const int Player_Heel_ActionRotationSpeed = 500;
    const int Player_Heel_ATTACK = 1;

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
    public void Playerinit(GameObject playerobj)
    {
        myPlayer = playerobj;
        Player player = playerobj.GetComponent<Player>();
        no = player.no;
        input = GetComponent<MultiInput>();
        input.PlayerNo = no;
        CharCon = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Model = transform.Find("BaseModel_Viran").transform;
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();

        MoveSpeed = Player_Heel_MoveSpeed;
        RotationSpeed = Player_Heel_RotationSpeed;
        player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
        PlayerSta = (int)player_Heel_sta;
        ComboFlg = false;
        Attack = Player_Heel_ATTACK;
        nodamageflg = false;

        HitAnime.Initialize(this);
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;

        switch (player_Heel_sta)
        {
            case PLAYER_HEEL_STA.NORMAL:
                Normal();
                break;
            case PLAYER_HEEL_STA.WEAKATTACK1:
                NormalAction1();
                break;
            case PLAYER_HEEL_STA.WEAKATTACK2:
                NormalAction2();
                break;
            case PLAYER_HEEL_STA.WEAKATTACK3:
                NormalAction3();
                break;
            case PLAYER_HEEL_STA.STRONGATTACK:
                StrongAction();
                break;
            case PLAYER_HEEL_STA.SPECIAL_START:
                SpecialActionStart();
                break;
            case PLAYER_HEEL_STA.SPECIAL_END:
                SpecialActionEnd();
                break;
            case PLAYER_HEEL_STA.DAMAGE:
                DamageAction();
                break;
            case PLAYER_HEEL_STA.PAUSE:
                PauseAction();
                break;
        }

        NoDameCnt();

        SetAnimatorData();
    }

    public override void PlayerDamage(HitObjectImpact damage)
    {
        if (nodamageflg) return;

        player_Heel_sta = PLAYER_HEEL_STA.DAMAGE;
        PlayerSta = (int)player_Heel_sta;
        damage.DamageHp(HP);
        animator.SetTrigger("Damage");
        nodamageflg = true;
        ModelTransformReset();
    }

    public override void PlayerPause()
    {
        player_Heel_sta = PLAYER_HEEL_STA.PAUSE;
        PlayerSta = (int)player_Heel_sta;
        animator.SetTrigger("Pause");
        ModelTransformReset();
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0, 0, 0);

        if (input.GetButtonSquareTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK1;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationWeakattack1();
            ModelTransformReset();
        }

        if (input.GetButtonTriangleTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.STRONGATTACK;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationStrongattack();
            ModelTransformReset();
        }

        if (input.GetButtonCircleTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.SPECIAL_START;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationSpecial();
            ModelTransformReset();
        }

        MoveCharacter();

        if (CheckAnimationSTART("wait") || CheckAnimationSTART("run"))
        {
            ModelTransformReset();
            print("riseto");
        }
    }

    private void NormalAction1()
    {
        RotationCharacter();

        if (input.GetButtonSquareTrigger() && ComboFlg == false)
        {
            ComboFlg = true;
        }

        if (CheckAnimationEND("conb1"))
        {
            if (ComboFlg)
            {
                player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK2;
                PlayerSta = (int)player_Heel_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack2();
            }
            else
            {
                player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
                PlayerSta = (int)player_Heel_sta;
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

        if (CheckAnimationEND("conb2"))
        {
            if (ComboFlg)
            {
                player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK3;
                PlayerSta = (int)player_Heel_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack3();
            }
            else
            {
                player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
                PlayerSta = (int)player_Heel_sta;
            }

            ModelTransformReset();
        }
    }

    private void NormalAction3()
    {
        RotationCharacter();

        if (CheckAnimationEND("conb3"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
        }
    }

    private void StrongAction()
    {
        RotationCharacter();

        if (CheckAnimationEND("Strong"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
        }
    }

    private void SpecialActionStart()
    {
        RotationCharacter();

        if (input.GetButtonCircleRelease())
        {
            player_Heel_sta = PLAYER_HEEL_STA.SPECIAL_END;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
            Vector3 front = transform.forward;
            front.Normalize();
            GameObject hitobj = PlayerManager.instance.GetHeelSpecialObj(front,transform.position,no);
            
            if (hitobj)
            {   
                print(hitobj.name);
                Player P = hitobj.GetComponent<Player>();
                P.PlayerForcibly(transform.position);
            }
            else
            {
                print("heelnohit");
            }
            
        }
    }

    private void SpecialActionEnd()
    {
        RotationCharacter();

        if (CheckAnimationEND("Special_end"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
        }
    }

    private void PauseAction()
    {
        if (CheckAnimationEND("Pause"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
        }
    }

    public override bool PlayerIsDeath()
    {
        bool res = false;

        if (HP.isDeath && player_Heel_sta != PLAYER_HEEL_STA.DAMAGE)
        {
            res = true;
        }

        return res;
    }
}
