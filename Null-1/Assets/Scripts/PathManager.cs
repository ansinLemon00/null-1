using UnityEngine;

public class PathManager : MonoBehaviour
{
    // 싱글톤 패턴을 활용해 적들이 어디서든 경로에 접근할 수 있게 합니다.
    public static PathManager Instance { get; private set; }

    [SerializeField] private Transform[] waypoints;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 자식 오브젝트들의 Transform을 자동으로 경로 배열에 수집합니다.
        SetupWaypoints();
    }

    private void SetupWaypoints()
    {
        int childCount = transform.childCount;
        waypoints = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    // 특정 인덱스의 Waypoint 위치를 반환
    public Vector3 GetWaypointPosition(int index)
    {
        if (index >= 0 && index < waypoints.Length)
            return waypoints[index].position;

        return Vector3.zero;
    }

    // 마지막 웨이포인트(성문)인지 확인
    public bool IsLastWaypoint(int index)
    {
        return index >= waypoints.Length;
    }
}