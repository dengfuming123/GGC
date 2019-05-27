using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    
  
    public float timer; //跟踪主角的时间
    public Vector2 pos;//记录初始位置
    public GameObject player;
    public Vector2 place;//触发位置
    public int i;
    public Vector2 power;//力量
    public float x;//初始力量后面慢慢增加
    // Use this for initialization
    void Start()
    {
        x = 8;
        player = GameObject.FindWithTag("Player");
        if (transform.gameObject.name == "Leftpalm")
        {
            i = 1;
        }
        else if (transform.gameObject.name == "Rightpalm")
        { i = 2; }
        switch (i)
        { case 1:
                Leftpalm();
                break;
          case 2:
                Rightpalm();
                break;
          default:
                break;
        }
    }
    void Update()
    {if (player.transform.position.x > place.x)
        {
            x += Time.deltaTime;
            gameObject.GetComponent<Rigidbody2D>().AddForce(power, ForceMode2D.Force); } }
    void Rightpalm()
    { pos = transform.position;
        power = new Vector3(-5, 0, 0);
      //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true); //与玩家不碰撞
    }
    void Leftpalm()
    { pos = transform.position;
        power = new Vector3(x, 0, 0);
    }
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "palm")
        {


            iTween.MoveTo(gameObject, pos, 1f);
         
        }

    }
}
