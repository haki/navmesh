using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runningSpeed = 8f;
    public float xSpeed = 2f;
    public float limitX = 5f;
    
    private float _touchXDelta = 0;
    private float _newX = 0;

    private void Update()
    {
        if (runningSpeed != 0)
        {
            SwipeCheck();
        }
    }

    private void SwipeCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            _touchXDelta = Input.GetAxis("Mouse X");
        }
        else
        {
            _touchXDelta = 0;
        }

        _newX = transform.position.x + xSpeed * _touchXDelta * Time.deltaTime;
        _newX = Mathf.Clamp(_newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(_newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}