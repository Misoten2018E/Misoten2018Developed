using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {

    Animator ani;
    Animator modelani;
    MultiInput Minput;
    Vector3 oldpos;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        Minput = GetComponent<MultiInput>();
        modelani = transform.Find("BaseModel_Human").GetComponent<Animator>();

        ani.enabled = false;
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Minput.GetButtonCircleTrigger())
        {
            ani.enabled = true;
        }

        Vector3 v = transform.position - oldpos;
        v.Normalize();
       
        modelani.SetFloat("Speed",v.magnitude);

        oldpos = transform.position;
    }
}
