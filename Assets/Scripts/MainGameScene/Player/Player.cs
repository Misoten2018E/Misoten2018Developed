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
        //NowCharacter.transform.parent = transform;
        playerbase = NowCharacter.GetComponent<PlayerBase>();
        playerbase.Playerinit(no);
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButton("Submit"))
        //{
        //    Vector3 pos = new Vector3(0,0,0);
        //    playerbase.ChangeCharacterAction(pos);
        //}
        //else
        //{
            playerbase.PlayerUpdate();
        //}
        
        transform.position = NowCharacter.transform.position;
        //if (input.GetButtonCircleTrigger())
        //{
        //    print("ButtonCircleTrigger" + no);
        //}

        //if (input.GetButtonCircleRelease())
        //{
        //    print("GetButtonCircleRelease" + no);
        //}

        //if (input.GetButtonCirclePress())
        //{
        //    print("GetButtonCirclePress" + no);
        //}


        //if (input.GetButtonSquareTrigger())
        //{
        //    print("GetButtonSquareTrigger" + no);
        //}

        //if (input.GetButtonSquareRelease())
        //{
        //    print("GetButtonSquareRelease" + no);
        //}

        //if (input.GetButtonSquarePress())
        //{
        //    print("GetButtonSquarePress" + no);
        //}


        //if (input.GetButtonTriangleTrigger())
        //{
        //    print("GetButtonTriangleTrigger" + no);
        //}

        //if (input.GetButtonTriangleRelease())
        //{
        //    print("GetButtonTriangleRelease" + no);
        //}

        //if (input.GetButtonTrianglePress())
        //{
        //    print("GetButtonTrianglePress" + no);
        //}


        //if (input.GetButtonCrossTrigger())
        //{
        //    print("GetButtonCrossTrigger" + no);
        //}

        //if (input.GetButtonCrossRelease())
        //{
        //    print("GetButtonCrossRelease" + no);
        //}

        //if (input.GetButtonCrossPress())
        //{
        //    print("GetButtonCrossPress" + no);
        //}


        //if (input.GetAllButtonCircleTrigger())
        //{
        //    print("GetAllButtonCircleTrigger" + no);
        //}

        //if (input.GetAllButtonCircleRelease())
        //{
        //    print("GetAllButtonCircleRelease" + no);
        //}

        //if (input.GetAllButtonCirclePress())
        //{
        //    print("GetAllButtonCirclePress" + no);
        //}


        //if (input.GetAllButtonSquareTrigger())
        //{
        //    print("GetAllButtonSquareTrigger" + no);
        //}

        //if (input.GetAllButtonSquareRelease())
        //{
        //    print("GetAllButtonSquareRelease" + no);
        //}

        //if (input.GetAllButtonSquarePress())
        //{
        //    print("GetAllButtonSquarePress" + no);
        //}


        //if (input.GetAllButtonTriangleTrigger())
        //{
        //    print("GetAllButtonTriangleTrigger" + no);
        //}

        //if (input.GetAllButtonTriangleRelease())
        //{
        //    print("GetAllButtonTriangleRelease" + no);
        //}

        //if (input.GetAllButtonTrianglePress())
        //{
        //    print("GetAllButtonTrianglePress" + no);
        //}


        //if (input.GetAllButtonCrossTrigger())
        //{
        //    print("GetAllButtonCrossTrigger" + no);
        //}

        //if (input.GetAllButtonCrossRelease())
        //{
        //    print("GetAllButtonCrossRelease" + no);
        //}

        //if (input.GetAllButtonCrossPress())
        //{
        //    print("GetAllButtonCrossPress" + no);
        //}
    }
}
