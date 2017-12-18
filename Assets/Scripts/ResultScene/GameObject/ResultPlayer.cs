using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour {

    public int playerno;
    public GameObject Hero;
    public GameObject Heel;
    public GameObject Normal;
    public GameObject Specialist;

    // Use this for initialization
    void Start () {
        print("player"+ playerno + Score.instance.GetCharacter(playerno));
        ChangeCharacter(Score.instance.GetCharacter(playerno));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ChangeCharacter(int Changechar)
    {
        GameObject obj;
        Vector3 workpos = new Vector3();
        workpos.Set(transform.position.x, transform.position.y, transform.position.z);

        switch (Changechar)
        {
            case ConstPlayerSta.NormalCharacter:
                obj = Instantiate(Normal, workpos, Quaternion.identity)as GameObject;
                obj.transform.parent = transform;
                break;
            case ConstPlayerSta.HeroCharacter:
                obj = Instantiate(Hero, workpos, Quaternion.identity) as GameObject;
                obj.transform.parent = transform;
                break;
            case ConstPlayerSta.HeelCharacter:
                obj = Instantiate(Heel, workpos, Quaternion.identity) as GameObject;
                obj.transform.parent = transform;
                break;
            case ConstPlayerSta.SpecialistCharacter:
                obj = Instantiate(Specialist, workpos, Quaternion.identity) as GameObject;
                obj.transform.parent = transform;
                break;
        }

    }
}
