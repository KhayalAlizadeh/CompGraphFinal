using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<ColoredLight> lights; // Green Red Yellow

    private void Start() {
        StartCoroutine(DynamicLight());
    }

    private IEnumerator DynamicLight() {
        int duration;

        while (true) {
            duration = Random.Range(2, 6);
            int index = (int)activeLightState;
            int target = (index + 1) % 3;
            lights[index].TurnOff();
            lights[target].TurnOn();
            activeLightState = (Light)target;

            yield return new WaitForSeconds(duration);
        }
    }

}
