using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster02 : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 5;
    public int attack = 1;

    public int curHP;
    public SpriteRenderer sr;
    public Color originColor;

    public GameObject initialbullet;
    public GameObject initialCoin;
    public GameObject initialMP;
    private float interval = 6f;
    private float count = 6f;
    public GameObject initialBlood;
    public AudioClip Sound;
    private AudioSource source;
    void Start()
    {
        Application.targetFrameRate = 60;
        curHP = HP;
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count >= interval)
        {
            count = 0;
            fire();
        }
    }

    public void BeAttack(int damage)
    {
        curHP = curHP - damage;
        FlashColor();
        Vector3 pos = transform.position;
        GameObject blood = Instantiate(initialBlood, pos, transform.rotation);
        if (curHP <= 0)
        {
            coinAndMP();
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

    public void fire()
    {
        Vector3 pos = transform.position;
        GameObject bullet1 = Instantiate(initialbullet, pos, transform.rotation);
        bullet1.transform.up = Vector3.left;
        GameObject bullet2 = Instantiate(initialbullet, pos, transform.rotation);
        bullet2.transform.up = Vector3.right;
        GameObject bullet3 = Instantiate(initialbullet, pos, transform.rotation);
        bullet3.transform.up = Vector3.up;
        GameObject bullet4 = Instantiate(initialbullet, pos, transform.rotation);
        bullet4.transform.up = Vector3.down;
        source.PlayOneShot(Sound);
    }
    public void coinAndMP()
    {
        for (var i = 1; i < Random.Range(1, 4); i++)
        {
            Vector3 pos = transform.position + new Vector3(i / 4f + 0.1f, 0, 0);
            GameObject coin = Instantiate(initialCoin, pos, transform.rotation);
        }
        for (var i = 0; i < Random.Range(0, 4); i++)
        {
            Vector3 pos = transform.position + new Vector3(i / 4f + 0.3f, 0.2f, 0);
            GameObject mp = Instantiate(initialMP, pos, transform.rotation);
        }

    }
}