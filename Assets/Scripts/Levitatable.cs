using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitatable : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevitate()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.up * 100, ForceMode.Acceleration);
    }

    public void StopLevitate()
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
    }
}
