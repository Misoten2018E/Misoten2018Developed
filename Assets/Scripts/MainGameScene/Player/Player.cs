using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    MultiInput input;
    public int no;
    public float MoveSpeed;
    public float RotationSpeed;

    CharacterController CharCon;
    Vector3 velocity;

    // Use this for initialization
    void Start () {
        input = this.GetComponent<MultiInput>();
        CharCon = this.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(input.GetXaxis() != 0)
        {
            print("X" + no + input.GetXaxis());
        }
        if(input.GetYaxis() != 0)
        {
            print("Y" + no + input.GetYaxis());
        }

        velocity.x = input.GetXaxis() * MoveSpeed;
        velocity.z = input.GetYaxis() * MoveSpeed;

        Vector3 direction = new Vector3(velocity.x,0,velocity.z);
        if(direction.sqrMagnitude > 0f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward,direction,RotationSpeed * Time.deltaTime/Vector3.Angle(transform.forward,direction));
            transform.LookAt(transform.position + forward);
        }

        CharCon.Move(velocity * Time.deltaTime);

        if (input.GetButtonCircleTrigger())
        {
            print("ButtonCircleTrigger" + no);
        }

        //if (input.GetButtonCircleRelease())
        //{
        //    print("GetButtonCircleRelease" + no);
        //}

        //if (input.GetButtonCirclePress())
        //{
        //    print("GetButtonCirclePress" + no);
        //}


        if (input.GetButtonSquareTrigger())
        {
            print("GetButtonSquareTrigger" + no);
        }

        //if (input.GetButtonSquareRelease())
        //{
        //    print("GetButtonSquareRelease" + no);
        //}

        //if (input.GetButtonSquarePress())
        //{
        //    print("GetButtonSquarePress" + no);
        //}


        if (input.GetButtonTriangleTrigger())
        {
            print("GetButtonTriangleTrigger" + no);
        }

        //if (input.GetButtonTriangleRelease())
        //{
        //    print("GetButtonTriangleRelease" + no);
        //}

        //if (input.GetButtonTrianglePress())
        //{
        //    print("GetButtonTrianglePress" + no);
        //}


        if (input.GetButtonCrossTrigger())
        {
            print("GetButtonCrossTrigger" + no);
        }

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
