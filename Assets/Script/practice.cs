using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice : MonoBehaviour
{
    // Start is called before the first frame update
    private bool toRight = true;
    public Sprite sprite0;
    public Sprite sprite1;

    public GameObject myPrefab;
    void Start()
    {
        Application.targetFrameRate = 60;
        transform.eulerAngles = new Vector3(0, 0, -90);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        if (toRight && sp.x > Screen.width)
        {
            toRight = false;
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (!toRight && sp.x < 0)
        {
            toRight = true;
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        float step = 0.1f + Time.deltaTime;
        transform.Translate(0, step, 0, Space.Self);
        // 首先执行Awake OnEnable Start
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(myPrefab);
        bullet.transform.position = transform.position + new Vector3(0, 1f, 0);
        bullet.name = "my bullet";
        Invoke("response",3);//延时调用
    }
}
