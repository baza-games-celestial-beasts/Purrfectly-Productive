using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers.Game_States
{
    public class GameStateManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float failDelay;
        [SerializeField] private float victoryDelay;
        
        [SerializeField, ReadOnly] private GameState currentState;

        public GameState CurrentState => currentState;
        
        public event Action Menu;
        public event Action Prepare;
        public event Action StartGame;
        public event Action Victory;
        public event Action Fail;
        public event Action Finish;
        #endregion
    
        #region Monobehaviour Callbacks
        private void Start()
        {
            ChangeState(GameState.Menu);
        }
        #endregion
        
        public void ChangeState(GameState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case GameState.Menu:
                    Menu?.Invoke();
                    break;
                case GameState.Prepare:
                    Prepare?.Invoke();
                    break;
                case GameState.Game:
                    StartGame?.Invoke();
                    break;
                case GameState.Finish:
                    Finish?.Invoke();
                    break;
                case GameState.Victory:
                    Victory?.Invoke();
                    break;
                case GameState.Fail:
                    Fail?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
