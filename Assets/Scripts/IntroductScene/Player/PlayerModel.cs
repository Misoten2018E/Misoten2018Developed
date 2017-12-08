using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {

    Animator ani;
    MultiInput Minput;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        Minput = GetComponent<MultiInput>();

        ani.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Minput.GetButtonCircleTrigger())
        {
            ani.enabled = true;
        }
    }
}
