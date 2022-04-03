using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    private AudioSource audioSource;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();

        this.magicType = MagicTypes.DoorSlam;

        magicSys = GameObject.FindObjectOfType<MagicSystem>();
        hinge = transform.parent.gameObject;
        hinge.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 140));
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
        if (isMoving || currentState == DoorState.Close) return;

        if (other.name/*change to tag?*/ == "Player")
        {
            if (magicSys.MagicVars.Intensity > 0)
            {
                int slamRoll = Random.Range(0, 100);
                if (slamRoll <= 10)
                    SlamDoor(5);
            }
        }
    }

    public void SlamDoor(float time, int speed = 300)
    {
        isMoving = true;
        moveDir = -1;
        moveSpeed = speed;

        audioSource.Play();

        Invoke("OpenDoor", time);
    }

    public void OpenDoor()
    {
        isMoving = true;
        moveDir = 1;
        moveSpeed = 100;
    }
}
