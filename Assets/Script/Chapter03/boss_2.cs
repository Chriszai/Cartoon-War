using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public float waitTime;
    public float startWaitTime;
    public float speed = 2f;
    public GameObject initialbullet1;
    public GameObject initialbullet2;
    public GameObject initialbullet3;
    public GameObject initialbullet4;
    public GameObject initialstate1;
    public GameObject initialstate2;
    public GameObject initialstate3;
    public GameObject initialstate4;
    public Transform playerTransform;
    private float interval = 4f;
    public GameObject effect;
    private float count = 1f;
    public int HP = 300;
    public int curHP;
    public GameObject initialBlood;
    public SpriteRenderer sr;
    public Color originColor;
    GameObject state;
    public int atk;
    public GameObject initialEffect;
    public Vector3 startPoint;
    public enum EnemyState
    {
        Idle,
        Tracking,
        patrol
    }
    public EnemyState move;
    void Start()
    {
        /*
        三种状态，普通状态，红色散弹攻击。激光状态，使用激光射击。
        暴怒状态，范围子弹攻击。
        时不时进行冰点攻击，冰冻玩家。 
         */
        startPoint = transform.position;
        curHP = HP;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Invoke("attack4", 5f);
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        move = EnemyState.patrol;
    }

    // Update is called once per frame
    void Update()
    {
        observe();
        switch (move)
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
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count >= interval)
        {
            switch (atk)
            {
                case 1:
                    attack1();
                    break;
                case 2:
                    attack2();
                    break;
                case 3:
                    attack3();
                    break;
                default:
                    break;
            }
            count = 0;
        }
    }
    public void attack1()
    {
        Vector3 pos = transform.position;
        GameObject bullet1 = Instantiate(initialbullet1, pos, transform.rotation);
        bullet1.transform.right = playerTransform.position - transform.position;
        for (var i = 1; i < 4; i++)
        {
            GameObject bullet = Instantiate(initialbullet1, pos, transform.rotation);
            bullet.transform.right = playerTransform.position - transform.position;
            bullet.transform.Rotate(0, 0, i * 8f);
        }
        for (var i = 1; i < 4; i++)
        {
            GameObject bullet = Instantiate(initialbullet1, pos, transform.rotation);
            bullet.transform.right = playerTransform.position - transform.position;
            bullet.transform.Rotate(0, 0, i * -8f);
        }
    }
    public void attack2()
    {
        Vector3 pos = transform.position;
        GameObject bullet = Instantiate(initialbullet2, pos, transform.rotation);
        bullet.transform.right = playerTransform.position - transform.position;
        bullet.transform.Translate(0, 1f, 0, Space.Self);
    }
    public void attack3()
    {
        for (var i = 0; i < 5; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, i / 1.5f, 0);
            GameObject bullet = Instantiate(initialbullet3, pos, transform.rotation);
            bullet.transform.right = playerTransform.position - transform.position;
        }
    }
    public void attack4()
    {
        Vector3 pos = transform.position;
        GameObject bullet = Instantiate(initialbullet4, pos, transform.rotation);
        bullet.transform.right = playerTransform.position - transform.position;
        Invoke("attack4", 2f);
    }
    public void attackWay(int ran)
    {
        switch (ran)
        {
            case 0:
                state = Instantiate(initialstate1, startPoint, transform.rotation);
                break;
            case 1:
                state = Instantiate(initialstate2, startPoint, transform.rotation);
                break;
            case 2:
                state = Instantiate(initialstate3, startPoint, transform.rotation);
                break;
            default:
                break;
        }
        // state.transform.GetChild(0).gameObject.GetComponent<boss_2>().HP = curHP;
        Destroy(this.transform.parent.gameObject);
    }
    public void BeAttack(int damage)
    {
        curHP = curHP - damage;

        FlashColor();
        Vector3 pos = transform.position;
        GameObject blood = Instantiate(initialBlood, pos, transform.rotation);
        if (curHP <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
        else if (curHP <= 200)
        {
            attackWay(2);
        }
        else if (curHP <= 400)
        {
            attackWay(1);
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
            move = EnemyState.Tracking;
            Vector2 pos = transform.position + new Vector3(1f, 1f, 0);
            if (!effect)
            {
                effect = Instantiate(initialEffect, pos, transform.rotation);
                effect.transform.SetParent(this.transform);
            }
        }
        else if (crossX < 0 && crossX > -6f && sr.flipX == false && (crossY < 3f || crossY > -3f))
        {
            move = EnemyState.Tracking;
            Vector2 pos = transform.position + new Vector3(1f, 1f, 0);
            if (!effect)
            {
                effect = Instantiate(initialEffect, pos, transform.rotation);
                effect.transform.SetParent(this.transform);
            }

        }
        else
        {
            move = EnemyState.patrol;
            Destroy(this.effect);
        }
    }
    public void patrol()
    {
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
    public void Tracking()
    {
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
    public Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
