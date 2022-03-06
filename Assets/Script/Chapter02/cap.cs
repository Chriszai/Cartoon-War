using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cap : MonoBehaviour
{
    public int HP = 500;
    public int curHP;
    public SpriteRenderer sr;
    public Color originColor;
    public GameObject boss_0;
    // Start is called before the first frame update
    void Start()
    {
        curHP = HP;
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        boss_0 = GameObject.Find("boss_0/BossBody_0");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = boss_0.transform.position;
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
        sr.color = Color.blue;
        Invoke("ResetColor", 0.2f);
    }
    public void ResetColor()
    {
        sr.color = originColor;
    }
}
