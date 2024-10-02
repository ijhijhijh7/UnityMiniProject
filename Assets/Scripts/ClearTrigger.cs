using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어가 트리거에 닿았는지 확인
        {
            if (GameManager.instance != null) // GameManager.instance가 null인지 확인
            {
                GameManager.instance.ClearGame(); // 게임 클리어 처리
                collision.GetComponent<PlayerController>().StopMovement(); // 플레이어의 움직임 멈춤

                // 골 오브젝트 삭제
                Destroy(gameObject); // 현재 게임 오브젝트(골 오브젝트) 삭제
            }
            else
            {
                Debug.LogError("GameManager instance is null!");
            }
        }
    }
}
