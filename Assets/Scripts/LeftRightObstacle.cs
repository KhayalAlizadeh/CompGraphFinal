using Unity.VisualScripting;
using UnityEngine;

public class LeftRightObstacle : MovingObstacle  {
    [SerializeField] private float speed = 1f;
    [SerializeField] private float range = 1f;
    
    protected override void Move() {
        float moveX = startPosition.x + range * Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
    }
}
