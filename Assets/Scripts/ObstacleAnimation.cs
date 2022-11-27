using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleAnimation : MonoBehaviour
{
    public float speed = 1f;
    public float strenght = 2.5f;
    private float _randomOffset;

    private void Start()
    {
        _randomOffset = Random.Range(-strenght, strenght);
    }

    private void Update()
    {
        var pos = transform.position;
        pos.x = Mathf.Sin(Time.time * speed + _randomOffset) * strenght;
        transform.position = pos;
    }
}