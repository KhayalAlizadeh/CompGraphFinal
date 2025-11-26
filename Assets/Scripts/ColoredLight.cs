using UnityEngine;

public class ColoredLight : MonoBehaviour {

    private Material material;

    private void Start() {
        material = GetComponent<MeshRenderer>().material;
    }

    public void TurnOn() {
        material.EnableKeyword("_EMISSION");
    }

    public void TurnOff() {
        material.DisableKeyword("_EMISSION");
    }
}
