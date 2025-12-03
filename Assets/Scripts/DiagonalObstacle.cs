using UnityEngine;

public class DiagonalObstacle : MovingObstacle {
    [SerializeField] private float speed = 1f;
    [SerializeField] private float range = 1f;
    protected override void Move() {
        float moveX = startPosition.x + range * Mathf.Sin(speed * Time.time);
        float moveZ = startPosition.z + range * Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(moveX, transform.position.y, moveZ);
    }
}
