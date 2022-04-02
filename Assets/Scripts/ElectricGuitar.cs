using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGuitar : Interactable
{
    private MagicSystem magicSystem;
    private bool isOnCooldown;
    private float cooldownTimer;

    public float CooldownInterval = 2;

    // Start is called before the first frame update
    void Start()
    {
        magicSystem = FindObjectOfType<MagicSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnCooldown)
        {
            if (cooldownTimer >= CooldownInterval)
            {
                isOnCooldown = false;
                cooldownTimer = 0;
            }

            cooldownTimer += Time.deltaTime;
        }
    }

    public override void Interact(GameObject player)
    {
        base.Interact(player);

        if (!isOnCooldown)
        {
            magicSystem.IncreaseIntensity(5);
            isOnCooldown = true;
        }
    }

    public override string GetInteractionText()
    {
        return isOnCooldown ? "" : "Play guitar";
    }
}
