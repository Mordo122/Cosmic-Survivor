using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
     public enum GameStates{
        Gameplay,
        Paused,
        GameOver
     }

     public GameStates currentState;

     void Update(){
        TestSwitchState();
        switch (currentState){
            case GameStates.Gameplay:
            break;

            case GameStates.Paused:
            break;

            case GameStates.GameOver:
            break;

            default:
            Debug.LogWarning("STATE DOES NOT EXIST");
            break;
        }
     }

     void TestSwitchState(){
        if(Input.GetKeyDown(KeyCode.E)){
            currentState++;
        }else if (Input.GetKeyDown(KeyCode.Q)){
            currentState--;
        }
     }
}
