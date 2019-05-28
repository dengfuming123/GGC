using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upjuan : MonoBehaviour {
    public GameObject bg1;  //背景1
    public GameObject bg2;  //背景2
    public float y1;
    public float y2;
    public GameObject Player;
    public float count = 1;
	// Use this for initialization
	void Start () {
		y1= bg1.transform.localScale.y * bg1.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
       
        y2 = bg2.transform.localScale.y * bg2.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        Player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.y > bg1.transform.position.y)
        { bg2.transform.localPosition =new Vector2( bg2.transform.position.x,bg1.transform.position.y+ y1 / 2+ y2/2); }
        else
        { bg2.transform.localPosition = new Vector2(bg2.transform.position.x, bg1.transform.position.y - y1 / 2 - y2 / 2); }
        if (Player.transform.position.y > bg2.transform.position.y)
        { bg1.transform.localPosition = new Vector2(bg1.transform.position.x, bg2.transform.position.y + y1 / 2 + y2 / 2); }
        else
        { bg1.transform.localPosition = new Vector2(bg1.transform.position.x, bg2.transform.position.y - y1 / 2 - y2 / 2); }

    }
}
