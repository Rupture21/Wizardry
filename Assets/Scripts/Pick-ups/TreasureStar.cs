using UnityEngine;

public class TreasureStar : MonoBehaviour
{
    InventoryManager inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OpenTreasureStar();
            Destroy(gameObject);
        }
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
