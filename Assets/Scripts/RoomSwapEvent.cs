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
            CloseDoors();
            SwapRooms();
        }
    }

    private void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<DoorSlamEvent>().SlamDoor();
        }
    }

    private void SwapRooms()
    {
        List<Transform> roomTransforms = new List<Transform>();
        foreach (var room in rooms)
            roomTransforms.Add(room.transform);
        roomTransforms = ShuffleTransforms(roomTransforms);

        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].transform.SetPositionAndRotation(roomTransforms[i].position, roomTransforms[i].rotation);
        }
    }

    private List<Transform> ShuffleTransforms(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
