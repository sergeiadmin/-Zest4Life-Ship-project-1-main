using UnityEngine;

public class PlayerMovement_Control_anim : MonoBehaviour
{
    public GameObject objectToShow;  // ������, ������� ����� ��������/������
    public bool isWalking;  // ����, �����������, �������� �� �����

    private Animator anim;  // ��������� ���������
    private Vector3 lastPosition;  // ��������� ������� ������

    void Start()
    {
        anim = GetComponent<Animator>();  // �������� ��������� ��������� � ������
        lastPosition = transform.position;  // ���������� ������� ������� ������
    }

    void Update()
    {
        // ���������, ��������� �� �����
        if (transform.position != lastPosition)
        {
            isWalking = true;
            anim.SetBool("Walk", true);  // �������� �������� ������

            if (objectToShow != null)
            {
                // ���� ������ �����, ���������� ���
                objectToShow.SetActive(true);
            }
        }
        else
        {
            isWalking = false;
            anim.SetBool("Walk", false);  // �������� �������� �������

            if (objectToShow != null)
            {
                // ���� ������ �����, �������� ���
                objectToShow.SetActive(false);
            }
        }

        lastPosition = transform.position;  // ���������� ������� ������� ������ ��� ��������� � ��������� �����
    }
}
