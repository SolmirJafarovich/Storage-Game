using UnityEngine;

public class TruckLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли объект тэг "Box" или другой идентификатор
        if (other.CompareTag("Pickup"))
        {
            // Уничтожаем коробочку
            Destroy(other.gameObject);

            // Дополнительно: можете добавить сюда очки или звук
            Debug.Log("Коробочка закинута в грузовичок!");
        }
    }
}
