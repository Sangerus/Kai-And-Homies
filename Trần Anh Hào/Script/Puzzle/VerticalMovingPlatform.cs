using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    public int direction;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * direction * Time.deltaTime;

        if (Mathf.Abs(transform.position.y - startPosition.y) >= moveDistance)
        {
            direction *= -1; // Đảo chiều di chuyển
        }
    }
}
