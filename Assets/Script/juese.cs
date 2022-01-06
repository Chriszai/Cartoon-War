using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juese : MonoBehaviour
{
    // Start is called before the first frame update

    public int number = 10;
    public string hello = "ss";

    void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            GameObject obj1 = GameObject.Find("rubik");
            GameObject target = GameObject.Find("物体");
            obj1.transform.SetParent(target.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
