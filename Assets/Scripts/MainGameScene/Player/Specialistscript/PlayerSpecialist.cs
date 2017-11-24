using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialist : PlayerBase{

    private enum PLAYER_SPECIALIST_STA
    {
        NORMAL = 0,
        WEAKATTACK1,
        WEAKATTACK2,
        WEAKATTACK3,
        SET,
        BOON,
        SPECIAL,
        DAMAGE,
        PAUSE,
        PLAYER_STA_MAX
    }

    public GameObject Bombprefab;

    MultiInput input;
    PLAYER_SPECIALIST_STA player_Special_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;
    bool bomsetflg;
    Bomb bomb;

    const int Player_Special_MoveSpeed = 5;
    const int Player_Special_RotationSpeed = 750;
    const int Player_Special_ActionRotationSpeed = 500;
    const int Player_Special_ATTACK = 1;

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
        Model = transform.Find("BaseModel_Hero").transform;
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();
       

        MoveSpeed = Player_Special_MoveSpeed;
        RotationSpeed = Player_Special_RotationSpeed;
        player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
        PlayerSta = (int)player_Special_sta;
        ComboFlg = false;
        Attack = Player_Special_ATTACK;
        nodamageflg = false;
        bomsetflg = false;

        HitAnime.Initialize(this);
    }

    override
    public void PlayerUpdate()
    {
        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;
        velocity.y = 0;

        switch (player_Special_sta)
        {
            case PLAYER_SPECIALIST_STA.NORMAL:
                Normal();
                break;
            case PLAYER_SPECIALIST_STA.WEAKATTACK1:
                NormalAction1();
                break;
            case PLAYER_SPECIALIST_STA.WEAKATTACK2:
                NormalAction2();
                break;
            case PLAYER_SPECIALIST_STA.WEAKATTACK3:
                NormalAction3();
                break;
            case PLAYER_SPECIALIST_STA.SET:
                BomSetAction();
                break;
            case PLAYER_SPECIALIST_STA.BOON:
                BomBoonAction();
                break;
            case PLAYER_SPECIALIST_STA.SPECIAL:
                SpecialAction();
                break;
            case PLAYER_SPECIALIST_STA.DAMAGE:
                DamageAction();
                break;
            case PLAYER_SPECIALIST_STA.PAUSE:
                PauseAction();
                break;
        }

        NoDameCnt();

        SetAnimatorData();
    }

    public override void PlayerDamage(HitObjectImpact damage)
    {
        if (nodamageflg) return;

        player_Special_sta = PLAYER_SPECIALIST_STA.DAMAGE;
        PlayerSta = (int)player_Special_sta;
        damage.DamageHp(HP);
        animator.SetTrigger("Damage");
        nodamageflg = true;
        ModelTransformReset();
    }

    public override void PlayerPause()
    {
        player_Special_sta = PLAYER_SPECIALIST_STA.PAUSE;
        PlayerSta = (int)player_Special_sta;
        animator.SetTrigger("Pause");
        ModelTransformReset();
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0, 1, 0);

        if (input.GetButtonSquareTrigger())
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.WEAKATTACK1;
            PlayerSta = (int)player_Special_sta;
            HitAnime.HitAnimationWeakattack1();
            ModelTransformReset();
        }

        if (input.GetButtonTriangleTrigger())
        {
            if (bomsetflg)
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.BOON;
                PlayerSta = (int)player_Special_sta;
                HitAnime.HitAnimationStrongattack();
                ModelTransformReset();
            }
            else
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.SET;
                PlayerSta = (int)player_Special_sta;
                HitAnime.HitAnimationStrongattack();
                ModelTransformReset();
            }
            
        }

        if (input.GetButtonCircleTrigger())
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.SPECIAL;
            PlayerSta = (int)player_Special_sta;
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

        if (CheckAnimationEND("conb1"))
        {
            if (ComboFlg)
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.WEAKATTACK2;
                PlayerSta = (int)player_Special_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack2();
            }
            else
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
                PlayerSta = (int)player_Special_sta;
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
                player_Special_sta = PLAYER_SPECIALIST_STA.WEAKATTACK3;
                PlayerSta = (int)player_Special_sta;
                ComboFlg = false;
                HitAnime.HitAnimationWeakattack3();
            }
            else
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
                PlayerSta = (int)player_Special_sta;
            }

            ModelTransformReset();
        }
    }

    private void NormalAction3()
    {
        RotationCharacter();

        if (CheckAnimationEND("conb3"))
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
        }
    }

    private void BomSetAction()
    {
        Vector3 bombpos;
        RotationCharacter();

        if (CheckAnimationEND("set"))
        {
            bombpos = transform.position;
            bombpos.y = 0.0f;
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
            bomsetflg = true;
            GameObject obj = Instantiate(Bombprefab, bombpos, Quaternion.identity);
            bomb = obj.GetComponent<Bomb>();
        }
    }

    private void BomBoonAction()
    {
        RotationCharacter();

        if (CheckAnimationEND("booon"))
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
            bomsetflg = false;
            bomb.BombswitchON();
        }
    }

    private void SpecialAction()
    {
        RotationCharacter();

        if (CheckAnimationEND("Special"))
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
            PlayerManager.instance.PlyerDoping(transform.position,no);
        }
    }

    private void DamageAction()
    {
        if (CheckAnimationEND("Damage"))
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
        }
    }

    private void PauseAction()
    {
        if (CheckAnimationEND("Pause"))
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
            PlayerSta = (int)player_Special_sta;
            ModelTransformReset();
        }
    }
}
