using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class KittenController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private float valueOfCooldown = 5;

    private Rigidbody2D _rigidbody2D;
    private bool _isMove;
    private Vector2 _movingVector;
    private readonly Cooldown _cooldown = new Cooldown();

    private static readonly int IsMove = Animator.StringToHash("is-move");

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cooldown.ValueOfCooldown = valueOfCooldown;
    }

    private void Update()
    {
        if (_cooldown.IsReady)
        {
            ChooseMovingVector();
            _cooldown.Reset();
        }

        Existence();
    }

    #endregion

    private void Existence()
    {
        _isMove = !(_movingVector.x == 0 && _movingVector.y == 0);
        
        if (_isMove)
        {
            transform.localScale = new Vector3(_movingVector.x > 0 ? 1 : -1, 1, 1);
        }

        _rigidbody2D.velocity = new Vector2(_movingVector.x, _movingVector.y) * speed;
        animator.SetBool(IsMove, _isMove);
    }

    private void ChooseMovingVector()
    {
        float randHorizontal = new Random().Next(-1001, 1001)/1000.0f;
        float randVertical = new Random().Next(-1001, 1001)/1000.0f;

        if (randHorizontal <= 0.3 && randHorizontal >= -0.3)
        {
            randHorizontal = 0;
        }
        
        if (randVertical <= 0.3 && randVertical >= -0.3)
        {
            randVertical = 0;
        }

        _movingVector = new Vector2(randHorizontal, randVertical);
    }

    public void ChangeDirection()
    {
        _movingVector = new Vector2(-_movingVector.x, -_movingVector.y);
    }
}