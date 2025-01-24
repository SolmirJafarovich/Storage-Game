using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [Header("Slots on Shelf")]
    [SerializeField] private Transform[] slots; // Слоты для предметов

    [Header("Prefabs to Spawn")]
    [SerializeField] private GameObject[] itemPrefabs; // Префабы предметов

    private void Start()
    {
        PopulateShelf();
    }

    public void PopulateShelf()
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0) // Проверяем, что слот пустой
            {
                SpawnRandomItem(slot);
            }
        }
    }

    private void SpawnRandomItem(Transform slot)
    {
        // Выбираем случайный префаб
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject randomPrefab = itemPrefabs[randomIndex];

        // Создаём объект
        GameObject spawnedItem = Instantiate(randomPrefab, slot.position, Quaternion.identity);

        // Сбрасываем масштаб
        spawnedItem.transform.localScale = Vector3.one;

        // Привязываем к слоту
        spawnedItem.transform.parent = slot;
        spawnedItem.name = randomPrefab.name; // Назначаем имя для удобства
    }

}
