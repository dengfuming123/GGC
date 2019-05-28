using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fplayer : MonoBehaviour
{
    public Transform m_transform;
    // Use this for initialization
    public float speed;
    public float life = 10; //生命值
    public GameObject player;
    /// <summary>
    /// ///////////
    /// </summary>
   
    public float _gravityActive = 1;
    public bool LadderColliding;  //爬梯状态
    Ladder ladder;
    public  GameObject LT; //左边管道
    public bool center; //是否位于可攀爬的地方
    public Vector2 targetposition;
    public Rigidbody2D rb;
    public Animator m_ani;
    public Vector2 pos0;
    public GameObject RT; //右边管道
    private SpriteRenderer spriteRenderer; //获取sprite renderer组件
    public bool jump; //判断是否能跳跃
    private GameObject Bridge;
    public float Jumpforce; //弹跳力
    private bool secondjump; //空中可以二段跳
    private float m_nextFire;
    /// 开火速率
    public float FireRate = 2.0f;
    /// 子弹对象
    public GameObject Bullet;
    /// 子弹速度
    public float BulletSpeed;

    public enum Pos
    {
        leftl,
        leftr,
        rightl,
        rightr,
        bridge,
    }
    public Pos pos;
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        m_transform = this.transform;
        player = GameObject.FindWithTag("Player");
        LT = GameObject.FindWithTag("LT");
        RT = GameObject.FindWithTag("RT");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        m_ani = player.GetComponent<Animator>();
        pos = Pos.leftl;
        pos0.x= LT.transform.position.x - 1.5f;
        jump = false;
        Bridge = GameObject.FindWithTag("Bridge");
    }

    // Update is called once per frame
    void Update()
    {
        pos0.y = m_transform.position.y;
        this.GetComponent<Rigidbody2D>().gravityScale = _gravityActive;
        m_nextFire = m_nextFire + Time.deltaTime;
        if (Input.GetMouseButton(0) && m_nextFire > FireRate)
        {
            // 获取鼠标坐标并转换成世界坐标
            Vector3 m_mousePosition = Input.mousePosition;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            // 因为是2D，用不到z轴。使将z轴的值为0，这样鼠标的坐标就在(x,y)平面上了
            m_mousePosition.z = 0;

            // 子弹角度
            float m_fireAngle = Vector2.Angle(m_mousePosition - this.transform.position, Vector2.up);

            if (m_mousePosition.x > this.transform.position.x)
            {
                m_fireAngle = -m_fireAngle;
            }

            // 计时器归零
            m_nextFire = 0;

            // 生成子弹
            GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;

            // 速度
            m_bullet.GetComponent<Rigidbody2D>().velocity = ((m_mousePosition - transform.position).normalized * BulletSpeed);

            // 角度
            m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);

        }
       
        Where();
        AnimatorStateInfo stateInfo = m_ani.GetCurrentAnimatorStateInfo(1);
        if (jump == true)
        {
            secondjump = true;
        }
        if (stateInfo.fullPathHash == Animator.StringToHash("damage.damage") && !m_ani.IsInTransition(0))
        {
            m_ani.SetBool("damage", false);
        }
        if (LadderColliding == false) //不处于攀爬状态
        {
            
            center = false;
            // Debug.LogError("ss");
            Control();

            Debug.Log("move");
        }
        else if (LadderColliding == true)
        {
           Laddermove(); }
        if ((m_transform.position.x >= pos0.x-1)||(m_transform.position.x<=pos0.x+1))
        { center = true; }
        else { center = false; }
       
    }
    public void Control()
    {
        m_ani.SetBool("climb", false);
        if (Input.GetKey(KeyCode.A))
        {
           
            m_transform.Translate(Vector2.left * Time.deltaTime*speed,Space.World);
            transform.rotation = Quaternion.Euler(0, 180, 0);
           // iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_transform.Translate(Vector2.right * Time.deltaTime*speed,Space.World);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
        }
        if (Input.GetKeyDown(KeyCode.W))   //最后翻转还是通过动画
        {if (jump == true)
            { Jump(); }
        else if(secondjump==true)
            { Jump();
              secondjump = false;
            }        
        }
    }
    protected void Jump()
    { rb.velocity = new Vector2(rb.velocity.x, Jumpforce); }
    public void Laddermove()   //攀爬时刻的行动
    {
       
        if (Input.GetKey(KeyCode.W) && (center == true))
        {
            rb.bodyType = RigidbodyType2D.Static;   //当主角从阳台跳向水管时，消去惯力影响
            rb.bodyType = RigidbodyType2D.Dynamic;
           rb.MovePosition(rb.position + Vector2.up * Time.deltaTime * speed);
            m_ani.SetBool("climb", true);

        }
        else if (Input.GetKey(KeyCode.S) && (center == true))
        {
            rb.bodyType = RigidbodyType2D.Static;   //当主角从阳台跳向水管时，消去惯力影响
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.MovePosition(rb.position + Vector2.down * Time.deltaTime * speed);
            m_ani.SetBool("climb", true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
           Offlladder();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Offrladder();
        }
        else
        { m_ani.SetBool("climb", false); }      
    }
    public void Offlladder()//离开左边朝右
    {
       
        m_ani.SetBool("climb", false);
       
        switch (pos)
        { case Pos.leftl:
                //pos = Pos.leftr;
                pos0.x = LT.transform.position.x+1.5f;
        //        iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
                Center();
                break;
          case Pos.leftr:
               // pos = Pos.rightl;
                pos0.x = RT.transform.position.x -1.5f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
               
                rb.AddForce(new Vector2(8, 0), ForceMode2D.Impulse);
                break;
          case Pos.rightl:

                //pos = Pos.rightr;
                pos0.x = RT.transform.position.x +1.5f;
             //   iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
                Center();
                break;
            case Pos.rightr:
                
                
                rb.AddForce(new Vector2(5, -5), ForceMode2D.Impulse);
                break;
            default:
                break;
        }
       
    
    }
    public void Offrladder()  //离开右边朝左

    {
        
        m_ani.SetBool("climb", false);
        //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30, 2), ForceMode2D.Force);
        switch (pos)
        {
            case Pos.leftl:
               
                rb.AddForce(new Vector2(-5, -5), ForceMode2D.Impulse);
                break;
            case Pos.leftr:
               ;
                pos0.x = LT.transform.position.x - 1.5f;
            
                Center();
                break;
            case Pos.rightl:
               // pos = Pos.leftr;
                pos0.x =LT.transform.position.x +1.5f;

                transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.AddForce(new Vector2(-8, 0), ForceMode2D.Impulse);
                break;
            case Pos.rightr:
                //pos = Pos.rightl;
                pos0.x = RT.transform.position.x -1.5f;
           //     iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
                Center();
                break;
            default:
                break;
        }
       
     
    }
    public void Where()  //判断处于水管的那个位置
    {
        if ((m_transform.position.x >= RT.transform.position.x - 3f) && (m_transform.position.x <= RT.transform.position.x))
        {
            pos = Pos.rightl;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((m_transform.position.x >=LT.transform.position.x) && (m_transform.position.x <= LT.transform.position.x + 3f))
        { pos = Pos.leftr;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if ((m_transform.position.x > RT.transform.position.x) && (m_transform.position.x <= RT.transform.position.x + 3f))
        {
            pos = Pos.rightr;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if ((m_transform.position.x >= LT.transform.position.x -3f) && (m_transform.position.x < LT.transform.position.x))
        { pos = Pos.leftl;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if((m_transform.position.x>Bridge.transform.position.x-1)&&(m_transform.position.x<Bridge.transform.position.x+1)) { pos = Pos.bridge; }
    }
    public void Center() 
    {

       
        
      iTween.MoveTo(this.gameObject, pos0, 0.2f);
}

   
      
    
    public void Damage(int n)
    { life -= n;
      m_ani.SetBool("damage", true);
    }
    
}