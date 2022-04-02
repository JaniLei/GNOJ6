using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CakeIngredientType
{
    Egg,
    Flour,
    Milk,
    Spatula,
    Mixer,
    Cherry
}

public class CakeIngredient : Interactable
{
    public CakeIngredientType IngredientType;

    // Start is called before the first frame update
    void Start()
    {
        InteractionText = "Pick up";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(GameObject player)
    {
        base.Interact(player);

        //player -> add to inventory (IngredientType)
        player.GetComponent<PlayerInventory>().CollectedIngredients.Add(this.IngredientType);
        gameObject.SetActive(false);
    }
}
