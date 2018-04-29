using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    public GameObject Bomb;
    public float force;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bomb = Instantiate(Bomb);
            bomb.transform.position = transform.position + transform.forward * 2;
            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
    }
}