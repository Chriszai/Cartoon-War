using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster01 : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 3;
    public int damage = 1;

    public int curHP;
    public SpriteRenderer sr;
    public Color originColor;
    public GameObject initialCoin;
    public GameObject initialMP;
    void Start()
    {
        curHP = HP;
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {

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
