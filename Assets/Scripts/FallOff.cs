using UnityEngine;

public class FallOff : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().GameOver();
        }
    }
}
