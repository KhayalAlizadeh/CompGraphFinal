using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class NPC1 : MonoBehaviour {
    [SerializeField] private float accuracy; //%
    [SerializeField] private float speed = 10f;

    private Animator animator;
    private LightManager lightManager;
    private float accuracyRoll;
    private bool willMakeMistake = false;

    private int runParameterID = 0;
    private int danceParameterID = 1;

    private enum Action {
        None,
        MoveForward,
        Stop,
        Dance
    }
    private Action[] actions = { Action.MoveForward, Action.Stop, Action.Dance };
    void Start() {
        animator = GetComponent<Animator>();
        runParameterID = animator.parameters[runParameterID].nameHash;
        danceParameterID = animator.parameters[danceParameterID].nameHash;
        lightManager = LightManager.Instance;
    }

    void Update() {
        if (lightManager.ActiveLightState != lightManager.PreviousLightState) {
            accuracyRoll = UnityEngine.Random.Range(0f, 100f);
            willMakeMistake = accuracyRoll >= accuracy;
            //Debug.Log("Light change detected by npc"); // This should run once flag it bro
            DecideAction();
        }
    }

    private void DecideAction() {
        Action correctAction;
        switch (LightManager.Instance.ActiveLightState) {
            case LightManager.Light.Green: {
                correctAction = Action.MoveForward;
                break;
            }
            case LightManager.Light.Red: {
                correctAction = Action.Stop;
                break;
            }
            case LightManager.Light.Yellow: {
                correctAction = Action.Dance;
                break;
            }
            default: {
                correctAction = Action.None;
                break;
            }
        }

        if (willMakeMistake) {
            MakeMistake(correctAction);
        }
        else {
            Act(correctAction);
        }
    }

    private void MakeMistake(Action correctAction) {

        if (correctAction == Action.MoveForward) {
            return;
        }
        
        List<Action> wrongActions = new List<Action>();

        foreach (Action action in actions) {
            if (action != correctAction) {
                wrongActions.Add(action);
            }
        }
        
        Act(wrongActions[UnityEngine.Random.Range(0, wrongActions.Count)]);
        
        Die();
    }

    private void Act(Action action) {
        
        if (action == Action.None) {
            return;
        }
        
        switch (action) {
            case Action.MoveForward: {
                MoveForward();
                return;
            }
            case Action.Stop: {
                Stop();
                return;
            }
            case Action.Dance: {
                Dance();
                return;
            }
            default: break;
        }
    }
    
    private void MoveForward() {
        animator.SetBool(danceParameterID, false);
        animator.SetBool(runParameterID, true);
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void Stop() {
        animator.SetBool(runParameterID, false);
        animator.SetBool(danceParameterID, false);
    }

    private void Dance() {
        animator.SetBool(runParameterID, false);
        animator.SetBool(danceParameterID, true);
    }

    private void Die() {
        Destroy(gameObject);
    }
}
