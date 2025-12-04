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

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.transform.SetParent(transform);
            other.GetComponent<Player>().ApplyGravity = false;
        }
    }
    
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.transform.SetParent(null);
            other.GetComponent<Player>().ApplyGravity = true;
        }
    }
}
