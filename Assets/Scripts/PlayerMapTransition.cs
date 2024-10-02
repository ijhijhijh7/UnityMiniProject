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

    public float transitionSpeed = 5f; // 이동 속도
    private bool isTransitioning = false;

    private void Update()
    {
        if (isTransitioning) return;

        // 플레이어와 goToMap2 사이의 거리 체크
        if (Vector2.Distance(transform.position, goToMap2.position) < 1f) // 도달했는지 확인
        {
            StartMapTransition();
        }
    }

    private void StartMapTransition()
    {
        isTransitioning = true;

        // 카메라 우선순위 조정
        map2Camera.Priority += 1; // 카메라 우선순위 증가

        // 플레이어를 startMap2 위치로 이동
        StartCoroutine(MovePlayerToStartMap2());
    }

    private IEnumerator MovePlayerToStartMap2()
    {
        while (Vector2.Distance(transform.position, startMap2.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startMap2.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }

        // 최종 위치에 도달 후 이동 완료
        transform.position = startMap2.position;
        isTransitioning = false;
    }

}
