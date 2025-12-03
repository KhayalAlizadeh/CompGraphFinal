using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightManager : MonoBehaviour {
    public enum Light {
        None = -1,
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
    public Light PreviousLightState { get; private set; } = Light.None;

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
    
    [SerializeField] private int lightDurationMinInclusive = 2;
    [SerializeField] private int lightDurationMaxExclusive = 6;
    private bool canCheck = false;
    
    private void Start() {
        StartCoroutine(DynamicLight());
    }

    private void FixedUpdate() {
        LightRuleCheck();
    }
    
    private IEnumerator DynamicLight() {
        int duration;

        while (true) {
            canCheck = false;
            yield return new WaitForSeconds(lightStateChangeCheckDelay);
            canCheck = true;
            duration = Random.Range(lightDurationMinInclusive, lightDurationMaxExclusive);
            yield return new WaitForSeconds(duration);
            int index = (int)activeLightState;
            int target = (index + 1) % 3;
            lights[index].TurnOff();
            lights[target].TurnOn();
            PreviousLightState = activeLightState;
            activeLightState = (Light)target;
        }
    }

    private void LightRuleCheck() {
        if (!canCheck) {
            return;
        }

        switch (activeLightState) {
            case Light.None: {
                break;
            }
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
