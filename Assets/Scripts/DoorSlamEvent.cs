using UnityEngine;

public class DoorSlamEvent : MagicEvent
{
    private enum DoorState
    {
        Open,
        Close
    }

    private GameObject hinge;
    private MagicSystem magicSys;
    private DoorState currentState;
    private bool isMoving;
    private float timer;
    private int moveDir;
    private int moveSpeed = 200;
    private float movedAmount;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        magicSys = GameObject.FindObjectOfType<MagicSystem>();
        hinge = transform.parent.gameObject;
        hinge.transform.Rotate(Vector3.forward * 140);
        currentState = DoorState.Open;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float movement = moveDir * moveSpeed * Time.deltaTime;
            hinge.transform.Rotate(Vector3.forward * movement);
            movedAmount += Mathf.Abs(movement);

            if (movedAmount >= 140)
            {
                isMoving = false;
                currentState = (currentState == DoorState.Open) ? DoorState.Close : DoorState.Open;
                movedAmount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMoving) return;

        if (other.tag == "Player")
        {
            if (magicSys.MagicVars.Intensity > 0)
            {
                int slamRoll = Random.Range(0, 100);
                if (slamRoll <= 10)
                    SlamDoor();
            }
        }
    }

    public void SlamDoor()
    {
        isMoving = true;
        moveDir = -1;
        moveSpeed = 200;

        Invoke("OpenDoor", 1 + (magicSys.MagicVars.Intensity * 0.03f));
    }

    public void OpenDoor()
    {
        isMoving = true;
        moveDir = 1;
        moveSpeed = 100;
    }
}
