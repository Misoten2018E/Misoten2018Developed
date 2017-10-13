using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiInput : MonoBehaviour {
    public int PlayerNo;
    private int WorkPlayerNo;

    // Use this for initialization
    void Start () {
        if (PlayerNo < 0 || PlayerNo > 3)
        {
            WorkPlayerNo = 0;
        }
        else
        {
            WorkPlayerNo = PlayerNo;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //======================================================================
    public float GetXaxis()
    {
        return Input.GetAxisRaw("Player"+ WorkPlayerNo + "_X");
    }

    public float GetYaxis()
    {
        return Input.GetAxisRaw("Player" + WorkPlayerNo + "_Y");
    }
    //======================================================================

    //======================================================================
    public bool GetButtonCircleTrigger()
    {
        return Input.GetButtonDown("Player" + WorkPlayerNo + "_Circle");
    }

    public bool GetButtonCircleRelease()
    {
        return Input.GetButtonUp("Player" + WorkPlayerNo + "_Circle");
    }

    public bool GetButtonCirclePress()
    {
        return Input.GetButton("Player" + WorkPlayerNo + "_Circle");
    }
    //======================================================================

    //======================================================================
    public bool GetButtonTriangleTrigger()
    {
        return Input.GetButtonDown("Player" + WorkPlayerNo + "_Triangle");
    }

    public bool GetButtonTriangleRelease()
    {
        return Input.GetButtonUp("Player" + WorkPlayerNo + "_Triangle");
    }

    public bool GetButtonTrianglePress()
    {
        return Input.GetButton("Player" + WorkPlayerNo + "_Triangle");
    }
    //======================================================================
    
    //======================================================================
    public bool GetButtonSquareTrigger()
    {
        return Input.GetButtonDown("Player" + WorkPlayerNo + "_Square");
    }

    public bool GetButtonSquareRelease()
    {
        return Input.GetButtonUp("Player" + WorkPlayerNo + "_Square");
    }

    public bool GetButtonSquarePress()
    {
        return Input.GetButton("Player" + WorkPlayerNo + "_Square");
    }
    //======================================================================
    
    //======================================================================
    public bool GetButtonCrossTrigger()
    {
        return Input.GetButtonDown("Player" + WorkPlayerNo + "_Cross");
    }

    public bool GetButtonCrossRelease()
    {
        return Input.GetButtonUp("Player" + WorkPlayerNo + "_Cross");
    }

    public bool GetButtonCrossPress()
    {
        return Input.GetButton("Player" + WorkPlayerNo + "_Cross");
    }
    //======================================================================

    //======================================================================
    public bool GetAllButtonCircleTrigger()
    {
        for(int i = 0;i < 4 ;i++)
        {
            if(Input.GetButtonDown("Player" + i + "_Circle"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonCircleRelease()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonUp("Player" + i + "_Circle"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonCirclePress()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButton("Player" + i + "_Circle"))
            {
                return true;
            }
        }
        return false;
    }
    //======================================================================

    //======================================================================
    public bool GetAllButtonTriangleTrigger()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Player" + i + "_Triangle"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonTriangleRelease()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonUp("Player" + i + "_Triangle"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonTrianglePress()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButton("Player" + i + "_Triangle"))
            {
                return true;
            }
        }
        return false;
    }
    //======================================================================

    //======================================================================
    public bool GetAllButtonSquareTrigger()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Player" + i + "_Square"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonSquareRelease()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonUp("Player" + i + "_Square"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonSquarePress()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButton("Player" + i + "_Square"))
            {
                return true;
            }
        }
        return false;
    }
    //======================================================================

    //======================================================================
    public bool GetAllButtonCrossTrigger()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Player" + i + "_Cross"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonCrossRelease()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonUp("Player" + i + "_Cross"))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetAllButtonCrossPress()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButton("Player" + i + "_Cross"))
            {
                return true;
            }
        }
        return false;
    }
    //======================================================================
}
