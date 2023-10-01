using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private State currentState;
    
    private enum State
    {
        Pause,
        Running,
        GameOver
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.Pause:
                Debug.Log("Pause");
                break;
            case State.Running:
                Debug.Log("Running");
                break;
            case State.GameOver:
                Debug.Log("GameOver");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
