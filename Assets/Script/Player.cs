using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int iscollide = 0;
    public GameObject initialWeapon;
    public Vector2 movement;
    public Animator playerAnima;
    public SpriteRenderer sr;
    public Color originColor;
    public int HP = 5;
    public int curHP;
    // Start is called before the first frame update
    void Start()
    {
        playerAnima = this.GetComponent<Animator>();
        Application.targetFrameRate = 60;
        sr = GetComponent<SpriteRenderer>();
        initial();
        originColor = sr.color;
        curHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        float step = 8f * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && iscollide != 2)
        {
            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement * step);
        }
        if (Input.GetKey(KeyCode.D) && iscollide != 4)
        {
            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement * step);
        }
        if (Input.GetKey(KeyCode.W) && iscollide != 1)
        {
            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement * step);
        }
        if (Input.GetKey(KeyCode.S) && iscollide != 3)
        {
            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement * step);
        }

        if (movement == new Vector2(0, 0))
        {
            playerAnima.SetBool("run", false);
        }
        else
        {
            playerAnima.SetBool("run", true);
        }

        setLookAt();
        weaponPos();
    }


    private void setLookAt()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;    // 把Z坐标置0，放到2D平面上来

        Rigidbody2D body = this.GetComponent<Rigidbody2D>();
        if (body.position.x > target.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (body.position.x < target.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void weaponPos()
    {
        GameObject weapon = GameObject.Find("Weapon1(Clone)");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        SetGunDirection(pos);

    }

    public void SetGunDirection(Vector3 targetPos)
    {
        GameObject weapon = GameObject.Find("Weapon1(Clone)");
        weapon.transform.right = targetPos - weapon.transform.position;
        if (weapon.transform.position.x > targetPos.x)
        {
            weapon.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (weapon.transform.position.x < targetPos.x)
        {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
        }

    }

    public void initial()
    {
        Vector3 pos = transform.position;
        GameObject weapon = Instantiate(initialWeapon, pos, transform.rotation);
        weapon.transform.SetParent(this.transform);
        weapon.transform.Translate(0, -0.25f, 0);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_1"))
    //     {
    //         if (Input.GetKey(KeyCode.W))
    //         {
    //             iscollide = 1;
    //         }
    //         if (Input.GetKey(KeyCode.A))
    //         {
    //             iscollide = 2;
    //         }
    //         if (Input.GetKey(KeyCode.S))
    //         {
    //             iscollide = 3;
    //         }
    //         if (Input.GetKey(KeyCode.D))
    //         {
    //             iscollide = 4;
    //         }
    //     }
    // }
    private void OnTriggerExit2D(Collider2D collision)
    {
        iscollide = 0;
    }

    public void BeAttack(int damage)
    {
        curHP = curHP - damage;
        FlashColor();

        if (curHP <= 0)
        {
            Destroy(this.gameObject);
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
