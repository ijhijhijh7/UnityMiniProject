using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� �������� GameManager�� ��𼭳� ���� �����ϰ� ����
    private bool gameCleared = false; // ���� Ŭ���� ���¸� ����

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

    // ���� Ŭ���� ���·� ��ȯ�ϴ� �Լ�
    public void ClearGame()
    {
        gameCleared = true;
        Debug.Log("Game Cleared! Press Q to restart.");
    }

    // ���� ����� �Լ�
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    }
}
