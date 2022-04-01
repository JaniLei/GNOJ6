using System;
using UnityEngine;


public enum MagicTypes
{
    DoorSlam,
    RoomSwap,
    Levitation
}

public class MagicEventArgs : EventArgs
{
    public MagicTypes MagicType;
    public int Escalation;
    public float Intensity;
}

public class MagicSystem : MonoBehaviour
{
    private readonly int _magicTypesCount = Enum.GetValues(typeof(MagicTypes)).Length;

    public delegate void TriggerMagicEventHandler(object sender, MagicEventArgs e);
    public event EventHandler<MagicEventArgs> TriggerMagic;
    public MagicEventArgs MagicVars = new MagicEventArgs();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*if (threshold reached)
        {
            MagicVars.MagicType = SelectNextMagic();
            OnTriggerMagic(args);
        }*/
    }

    protected virtual void OnTriggerMagic(MagicEventArgs e)
    {
        TriggerMagic?.Invoke(this, e);
    }

    public MagicTypes SelectNextMagic()
    {
        // Select magic type (randomly? based on player actions?)
        var nextMagic = (MagicTypes)UnityEngine.Random.Range(0, _magicTypesCount);
        return nextMagic;
    }

    public void IncreaseIntensity(float amount)
    {
        MagicVars.Intensity += amount;
        if (MagicVars.Intensity > 100)
            MagicVars.Intensity = 100;
    }

    public void IncreaseEscalation(int amount)
    {
        MagicVars.Escalation += amount;
    }
}
