using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiInput : SceneStartEvent{
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
        return Input.GetAxisRaw("Player_X" + WorkPlayerNo);
    }

    public float GetYaxis()
    {
        return Input.GetAxisRaw("Player_Y" + WorkPlayerNo);
    }
    //======================================================================

    //======================================================================
    public bool GetButtonCircleTrigger()
    {
        return Input.GetButtonDown("Player_Circle" + WorkPlayerNo);
    }

    public bool GetButtonCircleRelease()
    {
        return Input.GetButtonUp("Player_Circle" + WorkPlayerNo);
    }

    public bool GetButtonCirclePress()
    {
        return Input.GetButton("Player_Circle" + WorkPlayerNo);
    }
    //======================================================================

    //======================================================================
    public bool GetButtonTriangleTrigger()
    {
        return Input.GetButtonDown("Player_Triangle" + WorkPlayerNo);
    }

    public bool GetButtonTriangleRelease()
    {
        return Input.GetButtonUp("Player_Triangle" + WorkPlayerNo);
    }

    public bool GetButtonTrianglePress()
    {
        return Input.GetButton("Player_Triangle" + WorkPlayerNo);
    }
    //======================================================================
    
    //======================================================================
    public bool GetButtonSquareTrigger()
    {
        return Input.GetButtonDown("Player_Square" + WorkPlayerNo);
    }

    public bool GetButtonSquareRelease()
    {
        return Input.GetButtonUp("Player_Square" + WorkPlayerNo);
    }

    public bool GetButtonSquarePress()
    {
        return Input.GetButton("Player_Square" + WorkPlayerNo);
    }
    //======================================================================
    
    //======================================================================
    public bool GetButtonCrossTrigger()
    {
        return Input.GetButtonDown("Player_Cross" + WorkPlayerNo);
    }

    public bool GetButtonCrossRelease()
    {
        return Input.GetButtonUp("Player_Cross" + WorkPlayerNo);
    }

    public bool GetButtonCrossPress()
    {
        return Input.GetButton("Player_Cross" + WorkPlayerNo);
    }
    //======================================================================

    //======================================================================
    public bool GetAllButtonCircleTrigger()
    {
        for(int i = 0;i < 4 ;i++)
        {
            if(Input.GetButtonDown("Player_Circle" + i))
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
            if (Input.GetButtonUp("Player_Circle" + i))
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
            if (Input.GetButton("Player_Circle" + i))
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
            if (Input.GetButtonDown("Player_Triangle" + i))
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
            if (Input.GetButtonUp("Player_Triangle" + i))
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
            if (Input.GetButton("Player_Triangle" + i))
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
            if (Input.GetButtonDown("Player_Square" + i))
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
            if (Input.GetButtonUp("Player_Square" + i))
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
            if (Input.GetButton("Player_Square" + i))
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
            if (Input.GetButtonDown("Player_Cross" + i))
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
            if (Input.GetButtonUp("Player_Cross" + i))
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
            if (Input.GetButton("Player_Cross" + i))
            {
                return true;
            }
        }
        return false;
    }
    //======================================================================
}
