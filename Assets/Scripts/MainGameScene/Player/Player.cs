using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SceneStartEvent{
    enum PLAYER_STA
    {
        NORMAL = 0,
        CITYIN,
        CHANGE,
        CITYOUT,
        FORCIBLY,
        PLAYER_STA_MAX
    }
    const float CORRECTION = 1.5f;
    const int MOVERITU = 2;
    const float FORCIBLYSPEED = 0.3f;

    MultiInput m_input;
    public int no;
    //public float MoveSpeed;
    //public float RotationSpeed;

    //CharacterController CharCon;
    //Vector3 velocity;
    public GameObject NormalCharacter;
    public GameObject HeroCharacter;
    public GameObject HeelCharacter;
    public GameObject SpecialistCharacter;
    GameObject NowCharacter;
    PlayerBase playerbase;
    int CharacterSta;
    Vector3 ChangeStartPos;
    Vector3 ChangeCenterPos;
    int beforeCharacter;
    PLAYER_STA playersta;
    float ForciblySpeed;

    // 11/20 UIを管理するため追加
    [SerializeField] private UIPlayerInput UIPlayer;

    // Use this for initialization
    void Start () {
        m_input = GetComponent<MultiInput>();
        //初期化管理のためコメントアウト
        //Vector3 workpos = new Vector3();

        ////input = GetComponent<MultiInput>();
        ////CharCon = this.GetComponent<CharacterController>();

        //workpos.Set(transform.position.x,transform.position.y, transform.position.z);
        //NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

        //playerbase = NowCharacter.GetComponent<PlayerBase>();
        //playerbase.Playerinit(no);
    }
	
	// Update is called once per frame
	void Update () {
       
        if (!isInitialized) return;

        float r = 6.0f;

        switch (playersta)
        {
            case PLAYER_STA.NORMAL:
                playerbase.PlayerUpdate();

                break;
            case PLAYER_STA.CITYIN:
                playerbase.SetCharConNoHit(true);
                playerbase.PlayerCityIn(ChangeCenterPos, ChangeStartPos);

                r = (Mathf.Abs(transform.position.x - ChangeCenterPos.x) * Mathf.Abs(transform.position.x - ChangeCenterPos.x)) + 
                    (Mathf.Abs(transform.position.z - ChangeCenterPos.z) * Mathf.Abs(transform.position.z - ChangeCenterPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    playersta = PLAYER_STA.CHANGE;
                }
                
                break;
            case PLAYER_STA.CHANGE:
                if (m_input.GetButtonCircleTrigger())
                {
                    if (beforeCharacter != ConstPlayerSta.HeroCharacter)
                    {
                        
                        ChangeCharacter(ConstPlayerSta.HeroCharacter);
                        
                    }
                    playersta = PLAYER_STA.CITYOUT;
                }

                if (m_input.GetButtonTriangleTrigger())
                {
                    if (beforeCharacter != ConstPlayerSta.HeelCharacter)
                    {
                        ChangeCharacter(ConstPlayerSta.HeelCharacter);
                       
                    }
                    playersta = PLAYER_STA.CITYOUT;
                }

                if (m_input.GetButtonSquareTrigger())
                {
                    if (beforeCharacter != ConstPlayerSta.SpecialistCharacter)
                    {
                        ChangeCharacter(ConstPlayerSta.SpecialistCharacter);
                        
                    }
                    playersta = PLAYER_STA.CITYOUT;
                }
                break;
            case PLAYER_STA.CITYOUT:
                playerbase.PlayerCityOut(ChangeStartPos, ChangeCenterPos);

                r = (Mathf.Abs(transform.position.x - ChangeStartPos.x) * Mathf.Abs(transform.position.x - ChangeStartPos.x)) +
                    (Mathf.Abs(transform.position.z - ChangeStartPos.z) * Mathf.Abs(transform.position.z - ChangeStartPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    playersta = PLAYER_STA.NORMAL;
                    playerbase.SetCharConNoHit(false);
                    GetComponent<CapsuleCollider>().enabled = true;
                    if (CharacterSta != beforeCharacter)
                    {
                        playerbase.PlayerPause();
                    }
                   
                }
                break;
            case PLAYER_STA.FORCIBLY:
                playerbase.ForciblyMove(ChangeCenterPos,ForciblySpeed);

                r = (Mathf.Abs(transform.position.x - ChangeCenterPos.x) * Mathf.Abs(transform.position.x - ChangeCenterPos.x)) +
                   (Mathf.Abs(transform.position.z - ChangeCenterPos.z) * Mathf.Abs(transform.position.z - ChangeCenterPos.z));
                if (r < CORRECTION * CORRECTION)
                {
                    playersta = PLAYER_STA.NORMAL;
                }
                break;

        }

        //print(hitcityflg);
        transform.position = playerbase.GetBodyPosition();
    }

    private void LateUpdate()
    {
        if (!isInitialized) return;

        if (playerbase.PlayerIsDeath() && playersta == PLAYER_STA.NORMAL)
        {
            if (CharacterSta != ConstPlayerSta.NormalCharacter)
            {
                ChangeCharacter(ConstPlayerSta.NormalCharacter);
            }
        }
    }

    public override void SceneMyInit()
    {
        Vector3 workpos = new Vector3();
        
        //input = GetComponent<MultiInput>();
        //CharCon = this.GetComponent<CharacterController>();

        workpos.Set(transform.position.x, 1.0f, transform.position.z);
        NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;
        
        playerbase = NowCharacter.GetComponent<PlayerBase>();
        playerbase.Playerinit(gameObject);
        CharacterSta = ConstPlayerSta.NormalCharacter;
        playersta = PLAYER_STA.NORMAL;
        Score.instance.SetCharacter(no, CharacterSta);
        ForciblySpeed = 0;

        isInitialized = true;
    }

    public int GetCharacterSta() { return CharacterSta; }

    public int GetPlayerHP()
    {
        return playerbase.GetHP();
    }

    public void PlayerForcibly(Vector3 target)
    {
        playersta = PLAYER_STA.FORCIBLY;
        ChangeCenterPos = target;
        playerbase.PlayerDamageMotion();
        float length = (Mathf.Abs(transform.position.x - target.x) * Mathf.Abs(transform.position.x - target.x)) +
                        (Mathf.Abs(transform.position.z - target.z) * Mathf.Abs(transform.position.z - target.z));
        length = Mathf.Sqrt(length);
        ForciblySpeed = length / FORCIBLYSPEED;
        print("ForciblySpeed"+ ForciblySpeed);
    }

    public void AttackUP(float atk)
    {
        playerbase.AttackUP(atk);
    }

    public void AttackDOWN(float atk)
    {
        playerbase.AttackDOWN(atk);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ConstTags.EnemyAttack && playersta == PLAYER_STA.NORMAL)
        {
            HitObjectImpact damage = other.GetComponent<HitObjectImpact>();
            playerbase.PlayerDamage(damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ConstTags.City && playersta == PLAYER_STA.NORMAL)
        {
            
            if (m_input.GetButtonCrossTrigger() && playerbase.GetCharacterSta() == 0)
            {
                playersta = PLAYER_STA.CITYIN;
                ChangeStartPos = transform.position;
                ChangeCenterPos = other.gameObject.transform.position;
                ChangeStartPos.y = 0.0f;
                ChangeCenterPos.y = 0.0f;
                Vector3 v = ChangeStartPos - ChangeCenterPos;
                v.Normalize();
                ChangeStartPos += v * MOVERITU;
                beforeCharacter = CharacterSta;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    private void ChangeCharacter(int Changechar)
    {
        Vector3 workpos = new Vector3();
        workpos.Set(transform.position.x, 0.0f, transform.position.z);

        Destroy(NowCharacter);

        switch (Changechar)
        {
            case ConstPlayerSta.NormalCharacter:
                NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(gameObject);
                CharacterSta = ConstPlayerSta.NormalCharacter;
                break;
            case ConstPlayerSta.HeroCharacter:
                NowCharacter = Instantiate(HeroCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(gameObject);
                CharacterSta = ConstPlayerSta.HeroCharacter;
                break;
            case ConstPlayerSta.HeelCharacter:
                NowCharacter = Instantiate(HeelCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(gameObject);
                CharacterSta = ConstPlayerSta.HeelCharacter;
                break;
            case ConstPlayerSta.SpecialistCharacter:
                NowCharacter = Instantiate(SpecialistCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(gameObject);
                CharacterSta = ConstPlayerSta.SpecialistCharacter;
                break;
        }
        Score.instance.SetCharacter(no, CharacterSta);
        // 11/20 UIを管理するため追加
        UIPlayer.ChangeIcons(Changechar);

	}
}
