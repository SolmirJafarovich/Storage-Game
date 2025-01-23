using UnityEngine;

public class TruckLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ����� �� ������ ��� "Box" ��� ������ �������������
        if (other.CompareTag("Pickup"))
        {
            // ���������� ���������
            Destroy(other.gameObject);

            // �������������: ������ �������� ���� ���� ��� ����
            Debug.Log("��������� �������� � ����������!");
        }
    }
}
