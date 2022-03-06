using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attribute : MonoBehaviour
{
    public GameObject player;
    public Text healthValue;
    public int curHP;
    public int maxHP;
    public Image HPBar;
    ////////// HP
    public Text armorValue;
    public int curArmor;
    public int maxArmor;
    public Image armorBar;
    /////////// MP
    public Text MPValue;
    public int curMP;
    public int maxMP;
    public Image MPBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHP = player.GetComponent<Player>().getHP();
        curHP = maxHP;

        maxArmor = player.GetComponent<Player>().getArmor();
        curArmor = maxArmor;

        maxMP = player.GetComponent<Player>().getMP();
        curMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        curHP = player.GetComponent<Player>().getCurHP();
        HPBar.fillAmount = (float)curHP / (float)maxHP;
        healthValue.text = curHP.ToString() + "/" + maxHP.ToString();

        curArmor = player.GetComponent<Player>().getCurArmor();
        armorBar.fillAmount = (float)curArmor / (float)maxArmor;
        armorValue.text = curArmor.ToString() + "/" + maxArmor.ToString();

        curMP = player.GetComponent<Player>().getCurMP();
        MPBar.fillAmount = (float)curMP / (float)maxMP;
        MPValue.text = curMP.ToString() + "/" + maxMP.ToString();
    }
}
