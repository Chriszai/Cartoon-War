using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalControl : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject player;
    public int curHP;
    public int curMP;
    public int coin;
    public int weapon1;
    public int weapon2;
    Transform[] children;

    void Start()
    {
        player = GameObject.Find("player");
    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("player");
    }
    public int getCurHP()
    {
        return curHP;
    }
    public int getCurMP()
    {
        return curMP;
    }
    public int getCoin()
    {
        return coin;
    }
    public int getWeapon1()
    {
        return weapon1;
    }
    public int getWeapon2()
    {
        return weapon2;
    }
    public void save()
    {
        children = player.GetComponentsInChildren<Transform>();
        curHP = player.GetComponent<Player>().getCurHP();
        curMP = player.GetComponent<Player>().getCurMP();
        coin = player.GetComponent<Player>().getCoin();
        if (children.Length == 3)
        {
            weapon1 = player.transform.GetChild(0).gameObject.GetComponent<WeaponNum>().getNum();
            weapon2 = player.transform.GetChild(1).gameObject.GetComponent<WeaponNum>().getNum();
        }
        else
        {
            weapon1 = player.transform.GetChild(0).gameObject.GetComponent<WeaponNum>().getNum();
        }
    }

}
