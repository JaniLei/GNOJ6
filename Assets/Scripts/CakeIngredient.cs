using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CakeIngredientType
{
    Egg,
    Flour,
    Milk,
    Cherry
}

public class CakeIngredient : Interactable
{
    public CakeIngredientType IngredientType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact(GameObject player)
    {
        base.Interact(player);

        //player -> add to inventory (IngredientType)
        gameObject.SetActive(false);
    }
}
