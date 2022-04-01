using UnityEngine;

public class MagicEvent : MonoBehaviour
{
    protected MagicTypes magicType;

    // Start is called before the first frame update
    public virtual void Start()
    {
        MagicSystem magicSystem = GameObject.FindObjectOfType<MagicSystem>();
        magicSystem.TriggerMagic += this.OnTriggerMagic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnTriggerMagic(object sender, MagicEventArgs e)
    {
        if (e.MagicType == this.magicType)
            StartMagicEvent();
        else if (e.MagicType == MagicTypes.RoomSwap)
            StopMagicEvent();
    }

    protected virtual void StartMagicEvent()
    {

    }

    protected virtual void StopMagicEvent()
    {

    }
}
