using UnityEngine;

public class TreasureStar : Pickup
{
    InventoryManager inventory;

    protected override void Start()
    {
        base.Start();

        inventory = FindAnyObjectByType<InventoryManager>();
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         OpenTreasureStar();
    //         Destroy(gameObject);
    //     }
    // }

    protected override void OnDestroy()
    {
        if (!target) return; 

        OpenTreasureStar();

        base.OnDestroy(); 
    }

    public void OpenTreasureStar()
    {
        if (inventory.GetPossibleEvolutions().Count <= 0)
        {
            Debug.LogWarning("No available evolutions");
            return;
        }
        
        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)];
        inventory.EvolveWeapon(toEvolve);
    }
}
