using System.Collections.Generic;
using UnityEngine;

public class LevitationEvent : MagicEvent
{
    private List<Levitatable> levitatables;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        this.magicType = MagicTypes.Levitation;

        levitatables = new List<Levitatable>(GameObject.FindObjectsOfType<Levitatable>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void StartMagicEvent()
    {
        base.StartMagicEvent();

        // choose x amount of random levitatable objects?
        foreach (var item in levitatables)
        {
            item.StartLevitate();
        }
        Invoke("StopMagicEvent", 8/*longer based on intensity?*/);
    }

    protected override void StopMagicEvent()
    {
        base.StopMagicEvent();

        foreach (var item in levitatables)
        {
            item.StopLevitate();
        }
    }
}
