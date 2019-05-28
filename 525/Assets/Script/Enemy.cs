using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public bool live;
    public Rigidbody2D rb;
    public float speed;
    public Animator m_ani;
    public int damage;
    protected Enemyborn m_spawn;
    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.transform.Translate(new Vector2(-0.1f, 0f));
        //if (gameObject.transform.position.y < player.transform.position.y - 10)  //掉落得足够远的时候 消失
        //{ gameObject.SetActive(false);  }
	}
    protected virtual void OnTriggerEnter2D(Collider2D collision)  //与玩家碰撞后受到冲击
    {
        if (collision.transform.tag=="Player")
        {
            int f = Random.Range(5, 10);
            
            collision.transform.GetComponent<Fplayer>().Damage(damage);
            rb.AddForce(new Vector3(f, 15,0),ForceMode2D.Impulse);
            live = false;
            StartCoroutine(gr());
        }

    }
    IEnumerator gr()  //1秒后受重力作用落下
    { yield return new WaitForSeconds(1f);
        rb.gravityScale = 1;
    }
    public void Init(Enemyborn spawn, int a)
    {

        m_spawn = spawn;
        m_spawn.m_EnemyCount += a;
    }
}
