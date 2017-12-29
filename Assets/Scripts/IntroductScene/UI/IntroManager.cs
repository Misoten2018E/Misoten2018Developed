using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour, IFIntroStartEvent
{

    public List<GameObject> IntroObjList;

    Animator ManagerAni;

	// Use this for initialization
	//void Start () {
       
 //   }
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void StartEvent()
    {

        isEnd = false;

        Animator Ani;

        ManagerAni = GetComponent<Animator>();
        ManagerAni.enabled = false;

        for (int i = 0; i < IntroObjList.Count; i++)
        {
            Ani = IntroObjList[i].GetComponent<Animator>();
            Ani.enabled = false;
        }
    }

    public void ManagerAniON()
    {
        ManagerAni.enabled = true;
    }

    void IntroObjAniON(int index)
    {
        Animator Ani;

        Ani = IntroObjList[index].GetComponent<Animator>();
        Ani.enabled = true;
    }

    public void ManagerAniOFF()
    {
        ManagerAni.enabled = false;
    }

    void IntroObjAniOFF(int index)
    {
        Animator Ani;

        Ani = IntroObjList[index].GetComponent<Animator>();
        Ani.enabled = false;
    }

    public void NextScene()
    {
        StartMainGameScene();
    }

    /// <summary>
	/// メインのゲームに移る
	/// </summary>
	void StartMainGameScene()
    {

        if (isEnd)
        {
            return;
        }

        IntroductManager.Instance.NextSceneStart();
        print("Nextscene OK");
        isEnd = true;
    }


    bool _isEnd;
    public bool isEnd
    {
        private set { _isEnd = value; }
        get { return _isEnd; }
    }
}
