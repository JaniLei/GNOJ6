using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    private float timer;

    public delegate void TriggerMagicEventHandler(object sender, MagicEventArgs e);
    public event EventHandler<MagicEventArgs> TriggerMagic;
    public MagicEventArgs MagicVars = new MagicEventArgs();
    public float MagicInterval = 30;
    public Text EscalationText;
    public Slider IntensitySlider;

    // Start is called before the first frame update
    void Start()
    {

        IncreaseEscalation(1);

        if (EscalationText != null)
            EscalationText.text = $"Escalation: {MagicVars.Escalation}";
        if (IntensitySlider != null)
            IntensitySlider.value = MagicVars.Intensity;

        var lev = new GameObject("levitation");
        lev.AddComponent<LevitationEvent>();
        var rs = new GameObject("room_swap");
        rs.AddComponent<RoomSwapEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (MagicVars.Escalation > 1 && timer >= MagicInterval)
        {
            MagicVars.MagicType = SelectNextMagic();
            OnTriggerMagic(MagicVars);
            timer = 0;
        }
    }

    public void CallSwapRooms()
    {
        MagicVars.MagicType = MagicTypes.RoomSwap;
        OnTriggerMagic(MagicVars);
        timer = 0;
    }

    protected virtual void OnTriggerMagic(MagicEventArgs e)
    {
        TriggerMagic?.Invoke(this, e);
    }

    public MagicTypes SelectNextMagic()
    {
        List<MagicTypes> availableMagic = new List<MagicTypes>();
        if (MagicVars.Escalation >= 2)
            availableMagic.Add(MagicTypes.Levitation);
        if (MagicVars.Escalation >= 3)
            availableMagic.Add(MagicTypes.RoomSwap);
        var nextMagic = availableMagic[UnityEngine.Random.Range(0, availableMagic.Count)];

        //var nextMagic = (MagicTypes)UnityEngine.Random.Range(0, _magicTypesCount);
        return nextMagic;
    }

    public void IncreaseIntensity(float amount)
    {
        MagicVars.Intensity += amount;
        if (IntensitySlider != null)
            IntensitySlider.value = MagicVars.Intensity;
        if (MagicVars.Intensity >= 100)
        {
            //GAME OVER!
            var gameState = FindObjectOfType<GameStateManager>();
            gameState.StartGameOver();
        }
    }

    public void IncreaseEscalation(int amount)
    {
        MagicVars.Escalation += amount;
        if (EscalationText != null)
            EscalationText.text = $"Escalation: {MagicVars.Escalation}";
    }
}
