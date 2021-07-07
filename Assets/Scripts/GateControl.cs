using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
    public int lockcode;


    // Start is called before the first frame update
    void Start()
    {
        lockcode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockcode == 4 && transform.position.y < -122)
        {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }
}
