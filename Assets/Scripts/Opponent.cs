using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public GameObject target;
    public GameObject speedBoosterIcon;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _opponentStartPos;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _opponentStartPos = transform.position;
        speedBoosterIcon.SetActive(false);
    }

    private void Update()
    {
        _agent.SetDestination(target.transform.position);

        if ((int)_agent.remainingDistance == (int)_agent.stoppingDistance)
        {
            _animator.SetBool("Win", true);
            transform.Rotate(transform.position.x, 180, transform.position.z, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = _opponentStartPos;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("SpeedBoost"))
        {
            _agent.speed = _agent.speed + 3f;
            speedBoosterIcon.SetActive(true);
            StartCoroutine(SlowAfterWhileCoroutine());
        }
    }

    private IEnumerator SlowAfterWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        _agent.speed = _agent.speed - 3f;
        speedBoosterIcon.SetActive(false);
    }
}
