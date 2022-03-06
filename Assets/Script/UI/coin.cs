using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    public GameObject player;
    public int curCoin;
    public Text coinValue;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curCoin = player.GetComponent<Player>().getCoin();
    }

    // Update is called once per frame
    void Update()
    {
        curCoin = player.GetComponent<Player>().getCoin();
        coinValue.text = curCoin.ToString();
    }
}
