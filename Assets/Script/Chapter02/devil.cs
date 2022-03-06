using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devil : MonoBehaviour
{
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public float waitTime;
    public float startWaitTime;
    public float speed = 2f;
    public float radius = 5f;
    public Transform playerTransform;
    public GameObject initialbullet;
    public GameObject initialbullet2;
    public GameObject initialbullet3;
    public GameObject initialCap;
    public GameObject initialaim;
    // public GameObject initialBullet1;
    public bool isAttack = false;
    public GameObject bullet;
    public GameObject cap;
    public GameObject bullet1;
    public int HP = 1000;
    public int curHP;
    public SpriteRenderer sr;
    public Color originColor;
    public int accVal;
    public int status;
    private float interval = 3.5f;
    private float count = 0f;
    private float interval1 = 30f;
    private float count1 = 0f;
    public GameObject player;
    public GameObject initialBlood;

    // Start is called before the first frame update
    void Start()
    {
        status = 1;
        /*
        在一个方框中随机移动，当敌人靠近一定范围内近战攻击
        当积累的伤害达到一定程度会生成保护盾，当保护盾被攻破的时候会陷入疲惫。护盾期间会改变攻击方式。
        每隔4秒钟会进行一阵攻击，共两种方法。当血量第一次掉入百分之30以下时会增长至60.
        每被攻击10%血时会恢复玩家魔法值，当血量进入百分之10时会使用范围伤害大招。
        每隔60秒进入愤怒状态，持续10秒，该状态会被打破。子弹飞行速度增加，变成每隔三秒进行一次攻击。
         */
        player = GameObject.Find("player");
        Application.targetFrameRate = 60;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        curHP = HP;
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count1 <= interval1)
        {
            count1 += Time.deltaTime;
        }

        if (count1 >= interval1 && status == 1)
        {
            changeState();
            count1 = 0;
        }
        else if (count1 >= interval1 / 2 && status == 2)
        {
            status = 1;
            count1 = 0;
        }

        if (isAttack == false)
        {
            movement();
        }

        attack();
        if (count >= interval)
        {
            if (cap)
            {
                attack1();
            }
            else
            {
                attackWay();
            }
            count = 0;
        }
        restoreMP();
    }
    public void movement()
    {
        if (movePos.position == transform.position)
        {
            this.GetComponent<Animator>().SetBool("run", false);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("run", true);
        }
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
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
    public void attack()
    {
        float distance = (transform.position - playerTransform.position).sqrMagnitude;
        if (distance < radius && isAttack == false)
        {
            Vector3 pos = transform.position;
            bullet = Instantiate(initialbullet, pos, transform.rotation);
            isAttack = true;
        }
        if (distance > radius && bullet)
        {
            Destroy(bullet);
            isAttack = false;
        }
    }
    public Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
    public void accDamage(int damage)
    {
        accVal += damage;
        if (accVal >= 500)
        {
            createCap();
            accVal = 0;
        }
    }
    public void createCap()
    {
        Vector3 pos = transform.position;
        cap = Instantiate(initialCap, pos, transform.rotation);
    }
    public void BeAttack(int damage)
    {
        curHP = curHP - damage;

        FlashColor();
        Vector3 pos = transform.position;
        GameObject blood = Instantiate(initialBlood, pos, transform.rotation);
        accDamage(damage);
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

    public void attack1()
    {
        Vector2 pos = new Vector2(Random.Range(leftDownPos.position.x - 3f, rightUpPos.position.x + 3f), Random.Range(leftDownPos.position.y - 3f, rightUpPos.position.y + 3f));
        GameObject aim = Instantiate(initialaim, pos, transform.rotation);
    }
    public void attack2()
    {
        Vector2 pos = transform.position;
        for (var i = 1; i < 21; i++)
        {
            GameObject bullet2 = Instantiate(initialbullet2, pos, transform.rotation);
            bullet2.transform.Rotate(0, 0, i * 16f);
        }
    }
    public void attack3()
    {
        Vector2 pos = transform.position;
        GameObject bullet3 = Instantiate(initialbullet3, pos, transform.rotation);
    }
    public void attackWay()
    {
        int ran = Random.Range(0, 4);//(0,4)
        if (ran == 1)
        {
            for (var i = 0; i < 20; i++)
            {
                attack1();
            }

        }
        else if (ran == 2)
        {
            for (var i = 0; i < 5; i++)
            {
                Invoke("attack2", i / 10f + 0.1f);
            }
        }
        else
        {
            for (var i = 0; i < 10; i++)
            {
                Invoke("attack3", i / 10f + 0.1f);
            }
        }
    }
    public void restoreMP()
    {
        if (curHP == 100 || curHP == 200 || curHP == 300 || curHP == 400 || curHP == 500 || curHP == 600 || curHP == 700 || curHP == 800 || curHP == 900)
        {
            player.GetComponent<Player>().setMP(100);
        }
    }
    public void changeState()
    {
        if (status == 1)
        {
            status = 2;
            interval = 2.5f;
        }
    }
}
