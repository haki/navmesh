using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollisions : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    public GameObject speedBoosterIcon;
    public GameObject restartPanel;

    private Vector3 _playerStartPos;
    private Animator _animator;

    private void Start()
    {
        _animator = player.GetComponentInChildren<Animator>();
        _playerStartPos = transform.position;
        speedBoosterIcon.SetActive(false);
        restartPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            playerController.runningSpeed = 0;
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z, Space.Self);
            _animator.SetBool("Win", true);
            restartPanel.SetActive(true);
        }
        if (other.CompareTag("SpeedBoost"))
        {
            playerController.runningSpeed = playerController.runningSpeed + 3f;
            speedBoosterIcon.SetActive(true);
            StartCoroutine(SlowAfterWhileCoroutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = _playerStartPos;
        }
    }

    private IEnumerator SlowAfterWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        if (playerController.runningSpeed != 0)
        {
            playerController.runningSpeed = playerController.runningSpeed - 3f;
        }
        speedBoosterIcon.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}