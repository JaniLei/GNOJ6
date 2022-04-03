using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Interactable
{
    private Cake cake;
    private bool isBaking;
    private float bakingTimer;

    public int BakingTimeSeconds = 30;

    public GameObject mixer, spatula, cherry;

    // Start is called before the first frame update
    void Start()
    {
        cake = FindObjectOfType<Cake>();

        InteractionText = "Start baking";
    }

    // Update is called once per frame
    void Update()
    {
        if (isBaking)
        {
            if (bakingTimer >= BakingTimeSeconds)
            {
                StopBaking();
            }

            bakingTimer += Time.deltaTime;
        }
    }

    public override void Interact(GameObject player)
    {
        base.Interact(player);

        if (cake.GetCakeProcess() == CakeProcess.Dough)
            StartBaking();
    }

    private void StartBaking()
    {
        isBaking = true;
        cake.gameObject.transform.position = new Vector3(0f, -200f, 0f);
        // oven light?
    }

    private void StopBaking()
    {
        cake.SetNextState();
        cake.ReturnToOrigin();
        isBaking = false;
        mixer.SetActive(true);
        spatula.SetActive(true);
        cherry.SetActive(true);
    }

    public override string GetInteractionText()
    {
        if (isBaking)
            return $"Baking: {BakingTimeSeconds - (int)bakingTimer}";
        else
            return base.GetInteractionText();
    }
}
