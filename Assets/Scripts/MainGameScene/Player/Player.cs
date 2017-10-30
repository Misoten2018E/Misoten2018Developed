using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //MultiInput input;
    public int no;
    //public float MoveSpeed;
    //public float RotationSpeed;

    //CharacterController CharCon;
    //Vector3 velocity;
    public GameObject NormalCharacter;
    GameObject NowCharacter;
    PlayerBase playerbase;

    // Use this for initialization
    void Start () {
        Vector3 workpos = new Vector3();

        //input = GetComponent<MultiInput>();
        //CharCon = this.GetComponent<CharacterController>();

        workpos.Set(transform.position.x,transform.position.y, transform.position.z);
        NowCharacter = Instantiate(NormalCharacter, workpos, Quaternion.identity) as GameObject;

        playerbase = NowCharacter.GetComponent<PlayerBase>();
        playerbase.Playerinit(no);
    }
	
	// Update is called once per frame
	void Update () {
        
        playerbase.PlayerUpdate();
        
        transform.position = playerbase.GetBodyPosition();
        
    }
}
