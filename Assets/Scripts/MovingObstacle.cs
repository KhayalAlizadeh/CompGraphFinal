using System;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {
    protected Vector3 startPosition;
    void Start() {
        startPosition = transform.position;
    }
    
    void FixedUpdate() {
        Move();
    }

    protected virtual void Move() {
        return;
    }
}
