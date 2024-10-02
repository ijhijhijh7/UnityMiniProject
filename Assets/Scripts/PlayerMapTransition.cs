using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapTransition : MonoBehaviour
{

    public Transform goToMap2;
    public Transform startMap2;
    public Transform goToMap3;
    public Transform startMap3;
    public Transform goToMap4;
    public Transform startMap4;

    public CinemachineVirtualCamera map1Camera;
    public CinemachineVirtualCamera map2Camera;
    public CinemachineVirtualCamera map3Camera;
    public CinemachineVirtualCamera map4Camera;

    public float transitionSpeed = 5f; // �̵� �ӵ�
    private bool isTransitioning = false;

    private void Update()
    {
        if (isTransitioning) return;

        // �÷��̾�� goToMap2 ������ �Ÿ� üũ
        if (Vector2.Distance(transform.position, goToMap2.position) < 1f) // �����ߴ��� Ȯ��
        {
            StartMapTransition();
        }
    }

    private void StartMapTransition()
    {
        isTransitioning = true;

        // ī�޶� �켱���� ����
        map2Camera.Priority += 1; // ī�޶� �켱���� ����

        // �÷��̾ startMap2 ��ġ�� �̵�
        StartCoroutine(MovePlayerToStartMap2());
    }

    private IEnumerator MovePlayerToStartMap2()
    {
        while (Vector2.Distance(transform.position, startMap2.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startMap2.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }

        // ���� ��ġ�� ���� �� �̵� �Ϸ�
        transform.position = startMap2.position;
        isTransitioning = false;
    }

}
