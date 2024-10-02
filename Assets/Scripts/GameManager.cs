using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴으로 GameManager를 어디서나 접근 가능하게 설정
    private bool gameCleared = false; // 게임 클리어 상태를 관리

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameCleared && Input.GetKeyDown(KeyCode.Q))
        {
            RestartGame();
        }
    }

    // 게임 클리어 상태로 전환하는 함수
    public void ClearGame()
    {
        gameCleared = true;
        Debug.Log("Game Cleared! Press Q to restart.");
    }

    // 게임 재시작 함수
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }
}
