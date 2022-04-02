using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitatable : Interactable
{
    private Rigidbody rb;
    private Vector3 originPos;
    private bool isLevitating;
    private bool isReturning;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, originPos, Time.deltaTime);

            if ((originPos - transform.position).magnitude < 0.1)
            {
                transform.position = originPos;
                isReturning = false;
                rb.useGravity = true;
            }
        }
    }

    public void StartLevitate()
    {
        isLevitating = true;
        rb.useGravity = false;
        rb.AddForce(Vector3.up * 20, ForceMode.Acceleration);
    }

    public void StopLevitate()
    {
        if (!isReturning)
        {
            isLevitating = false;
            rb.useGravity = true;
        }
    }

    public void ReturnToOrigin()
    {
        isReturning = true;
        isLevitating = false;
    }

    protected override void Interact(GameObject player)
    {
        base.Interact(player);

        if (isLevitating)
        {
            ReturnToOrigin();
        }
    }
}
