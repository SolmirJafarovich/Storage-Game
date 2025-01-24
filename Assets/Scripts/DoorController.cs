using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private string _parameterName = "isOpen";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(")");
        // ���������, �������� �� ������ �������
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetBool(_parameterName, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("����� ����������� (������� ���)");
        // ��������� �����, ����� ����� ������
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetBool(_parameterName, true);
        }
    }
}
