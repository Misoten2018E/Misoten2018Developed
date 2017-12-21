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

    public GameObject BarrettObj;

    MultiInput input;
    PLAYER_HEEL_STA player_Heel_sta;
    bool ComboFlg;
    HitAnimationBase HitAnime;
    Transform Specialtarget;
    LineRenderer linerenderer;
    bool BarrettFlg;

    const float Player_Heel_MoveSpeed = 5 * 1.2f;
    const int Player_Heel_RotationSpeed = 750;
    const int Player_Heel_ActionRotationSpeed = 250;
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
        RootPos = Model.Find("Character1_Reference1").GetComponent<Transform>().Find("Character1_Hips").GetComponent<Transform>();
        linerenderer = GetComponent<LineRenderer>();

        MoveSpeed = Player_Heel_MoveSpeed;
        RotationSpeed = Player_Heel_RotationSpeed;
        player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
        PlayerSta = (int)player_Heel_sta;
        ComboFlg = false;
        Attack = Player_Heel_ATTACK;
        nodamageflg = false;
        CharCon.center = new Vector3(0, 10 + no * 3, 0);
        linerenderer.enabled = false;
        BarrettFlg = false;

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
        linerenderer.enabled = false;
        nodamageflg = true;
        ModelTransformReset();
    }

    public override void PlayerDamageMotion()
    {
        player_Heel_sta = PLAYER_HEEL_STA.DAMAGE;
        PlayerSta = (int)player_Heel_sta;
        animator.SetTrigger("Damage");
        linerenderer.enabled = false;
        ModelTransformReset();
    }


    public override void PlayerPause()
    {
        player_Heel_sta = PLAYER_HEEL_STA.PAUSE;
        PlayerSta = (int)player_Heel_sta;
        animator.SetTrigger("Pause");
        linerenderer.enabled = false;
        ModelTransformReset();
    }

    private void Normal()
    {
        CharCon.center = new Vector3(0, 0, 0);
        RotationSpeed = Player_Heel_RotationSpeed;
        linerenderer.enabled = false;

        if (input.GetButtonSquareTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK1;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationWeakattack1(Attack);
            ComboFlg = false;
            BarrettFlg = false;
            RotationSpeed = Player_Heel_ActionRotationSpeed;
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_Light1,transform.position);
        }

        if (input.GetButtonTriangleTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.STRONGATTACK;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationStrongattack(Attack);
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_Strong, transform.position);
        }

        if (input.GetButtonCircleTrigger())
        {
            player_Heel_sta = PLAYER_HEEL_STA.SPECIAL_START;
            PlayerSta = (int)player_Heel_sta;
            HitAnime.HitAnimationSpecial();
            ModelTransformReset();
            SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_SP_Catch, transform.position);
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

        float normaltime = GetAninormalizedTime("conb1");
        if (normaltime > 0.4f && BarrettFlg == false)
        {
            MakeBarrett(transform.position, transform.forward);
            BarrettFlg = true;
        }

        if (CheckAnimationEND("conb1"))
        {
            if (ComboFlg)
            {
                player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK2;
                PlayerSta = (int)player_Heel_sta;
                ComboFlg = false;
                BarrettFlg = false;
                HitAnime.HitAnimationWeakattack2(Attack);
                SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_Light2, transform.position);
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

        float normaltime = GetAninormalizedTime("conb2");
        if (normaltime > 0.5f && BarrettFlg == false)
        {
            MakeBarrett(transform.position, transform.forward);
            BarrettFlg = true;
        }


        if (CheckAnimationEND("conb2"))
        {
            if (ComboFlg)
            {
                player_Heel_sta = PLAYER_HEEL_STA.WEAKATTACK3;
                PlayerSta = (int)player_Heel_sta;
                ComboFlg = false;
                BarrettFlg = false;
                HitAnime.HitAnimationWeakattack3(Attack);
                SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_Light3, transform.position);
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

        float normaltime = GetAninormalizedTime("conb3");
        if (normaltime > 0.5f && BarrettFlg == false)
        {
            MakeBarrett(transform.position, transform.forward + transform.right * 0.5f);
            MakeBarrett(transform.position, transform.forward + transform.right * -0.5f);
            BarrettFlg = true;
        }

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
            SoundManager.Instance.PlaySE(SoundManager.SEType.Heel_SP_Pull, transform.position);

            if (hitobj)
            {   
                
                Player P = hitobj.GetComponent<Player>();
                P.PlayerForcibly(transform.position);
                Specialtarget = hitobj.transform;
                linerenderer.enabled = true;
            }
            
            
        }
    }

    private void SpecialActionEnd()
    {
        RotationCharacter();
        if (linerenderer.enabled == true)
        {
            Vector3[] poss = new Vector3[2];
            poss[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            poss[1] = new Vector3(Specialtarget.position.x, Specialtarget.position.y, Specialtarget.position.z);
            linerenderer.SetPositions(poss);
        }


        if (CheckAnimationEND("Special_end"))
        {
            player_Heel_sta = PLAYER_HEEL_STA.NORMAL;
            PlayerSta = (int)player_Heel_sta;
            ModelTransformReset();
            linerenderer.enabled = false;
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

    void MakeBarrett(Vector3 pos,Vector3 frontv)
    {
        Vector3 B_pos;
        B_pos = pos + frontv * 2;
        Quaternion qua = Quaternion.LookRotation(frontv);
        HeelBarrett HB;

        GameObject obj = Instantiate(BarrettObj, B_pos, qua)as GameObject;
        HB = obj.GetComponent<HeelBarrett>();
        HB.SetPlayerObj(myPlayer);
    }
}
