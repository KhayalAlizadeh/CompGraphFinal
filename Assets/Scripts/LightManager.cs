using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightManager : MonoBehaviour {
    public enum Light {
        Green,
        Red,
        Yellow
    }

    private static LightManager instance;
    public static LightManager Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    
    [SerializeField] private Light activeLightState = Light.Green;
    public Light PreviousLightState { get; private set; }

    public Light ActiveLightState {
        get {
            return activeLightState;
        }
        private set {
            activeLightState = value;
        }
    }
    
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
            lights[target].TurnOn();
            PreviousLightState = activeLightState;
            activeLightState = (Light)target;
        }
    }

    private IEnumerator LightRuleCheck() {
        while (true) {

            if (activeLightState != PreviousLightState) {
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
        // Should run
        if (!player.IsRunning) {
            // 
        }
    }

    private void RuleRed() {
        // Must stop
        if (player.IsRunning || player.IsDancing) {
            player.GameOver();
        }
    }

    private void RuleYellow() {
        // Must dance
        if (!player.IsDancing) {
            player.GameOver();
        }
    }

}
