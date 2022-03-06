using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk4Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public Player playerScript;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Invoke("response", 2f);
        playerScript.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void response()
    {
        Destroy(this.gameObject);
        playerScript.speed = 8f;
    }
}
