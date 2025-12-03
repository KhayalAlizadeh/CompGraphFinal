using UnityEngine;

public class OpenPanel : MonoBehaviour {
    [SerializeField] private GameObject panel;

    public void ShowPanel() {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    public void HidePanel() {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
