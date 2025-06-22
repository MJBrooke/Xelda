using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }
    
    private State state;
    private EnemyPathfinding _pathfinding;

    private void Awake()
    {
        _pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            _pathfinding.SetDirection(GetRoamingPosition());
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition() => new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
}
