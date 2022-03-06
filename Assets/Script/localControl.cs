using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip Sound2;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
