using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [Header("Slots on Shelf")]
    [SerializeField] private Transform[] slots; 

    [Header("Prefabs to Spawn")]
    [SerializeField] private GameObject[] itemPrefabs; 

    private void Start()
    {
        PopulateShelf();
    }

    public void PopulateShelf()
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0) 
            {
                SpawnRandomItem(slot);
            }
        }
    }

    private void SpawnRandomItem(Transform slot)
    {
        
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject randomPrefab = itemPrefabs[randomIndex];

       
        GameObject spawnedItem = Instantiate(randomPrefab, slot.position, Quaternion.identity);

        
        spawnedItem.transform.localScale = Vector3.one;

        
        spawnedItem.transform.parent = slot;
        spawnedItem.name = randomPrefab.name; 
    }

}
