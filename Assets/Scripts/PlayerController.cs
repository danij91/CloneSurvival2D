using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Transform characterTransform;


    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private Vector2 _direction = Vector2.left;
    private bool _isRight;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Flip()
    {
        Vector3 scale = characterTransform.localScale;
        scale.x *= -1;
        characterTransform.localScale = scale;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    private void FixedUpdate()
    {
        // 입력
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        if (_movement.sqrMagnitude != 0)
        {
            _direction = _movement.normalized;
        }

        // 이동
        _rigidbody.MovePosition(_rigidbody.position + _movement * moveSpeed * Time.fixedDeltaTime);

        // 애니메이션
        if (_movement.x != 0 || _movement.y != 0)
        {
            animator.SetBool("1_Move", true);

            if (_movement.x > 0 && !_isRight)
            {
                Flip();
                _isRight = true;
            }
            else if (_movement.x < 0 && _isRight)
            {
                Flip();
                _isRight = false;
            }
        }
        else
        {
            animator.SetBool("1_Move", false);
        }
    }
}