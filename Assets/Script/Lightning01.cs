using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning01 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool call;
    public bool stop;
    public GameObject initialmonster;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        call = true;
        foreach (Transform child in this.transform.parent.gameObject.transform)
        {
            if (child.tag == "monster")
            {
                call = false;
            }
        }
        if (call == true && stop == false)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            Invoke("CallMonster", 1);
            stop = true;
            Invoke("DestroySelf", 1);//延时调用
        }
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void CallMonster()
    {
        Vector3 pos = transform.position;
        GameObject monster = Instantiate(initialmonster, pos, transform.rotation);
    }
}
