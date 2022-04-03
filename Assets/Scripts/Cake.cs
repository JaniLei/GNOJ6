using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CakeProcess
{
    Empty,
    Dough,
    Baked,
    Filled,
    Frosted,
    Finished
}

public class Cake : Interactable
{
    private CakeProcess currentProcess;
    private Vector3 originPos;
    private MagicSystem magicSystem;

    public GameObject[] CakeObjects = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        magicSystem = FindObjectOfType<MagicSystem>();

        foreach (var cake in CakeObjects)
        {
            cake.SetActive(false);
        }
        CakeObjects[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(GameObject player)
    {
        base.Interact(player);

        if (currentProcess != CakeProcess.Finished && currentProcess != CakeProcess.Dough)
        {
            if (PlayerHasIngredient(player))
                SetNextState();
        }
    }

    public CakeProcess GetCakeProcess()
    {
        return currentProcess;
    }

    private bool PlayerHasIngredient(GameObject player)
    {
        var inv = player.GetComponent<PlayerInventory>();

        switch (currentProcess)
        {
            case CakeProcess.Empty:
                if (inv.CollectedIngredients.Contains(CakeIngredientType.Egg) &&
                    inv.CollectedIngredients.Contains(CakeIngredientType.Flour) &&
                    inv.CollectedIngredients.Contains(CakeIngredientType.Milk))
                {
                    return true;
                }
                break;
            case CakeProcess.Baked:
                if (inv.CollectedIngredients.Contains(CakeIngredientType.Mixer))
                    return true;
                break;
            case CakeProcess.Filled:
                if (inv.CollectedIngredients.Contains(CakeIngredientType.Spatula))
                    return true;
                break;
            default:
                break;
        }
        return false;
    }

    public void SetNextState()
    {
        CakeObjects[(int)currentProcess].SetActive(false);
        currentProcess++;
        CakeObjects[(int)currentProcess].SetActive(true);

        if (currentProcess == CakeProcess.Dough ||
            currentProcess == CakeProcess.Baked ||
            currentProcess == CakeProcess.Filled)
            magicSystem.IncreaseEscalation(1);

        if (currentProcess == CakeProcess.Finished)
        {
            var gameState = FindObjectOfType<GameStateManager>();
            gameState.StartVictory();
        }
    }

    public void ReturnToOrigin()
    {
        gameObject.transform.position = originPos;
    }

    public override string GetInteractionText()
    {
        switch (currentProcess)
        {
            case CakeProcess.Empty:
                return "Find the dough ingredients";
            case CakeProcess.Dough:
                return "Bake in oven";
            case CakeProcess.Baked:
                return "Find filling tool";
            case CakeProcess.Filled:
                return "Find frosting tool";
            default:
                return base.GetInteractionText();
        }
    }
}
