using UnityEngine;

public class RotateCounterClockwiseObstacle : MovingObstacle {
    [SerializeField] private float speed = 1f;
    [SerializeField] private float radius = 1f;
    protected override void Move() {
        float moveX = startPosition.x + radius * Mathf.Cos(speed * Time.time);
        float moveZ = startPosition.z + radius * Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(moveX, transform.position.y, moveZ);
    }
}
