using UnityEngine;

public class ForwardBackwardPlatform : MovingObstacle {
    [SerializeField] private float speed;
    [SerializeField] private float range;
    protected override void Move() {
        float moveZ = startPosition.z + (range * Mathf.Sin(speed * Time.time));
        transform.position = new Vector3(transform.position.x, transform.position.y, moveZ);
    }
    
}
