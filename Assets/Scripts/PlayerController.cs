using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public AnimatorController animatorController;
    public Animator animator;
    public Transform characterTransform;


    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private bool _isRight;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        // 입력 처리
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        // 이동 처리
        _rigidbody.MovePosition(_rigidbody.position + _movement * moveSpeed * Time.fixedDeltaTime);

        // 애니메이션 처리
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