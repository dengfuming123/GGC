using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [HideInInspector]
    public GameObject player;

    public GameObject[] doors;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 使用传送门功能是请把脚本放挂在角色上
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        //进行一对门之间的传送
        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_00")
        {
            player.transform.position = doors[1].transform.position;
        }
        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_01")
        {
            player.transform.position = doors[0].transform.position;
        }

        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_10")
        {
            player.transform.position = doors[3].transform.position;
        }
        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_11")
        {
            player.transform.position = doors[2].transform.position;
        }

        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_20")
        {
            player.transform.position = doors[5].transform.position;
        }
        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_21")
        {
            player.transform.position = doors[4].transform.position;
        }

        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_30")
        {
            player.transform.position = doors[7].transform.position;
        }
        if (col.gameObject.tag == "Door" && col.gameObject.name == "Door_31")
        {
            player.transform.position = doors[6].transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < 8; i++)
        {
            Destroy(doors[i].GetComponent<BoxCollider2D>());
        }
        Invoke("Pause", 1.5f);
    }

    void Pause()
    {
        for (int i = 0; i < 8; i++)
        {
            doors[i].AddComponent<BoxCollider2D>();
        }
    }
}
