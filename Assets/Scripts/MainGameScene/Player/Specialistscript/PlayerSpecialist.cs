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
    bool FlashON;

    const float Player_Special_MoveSpeed = 5;
    const int Player_Special_RotationSpeed = 750;
    const int Player_Special_ActionRotationSpeed = 250;
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
        Model = transform.Find("kuroko").transform;
        HitAnime = GetComponent<HitAnimationBase>();
        HP = gameObject.GetComponent<ObjectHp>();
        RootPos = Model.Find("Character1_Reference").GetComponent<Transform>().Find("Character1_Hips").GetComponent<Transform>();

        MoveSpeed = Player_Special_MoveSpeed;
        RotationSpeed = Player_Special_RotationSpeed;
        player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
        PlayerSta = (int)player_Special_sta;
        ComboFlg = false;
        Attack = Player_Special_ATTACK;
        nodamageflg = false;
        bomsetflg = false;
        CharCon.center = new Vector3(0, 10 + no * 3, 0);
        cpynodammax = nodamagetime;
        FlashON = false;

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

    public override void PlayerDamageMotion(int flg)
    {
        player_Special_sta = PLAYER_SPECIALIST_STA.DAMAGE;
        PlayerSta = (int)player_Special_sta;
        animator.SetTrigger("Damage");
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
        CharCon.center = new Vector3(0, 0, 0);
        RotationSpeed = Player_Special_RotationSpeed;

        if (input.GetButtonSquareTrigger())
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.WEAKATTACK1;
            PlayerSta = (int)player_Special_sta;
            HitAnime.HitAnimationWeakattack1(Attack);
            RotationSpeed = Player_Special_ActionRotationSpeed;
            ComboFlg = false;
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_Light1, transform.position);
            var par = ResourceManager.Instance.Get<EffectBase>(ConstDirectry.DirParticle, ConstEffects.Sand);

            EffectBase Effect = GameObject.Instantiate(par);
            EffectSupport.Follow(Effect,transform.position,transform.right * -1);
        }

        if (input.GetButtonTriangleTrigger())
        {
            if (bomsetflg)
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.BOON;
                PlayerSta = (int)player_Special_sta;
                ModelTransformReset();
            }
            else
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.SET;
                PlayerSta = (int)player_Special_sta;
                ModelTransformReset();
                SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_Strong_Set, transform.position);
            }
            
        }

        if (input.GetButtonCircleTrigger())
        {
            player_Special_sta = PLAYER_SPECIALIST_STA.SPECIAL;
            PlayerSta = (int)player_Special_sta;
            HitAnime.HitAnimationSpecial();
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_SP_Megaphone, transform.position,0.5f);
        }

        MoveCharacter();

        if (CheckAnimationSTART("wait") || CheckAnimationSTART("run"))
        {
            ModelTransformReset();
            
        }
    }

    private void NormalAction1()
    {
        //RotationCharacter();

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
                HitAnime.HitAnimationWeakattack2(Attack);
                SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_Light2, transform.position);

                var par = ResourceManager.Instance.Get<EffectBase>(ConstDirectry.DirParticle, ConstEffects.Water);

                EffectBase Effect = GameObject.Instantiate(par);
                EffectSupport.Follow(Effect, transform.position, transform.right * -1);
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
        //RotationCharacter();

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
                HitAnime.HitAnimationWeakattack3(Attack);
                SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_Light3, transform.position);

            }
            else
            {
                player_Special_sta = PLAYER_SPECIALIST_STA.NORMAL;
                PlayerSta = (int)player_Special_sta;
            }

            FlashON = false;
            ModelTransformReset();
        }
    }

    private void NormalAction3()
    {
        //RotationCharacter();
        float time = GetAninormalizedTime("conb3");
        if (FlashON == false && time > 0.4f)
        {
            var par = ResourceManager.Instance.Get<EffectBase>(ConstDirectry.DirParticle, ConstEffects.Flash);

            EffectBase Effect = GameObject.Instantiate(par);
            EffectSupport.Follow(Effect, transform.position, transform.right * -1);

            FlashON = true;
        }

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
            bomb.InitBomb(myPlayer);
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
            bomb.BombswitchON(Attack);
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

    public override bool PlayerIsDeath()
    {
        bool res = false;

        if (HP.isDeath && player_Special_sta != PLAYER_SPECIALIST_STA.DAMAGE)
        {
            res = true;
        }

        return res;
    }
}
