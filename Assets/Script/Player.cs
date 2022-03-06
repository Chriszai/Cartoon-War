using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int iscollide = 0;
    private float interval = 5f;
    private float count = 5f;
    private float rate = 1.5f;
    public GameObject initialWeapon;
    public Vector2 movement;
    public Animator playerAnima;
    public SpriteRenderer sr;
    public Color originColor;
    public int MaxHP = 5;
    public int curHP;

    public int MaxMP = 180;
    public int curMP;
    public int MaxArmor = 5;
    public int curArmor;
    public int coin;
    public int weapon1;
    public int weapon2;
    public float speed = 8f;
    public AudioClip Sound;
    public AudioClip Sound2;
    public AudioClip Sound3;
    private AudioSource source;
    private float interval1 = 0.35f;
    private float count1 = 0.35f;

    public globalControl gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("globalControl").GetComponent<globalControl>();
        playerAnima = this.GetComponent<Animator>();
        Application.targetFrameRate = 60;
        sr = GetComponent<SpriteRenderer>();
        initial();
        originColor = sr.color;
        curArmor = MaxArmor;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        setLookAt();
        weaponPos();
        changeAndHideWeapon();
        armorRefresh();
        if (count1 <= interval1)
        {
            count1 += Time.deltaTime;
        }
    }
    public void move()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        float step = speed * Time.deltaTime;
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
            if (count1 >= interval1)
            {
                source.PlayOneShot(Sound2);
                count1 = 0;
            }
            playerAnima.SetBool("run", true);
        }
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
        GameObject weapon = this.gameObject.transform.GetChild(0).gameObject;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        SetGunDirection(pos);

    }
    public void changeAndHideWeapon()
    {
        if (Input.GetMouseButtonDown(1) && this.transform.GetComponentsInChildren<Transform>(true).Length > 2)
        {
            source.PlayOneShot(Sound);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).transform.SetAsFirstSibling();
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void SetGunDirection(Vector3 targetPos)
    {
        GameObject weapon = this.gameObject.transform.GetChild(0).gameObject;
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
        if (gc.getWeapon1() != 0)
        {
            weapon1 = gc.getWeapon1();
            initialWeapon = GameObject.Find("WeaponList").transform.GetChild(weapon1 - 1).gameObject;
            GameObject w1 = Instantiate(initialWeapon, pos, transform.rotation);
            w1.transform.SetParent(this.transform);
            w1.transform.Translate(0, -0.25f, 0);
            if (gc.getWeapon2() != 0)
            {
                weapon2 = gc.getWeapon2();
                initialWeapon = GameObject.Find("WeaponList").transform.GetChild(weapon2 - 1).gameObject;
                GameObject w2 = Instantiate(initialWeapon, pos, transform.rotation);
                w2.transform.SetParent(this.transform);
                w2.transform.Translate(0, -0.25f, 0);
            }
        }
        else
        {
            GameObject w3 = Instantiate(initialWeapon, pos, transform.rotation);
            w3.transform.SetParent(this.transform);
            w3.transform.Translate(0, -0.25f, 0);
        }
        if (gc.getCoin() != 0)
        {
            coin = gc.getCoin();
        }
        if (gc.getCurHP() != 0)
        {
            curHP = gc.getCurHP();
        }
        else
        {
            curHP = MaxHP;
        }
        if (gc.getCurMP() != 0)
        {
            curMP = gc.getCurMP();
        }
        else
        {
            curMP = MaxMP;
        }
    }
    public void armorRefresh()
    {
        if (count < interval)
        {
            count += Time.deltaTime;
        }
        else
        {
            if (rate < 1.5f)
            {
                rate += Time.deltaTime;
            }
            if (curArmor < MaxArmor && rate >= 1.5f)
            {
                curArmor = curArmor + 1;
                rate = 0;
            }
        }
    }

    public void BeAttack(int damage)
    {
        source.PlayOneShot(Sound3);
        if (curArmor >= damage)
        {
            curArmor = curArmor - damage;
        }
        else if (curArmor < damage)
        {
            curHP = curHP + curArmor - damage;
            curArmor = 0;
        }
        FlashColor();

        if (curHP <= 0)
        {
            this.gameObject.SetActive(false);
            // Destroy(this.gameObject);
        }
        count = 0;

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
    public void Heal(int HP, int MP)
    {
        curHP = curHP + HP;
        if (curHP > MaxHP)
        {
            curHP = MaxHP;
        }

        curMP = curMP + MP;
        if (curMP > MaxMP)
        {
            curMP = MaxMP;
        }
    }
    public int getHP()
    {
        return MaxHP;
    }
    public int getCurHP()
    {
        return curHP;
    }
    public int getArmor()
    {
        return MaxArmor;
    }
    public int getCurArmor()
    {
        return curArmor;
    }
    public int getMP()
    {
        return MaxMP;
    }
    public int getCurMP()
    {
        return curMP;
    }
    public void changeMP(int num)
    {
        curMP = curMP + num;
    }
    public void setMP(int val)
    {
        curMP = curMP + val;
        if (curMP > MaxMP)
        {
            curMP = MaxMP;
        }
    }
    public void getProp(int c, int MP)
    {
        curMP = curMP + MP;
        if (curMP > MaxMP)
        {
            curMP = MaxMP;
        }
        coin = coin + c;
    }
    public int getCoin()
    {
        return coin;
    }
}
