using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : Interactable
{
    enum CakeProcess
    {
        Empty,
        Dough,
        Baked,
        Filled,
        Frosted,
        Finished
    }

    private CakeProcess currentProcess;

    public GameObject[] CakeObjects = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
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

        if (currentProcess != CakeProcess.Finished)
        {
            //if (currentProcess == CakeProcess.Dough)
            //  bake process?
            if (PlayerHasIngredient(player))
                SetNextState();
        }
    }

    private bool PlayerHasIngredient(GameObject player)
    {
        //WiP
        return true;
    }

    private void SetNextState()
    {
        CakeObjects[(int)currentProcess].SetActive(false);
        currentProcess++;
        CakeObjects[(int)currentProcess].SetActive(true);
    }
}
