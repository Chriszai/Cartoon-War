using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
    // Start is called before the first frame update
    public int index = 0;
    public Sprite sprite0;
    public Sprite sprite1;
    void Start()
    {
        Debug.Log("ssass");
        Application.targetFrameRate = 30;
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer1 = this.GetComponent<SpriteRenderer>();
        Debug.Log(this.gameObject.name);
        //获取组件
        renderer.flipY = true;

        //获取其他节点
        GameObject obj = GameObject.Find("/rubic");
        // GameObject parent = this.transform.parent.gameObject;

        Vector3 pps = new Vector3(0, 1.0f, 0);
        this.transform.position = pps;
        this.transform.eulerAngles = new Vector3(0, 0, 90f);
        transform.localPosition = new Vector3(0, 1.0f, 0);

        GameObject target = GameObject.Find("球");
        Vector3 p1 = target.transform.position;
        Vector3 p2 = this.transform.position;
        float angle = Vector3.SignedAngle(p1, p2, Vector3.forward);
        float angle1 = Vector3.Angle(p1, p2);

    }

    // Update is called once per frame
    void Update()
    {
        float step = 0.8f * Time.deltaTime;
        this.transform.Translate(0, 0.05f, 0, Space.Self);//Space.World
        if (Input.GetMouseButtonDown(0))
        {
            doChange();
        }
    }
    void doChange()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (index == 0)
        {
            index = 1;
            renderer.sprite = this.sprite1;
        }
        else
        {
            index = 0;
            renderer.sprite = this.sprite0;
        }
    }
}
