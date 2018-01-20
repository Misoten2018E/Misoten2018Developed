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
        
        ChangeCharacter(Score.instance.GetCharacter(playerno));

        int[] scrp = SceneToSceneDataSharing.Instance.mainToResultData.PlayersScore;

        int maxscr = 0;
        int maxno = 0;

        for (int i = 0;i < scrp.Length;i++)
        {
            if (scrp[i] > maxscr)
            {
                maxscr = scrp[i];
                maxno = i;
            }
        }

        if (maxno == playerno)
        {
            Vector3 pos = transform.position;
            pos.z += 2.5f;
            transform.position = pos;
        }
        else
        {
            Transform tra = transform.Find("Winner");
            tra.gameObject.SetActive(false);
        }
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
