using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    public Texture2D tex;

    MultiInput Minput;
    UnityEngine.UI.Image image;

    // Use this for initialization
    void Start () {
        Minput = GetComponent<MultiInput>();
        image = GetComponent<UnityEngine.UI.Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Minput.GetButtonCircleTrigger())
        {
            image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            
        }
	}
}
