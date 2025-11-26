using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : MonoBehaviour {
    enum Light {
        Green,
        Red,
        Yellow
    }
    [SerializeField] private Light activeLightState = Light.Green;
    private void Start() {
        StartCoroutine(DynamicLight());
    }

    private IEnumerator DynamicLight() {
        int duration;

        while (true) {
            duration = Random.Range(2, 6);
            int index = (int)activeLightState;
            int target = (index + 1) % 3;
            activeLightState = (Light)target;

            yield return new WaitForSeconds(duration);
        }
    }

}
