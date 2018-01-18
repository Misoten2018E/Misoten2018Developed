using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGameObjManager : MonoBehaviour {

    enum RESULT_STA
    {
        CITYMODE,
        PLAYERMODE,
        CAPTUREON,
        NEXTSCENE,
        RESULT_STA_MAX
    }

    const float MAX_TIME = 2.0f;
    const float MAX_TIME2 = 5.0f;

    RESULT_STA resultsta;
    ResultCity Rcity;
    ResultCamera RCamera;
    public CaptureImages Capture;
    float nowtime;
    bool EfeF;

    // Use this for initialization
    void Start () {
        
        resultsta = RESULT_STA.CITYMODE;
        Rcity = GetComponentInChildren<ResultCity>();
        RCamera = GetComponentInChildren<ResultCamera>();

        Rcity.ResultCityInit();
        RCamera.ResultCameraInit();
        nowtime = 0;
        EfeF = false;
    }


    // Update is called once per frame
    void Update () {
        bool res;

        switch (resultsta)
        {
            case RESULT_STA.CITYMODE:
                res = Rcity.ResultCityUpdate();

                if (res)
                {
                    resultsta = RESULT_STA.PLAYERMODE;
                }
                break;
            case RESULT_STA.PLAYERMODE:
                res = RCamera.ResultCameraUpdate();

                if (res)
                {
                    nowtime += Time.deltaTime;

                    if (EfeF == false)
                    {
                        var par = ResourceManager.Instance.Get<EffectBase>(ConstDirectry.DirParticleEdo, ConstEffects.ConffetiLoop);
                        Vector3 pos = new Vector3(4.71f,7.51f,11.3f);

                        EffectBase Effect1 = GameObject.Instantiate(par);
                        EffectSupport.Follow(Effect1, pos, transform.up);
                        pos.y = 11.73f;
                        EffectBase Effect2 = GameObject.Instantiate(par);
                        EffectSupport.Follow(Effect2, pos, transform.up);

                        EfeF = true;
                    }
                }

                if (nowtime >= MAX_TIME)
                {
                    resultsta = RESULT_STA.CAPTUREON;
                    nowtime = 0;
                }
                break;
            case RESULT_STA.CAPTUREON:
                
                Capture.CaptureON();

                nowtime += Time.deltaTime;
                if (nowtime >= MAX_TIME2)
                {
                    resultsta = RESULT_STA.NEXTSCENE;
                }
                break;
            case RESULT_STA.NEXTSCENE:
                ResultManager.Instance.NextSceneStart();
                resultsta = RESULT_STA.RESULT_STA_MAX;
                break;
            case RESULT_STA.RESULT_STA_MAX:
                break;


        }
    }
}
