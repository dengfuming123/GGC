using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyborn : MonoBehaviour
{
    //玩家
    GameObject m_Player;
    //出生点和玩家的位置距离
    float m_Distance;
    //敌人的预支体
    public Transform m_Enemy;
    //敌人生成的数量
    public int m_EnemyCount = 0;
    //敌人生成最大数量
    public int m_EnemyMax;
    //敌人生成的时间间隔
    public float m_EnemyTime = 0.2f;
    protected Transform m_transform;

    // Use this for initialization
    void Start()
    {
        m_transform = this.transform;
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        m_Distance = Vector3.Distance(gameObject.transform.position, m_Player.transform.position);
        
        //让玩家与出生点距离小于等于20时开始创建克隆
        if ((m_Distance <= 30)&& (m_Distance >= 5))
        {
           
            //如果生成敌人的数量达到最大值 停止生成敌人
            if (m_EnemyCount >= m_EnemyMax)
            {
                return;
            }
            //时间间隔
            m_EnemyTime -= Time.deltaTime;
            //生成时间小于0时
            if (m_EnemyTime <= 0)
            {
                //重置生成时间
                m_EnemyTime = Random.Range(2, 3f);
                //生成敌人 
                Transform transformEnemy = Instantiate(m_Enemy, m_transform.position, Quaternion.identity);
                //获取敌人脚本
                Enemy enemy = transformEnemy.GetComponent<Enemy>();
                //初始化敌人
                enemy.Init(this, 1);
            }
        }
        else
        {
            return;
        }

    }
}
