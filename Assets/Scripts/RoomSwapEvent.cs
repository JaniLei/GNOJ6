using UnityEngine;
using System.Collections.Generic;

public class RoomSwapEvent : MagicEvent
{
    private List<GameObject> rooms;
    private List<GameObject> doors;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        this.magicType = MagicTypes.RoomSwap;

        rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerMagic(object sender, MagicEventArgs e)
    {
        base.OnTriggerMagic(sender, e);

        if (e.MagicType == this.magicType)
        {
            CloseDoors(5 + (e.Intensity * 0.05f));
            Invoke("SwapRooms", 5);
        }
    }

    private void CloseDoors(float time)
    {
        foreach (var door in doors)
        {
            door.GetComponent<DoorSlamEvent>().SlamDoor(time);
        }
    }

    private void SwapRooms()
    {
        List<Vector3> roomPositions = new List<Vector3>();
        List<Quaternion> roomRotations = new List<Quaternion>();
        foreach (var room in rooms)
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                roomPositions.Add(room.transform.position);
                roomRotations.Add(room.transform.rotation);
            }
            else
            {
                roomPositions.Insert(0, room.transform.position);
                roomRotations.Insert(0, room.transform.rotation);
            }
        }
        //roomTransforms = ShuffleTransforms(roomTransforms);

        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].transform.SetPositionAndRotation(roomPositions[i], roomRotations[i]);
        }
    }

    private List<Transform> ShuffleTransforms(List<Transform> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            var temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
        return list;
    }
}
