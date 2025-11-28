using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {
    enum Light {
        Green,
        Red,
        Yellow
    }
    [SerializeField] private Light activeLightState = Light.Green;
    private Light previousLightState;
    [SerializeField] private List<ColoredLight> lights; // Green Red Yellow

    [SerializeField] private Player player;

    [SerializeField] private float lightStateChangeCheckDelay = 0.5f;
    private void Start() {
        StartCoroutine(DynamicLight());
        StartCoroutine(LightRuleCheck());
    }

    private IEnumerator DynamicLight() {
        int duration;

        while (true) {
            duration = Random.Range(2, 6);
            yield return new WaitForSeconds(duration);
            int index = (int)activeLightState;
            int target = (index + 1) % 3;
            lights[index].TurnOff();
            //Debug.Log($"index {index}, target: {target}");
            lights[target].TurnOn();
            previousLightState = activeLightState;
            activeLightState = (Light)target;
        }
    }

    private IEnumerator LightRuleCheck() {
        while (true) {

            if (activeLightState != previousLightState) {
                // State changed
                yield return new WaitForSeconds(lightStateChangeCheckDelay);
            }
            
            switch (activeLightState) {
                case Light.Green: {
                    RuleGreen();
                    break;
                }
                case Light.Red: {
                    RuleRed();
                    break;
                }
                case Light.Yellow: {
                    RuleYellow();
                    break;
                }
                default: {
                    break;
                }
            }
        }
    }

    private void RuleGreen() {
        // Must run
        if (!player.IsRunning) {
            // lose
        }
    }

    private void RuleRed() {
        // Must stop
        if (player.IsRunning || player.IsDancing) {
            // lose
        }
    }

    private void RuleYellow() {
        // Must dance
        if (!player.IsDancing) {
            // lose
        }
    }

}
