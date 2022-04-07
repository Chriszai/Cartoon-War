using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackAim_0 : MonoBehaviour
{
    public GameObject initialbullet;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("launch", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void launch()
    {
        Vector3 pos = transform.position + new Vector3(0, 10f, 0);
        GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
        Invoke("die", 1f);

    }
    public void die()
    {
        Destroy(this.gameObject);
    }
}
