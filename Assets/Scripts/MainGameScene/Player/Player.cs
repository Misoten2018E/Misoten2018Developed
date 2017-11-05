using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SceneStartEvent{
    enum PLAYER_STA
    {
        NORMAL = 0,
        CHANGE_HERO,
        CHANGE_HEEL,
        CHANGE_SPECIALIST,
        CHANGE_NORMAL,
        PLAYER_STA_MAX
    }
    //MultiInput input;
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
    int PlayerSta;
    Vector3 GoPosition;
    PLAYER_STA playersta;

    // Use this for initialization
    void Start () {
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

        switch (playersta)
        {
            case PLAYER_STA.NORMAL:
                playerbase.PlayerUpdate();
                break;
            case PLAYER_STA.CHANGE_HERO:

                break;
            case PLAYER_STA.CHANGE_HEEL:

                break;
            case PLAYER_STA.CHANGE_SPECIALIST:

                break;

        }

        if (playerbase.PlayerIsLive())
        {

        }

        transform.position = playerbase.GetBodyPosition();
        
    }

    public override void SceneMyInit()
    {
        Vector3 workpos = new Vector3();

        //input = GetComponent<MultiInput>();
        //CharCon = this.GetComponent<CharacterController>();

        workpos.Set(transform.position.x, transform.position.y, transform.position.z);
        NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

        playerbase = NowCharacter.GetComponent<PlayerBase>();
        playerbase.Playerinit(no);
        PlayerSta = ConstPlayerSta.NormalCharacter;
        playersta = PLAYER_STA.NORMAL;

        isInitialized = true;
    }

    public int GetPlayerSta() { return PlayerSta; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ConstTags.EnemyAttack)
        {
            playerbase.PlayerDamage();
        }

        if (other.gameObject.tag == ConstTags.HeelArea)
        {

        }

        if (other.gameObject.tag == ConstTags.HeroArea)
        {

        }

        if (other.gameObject.tag == ConstTags.SpecialistArea)
        {

        }
    }

    private void ChangeCharacter(int Changechar)
    {
        Vector3 workpos = new Vector3();
        workpos.Set(transform.position.x, transform.position.y, transform.position.z);

        Destroy(NowCharacter);

        switch (Changechar)
        {
            case ConstPlayerSta.NormalCharacter:
                NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

                playerbase = NowCharacter.GetComponent<PlayerBase>();
                playerbase.Playerinit(no);
                PlayerSta = ConstPlayerSta.NormalCharacter;
                break;
            case ConstPlayerSta.HeroCharacter:

                break;
            case ConstPlayerSta.HeelCharacter:

                break;
            case ConstPlayerSta.SpecialistCharacter:

                break;
        }
    }
}
