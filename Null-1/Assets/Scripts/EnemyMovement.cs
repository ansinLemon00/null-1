using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("이동 속도 (하운드는 1.5배 등으로 조정 가능)")]
    [SerializeField] private float moveSpeed = 2f;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        // 첫 번째 웨이포인트 위치로 적을 강제 순간이동 시키며 시작
        Vector3 startPos = PathManager.Instance.GetWaypointPosition(currentWaypointIndex);
        transform.position = startPos;

        // 다음 목적지 지정을 위해 인덱스 1 증가
        currentWaypointIndex++;
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        // 현재 목표로 하는 웨이포인트의 위치 가져오기
        Vector3 targetPosition = PathManager.Instance.GetWaypointPosition(currentWaypointIndex);

        // targetPosition 방향으로 한 프레임만큼 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 목표점에 거의 도달했는지 체크 (오차 범위 0.05f)
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            currentWaypointIndex++;

            // 만약 마지막 성문(종착점)에 도달했다면?
            if (PathManager.Instance.IsLastWaypoint(currentWaypointIndex))
            {
                OnReachGate();
            }
        }
    }

    private void OnReachGate()
    {
        // TODO: 기지(성문) HP 깎는 로직 연동
        Debug.Log("적 성문 도달! 기지 체력이 감소합니다.");

        // 오브젝트 파괴 (추후 3단계에서 Object Pool로 변경 예정) ->> 필수*****
        Destroy(gameObject); 
    }

    // 속도 변경 메소드 (얼음술사의 둔화 스킬 구현용)
    public void ChangeSpeed(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}