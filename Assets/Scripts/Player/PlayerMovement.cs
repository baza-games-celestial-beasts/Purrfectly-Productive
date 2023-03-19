using System;
using Managers.Game_States;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";
        [SerializeField] private PlayerAnimator playerAnimator;
        
        private Rigidbody2D _rigidbody2D;

        private PlayerMoveState moveState;
        private float ladderY;
        public Ladder targetLadder;

        [Inject] private GameStateManager _gameStateManager;
        #endregion

        #region Monobehaviour Callbacks

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _gameStateManager.Finish += DisableMotion;
            SetMoveState(PlayerMoveState.Walk);
        }

        private void Update()
        {
            if (moveState == PlayerMoveState.Walk) {
                Move();
            } else if(moveState== PlayerMoveState.LadderClimb) {
                Climb();
            }                       
        }
        #endregion

        public void SetMoveState(PlayerMoveState _moveState) {
            moveState = _moveState;

            if(moveState == PlayerMoveState.LadderClimb) {
                ladderY = 0f;
                _rigidbody2D.isKinematic = true;
                playerAnimator.SetIsClimbState(true);
                playerAnimator.SetIsMoveState(false);                
            } else {
                playerAnimator.SetIsClimbState(false);
                playerAnimator.SetIsMoveState(true);
                playerAnimator.SetClimbSpeed(1.0f);
                _rigidbody2D.isKinematic = false;
            }
        }

        private void Climb() {
            var verticalMove = Input.GetAxisRaw(verticalAxis);

            ladderY = Mathf.Clamp01(ladderY + verticalMove * Time.deltaTime);
            
            _rigidbody2D.transform.position = targetLadder.GetPlayerPosition(ladderY);

            playerAnimator.SetClimbSpeed(Mathf.RoundToInt(Mathf.Abs(verticalMove)));

            if(ladderY >= 0.9f) {
                if (Game.inst.inventory.TryTakeItem(ItemType.Patch)) {
                    Game.inst.actionPopup.Draw(transform.position + Vector3.up * 1.0f, "[E]\nВставить заплатку");

                    if (Input.GetKeyDown(KeyCode.E)) {
                        Game.inst.inventory.TakeItem(ItemType.Patch);

                        Debug.Log("DO PATCH");
                    }                    
                } else {
                    Game.inst.actionPopup.Draw(transform.position + Vector3.up * 1.0f, "[E]\nНужна заплатка");
                }   
            } else {
                Game.inst.actionPopup.Draw(transform.position + Vector3.up * 1.0f, "Вверх! [W]\nСлезть [E]");

                if(Input.GetKeyDown(KeyCode.E)) {
                    targetLadder.Interact();
                }
            }            
        }

        private void Move()
        {
            var horizontalMove = Input.GetAxis(horizontalAxis);
            var verticalMove = Input.GetAxis(verticalAxis);

            var isMove = !(horizontalMove == 0 && verticalMove == 0);
            playerAnimator.SetIsMoveState(isMove);
            if (isMove)
            {
                playerAnimator.transform.localScale = new Vector3(horizontalMove > 0 ? 1 : -1, 1, 1);
            }

            _rigidbody2D.velocity = new Vector2(horizontalMove, verticalMove) * speed;
        }
        
        private void DisableMotion()
        {
            SetMoveState(PlayerMoveState.None);
            
            _rigidbody2D.velocity = Vector2.zero;
            playerAnimator.SetIsMoveState(false);
        }
    }

    public enum PlayerMoveState {
        None,
        Walk,
        LadderClimb
    }

}