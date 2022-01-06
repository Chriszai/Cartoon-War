using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    // Start is called before the first frame update
    private float interval = 0.4f;
    private float count = 0.4f;
    public GameObject initialbullet;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {

            if (count >= interval)
            {
                count = 0;
                Vector3 pos = transform.position;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                this.transform.Translate(-0.15f, 0, 0, Space.Self);
                Invoke("response", 0.1f);//延时调用
            }
            // weapon.transform.Translate(0, -0.25f, 0);
        }

        if (Input.GetMouseButton(0))
        {
            if (count >= interval)
            {
                count = 0;
                Vector3 pos = transform.position;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                this.transform.Translate(-0.1f, 0, 0, Space.Self);
                Invoke("response", 0.1f);
            }

        }
    }
    public void response()
    {
        this.transform.position = this.transform.parent.gameObject.transform.position;
        this.transform.Translate(0, -0.25f, 0, Space.World);
    }
}
