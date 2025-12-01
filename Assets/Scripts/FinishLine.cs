using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    [SerializeField] private float sceneLoadDelay = 0.5f;
    [SerializeField] private TextMeshProUGUI win;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(Win());
        }
    }
    
    private IEnumerator Win() {
        win.gameObject.SetActive(true);
        win.SetText("You Win!");
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(1);
    }
}
