using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾ Ʈ���ſ� ��Ҵ��� Ȯ��
        {
            if (GameManager.instance != null) // GameManager.instance�� null���� Ȯ��
            {
                GameManager.instance.ClearGame(); // ���� Ŭ���� ó��
                collision.GetComponent<PlayerController>().StopMovement(); // �÷��̾��� ������ ����

                // �� ������Ʈ ����
                Destroy(gameObject); // ���� ���� ������Ʈ(�� ������Ʈ) ����
            }
            else
            {
                Debug.LogError("GameManager instance is null!");
            }
        }
    }
}
