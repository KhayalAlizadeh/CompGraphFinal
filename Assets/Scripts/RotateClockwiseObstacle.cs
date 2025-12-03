using UnityEngine;

public class RotateClockwiseObstacle : MovingObstacle {
    [SerializeField] private float speed = 1f;
    [SerializeField] private float radius = 1f;
    protected override void Move() {
        float moveX = startPosition.x + radius * Mathf.Sin(speed * Time.time);
        float moveZ = startPosition.z + radius * Mathf.Cos(speed * Time.time);
        transform.position = new Vector3(moveX, transform.position.y, moveZ);
    }
}
