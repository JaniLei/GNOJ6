using System.Collections.Generic;
using UnityEngine;

public class LevitationEvent : MagicEvent
{
    private List<Levitatable> levitatables;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        levitatables = new List<Levitatable>(GameObject.FindObjectsOfType<Levitatable>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void StartMagicEvent()
    {
        base.StartMagicEvent();

        foreach (var item in levitatables)
        {
            item.StartLevitate();
        }
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
