using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster04 : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 5;
    public int attack = 1;

    public float speed = 2f;
    public int curHP;
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public float waitTime;
    public float startWaitTime;
    public SpriteRenderer sr;
    public Color originColor;
    public Transform playerTransform;
    public float radius;
    void Start()
    {
        Application.targetFrameRate = 60;
        curHP = HP;
        movePos.position = GetRandomPos();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance < radius)
            {
                var cross = Vector3.Cross(movePos.position, transform.position);
                if (cross.z > 0)
                {

                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
            else
            {
                movement();
            }
        }

    }

    public void movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                var cross = Vector3.Cross(movePos.position, transform.position);
                if (cross.z > 0)
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

    public void BeAttack(int damage)
    {
        curHP = curHP - damage;
        FlashColor();

        if (curHP <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }

    public Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.SendMessage("BeAttack", attack);
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
}
