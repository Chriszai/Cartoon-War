using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_1 : MonoBehaviour
{
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public float waitTime;
    public float startWaitTime;
    public float speed = 2f;
    public float radius = 5f;
    public Transform playerTransform;
    public GameObject initialbullet1;
    public GameObject initialbullet2;
    public GameObject initialbullet3;
    public GameObject initialCap;
    public GameObject initialEffect;
    // public GameObject initialBullet1;
    public bool isAttack = false;
    public GameObject bullet;
    public GameObject cap;
    public GameObject bullet1;
    public GameObject effect;
    public int HP = 500;
    public int curHP;
    public SpriteRenderer sr;
    public Color originColor;
    public int accVal;
    public int status;
    private float interval = 3.5f;
    private float count = 0f;
    private float interval1 = 15f;
    private float count1 = 5f;
    public GameObject player;
    public GameObject initialBlood;
    public enum EnemyState
    {
        Idle,
        Tracking,
        patrol
    }
    public EnemyState State;
    // Start is called before the first frame update
    void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        /*
        共有两种状态，攻击状态，跟踪状态，每10秒转换一次
        每过十秒会出现无敌状态，持续5秒
        攻击一个是地上产生火焰，会灼伤玩家，一个是向前释放一个火焰阵型。另一个是埋一个地雷
        
         */
        State = EnemyState.patrol;
        this.GetComponent<Animator>().SetBool("run", false);
        player = GameObject.Find("player");
        Application.targetFrameRate = 60;
        curHP = HP;
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        observe();
        switch (State)
        {
            case EnemyState.patrol:
                patrol();
                break;
            case EnemyState.Tracking:
                Tracking();
                break;
            default:
                break;
        }

        if (count1 <= interval1)
        {
            count1 += Time.deltaTime;
        }
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count >= interval)
        {
            {
                attackWay();
            }
            count = 0;
        }
        if (count1 >= interval1)
        {
            {
                invincible();
            }
            count1 = 0;
        }
    }
    public void invincible()
    {
        Vector2 pos = transform.position;
        cap = Instantiate(initialCap, pos, transform.rotation);
    }
    public Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
    public void attack1()
    {
        Vector2 pos = GetRandomPos();
        GameObject bullet = Instantiate(initialbullet1, pos, transform.rotation);
    }
    public void attack2()
    {
        Vector2 pos = transform.position;
        for (var i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(initialbullet2, pos, transform.rotation);
            bullet.transform.right = playerTransform.position - transform.position;
            bullet.transform.Rotate(0, 0, i * 7f);
        }
        GameObject bullet1 = Instantiate(initialbullet2, pos, transform.rotation);
        bullet1.transform.right = playerTransform.position - transform.position;
        for (var i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(initialbullet2, pos, transform.rotation);
            bullet.transform.right = playerTransform.position - transform.position;
            bullet.transform.Rotate(0, 0, i * -7f);
        }

    }
    public void attack3()
    {
        Vector2 pos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y + 10f, rightUpPos.position.y + 10f));
        GameObject bullet = Instantiate(initialbullet3, pos, transform.rotation);
    }
    public void attackWay()
    {
        int ran = Random.Range(0, 4);//(0,4)
        if (ran == 1)
        {
            attack1();
        }
        else if (ran == 2)
        {
            for (var i = 0; i < 3; i++)
            {
                Invoke("attack2", i / 10f + 0.4f);
            }
        }
        else
        {
            for (var i = 0; i < 1; i++)
            {
                Invoke("attack3", i / 10f + 0.1f);
            }
        }
    }
    public void BeAttack(int damage)
    {
        if (cap)
        {
            return;
        }
        curHP = curHP - damage;

        FlashColor();
        Vector3 pos = transform.position;
        GameObject blood = Instantiate(initialBlood, pos, transform.rotation);
        if (curHP <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
    public void FlashColor()
    {
        sr.color = Color.red;
        Invoke("ResetColor", 0.2f);
    }
    public void ResetColor()
    {
        sr.color = originColor;
    }
    public void observe()
    {
        // GetComponent<AIPath>().maxSpeed = 0;
        var crossX = transform.position.x - playerTransform.position.x;
        var crossY = transform.position.y - playerTransform.position.y;
        if (crossX > 0 && crossX < 6f && sr.flipX == true && (crossY < 3f || crossY > -3f))
        {
            State = EnemyState.Tracking;
            Vector2 pos = transform.position + new Vector3(1f, 1f, 0);
            if (!effect)
            {
                effect = Instantiate(initialEffect, pos, transform.rotation);
                effect.transform.SetParent(this.transform);
            }
        }
        else if (crossX < 0 && crossX > -6f && sr.flipX == false && (crossY < 3f || crossY > -3f))
        {
            State = EnemyState.Tracking;
            Vector2 pos = transform.position + new Vector3(1f, 1f, 0);
            if (!effect)
            {
                effect = Instantiate(initialEffect, pos, transform.rotation);
                effect.transform.SetParent(this.transform);
            }

        }
        else
        {
            State = EnemyState.patrol;
            Destroy(this.effect);
        }
    }
    public void patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            this.GetComponent<Animator>().SetBool("run", false);
            if (waitTime <= 0)
            {
                this.GetComponent<Animator>().SetBool("run", true);
                movePos.position = GetRandomPos();

                if (movePos.position.x > transform.position.x)
                {

                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    public void Tracking()
    {
        this.GetComponent<Animator>().SetBool("run", true);
        var cross = playerTransform.position.x - transform.position.x;
        if (cross > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }
}
