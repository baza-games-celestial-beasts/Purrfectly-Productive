using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";

        private Rigidbody2D _rigidbody2D;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }
        #endregion


        private void Move()
        {
            var horizontalMove = Input.GetAxis(horizontalAxis);
            var verticalMove = Input.GetAxis(verticalAxis);
            
            _rigidbody2D.velocity = new Vector2(horizontalMove, verticalMove) * speed;
        }
    }
}
