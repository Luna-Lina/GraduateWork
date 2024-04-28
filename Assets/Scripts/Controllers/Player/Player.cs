using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

namespace StarVelocity
{
    public class Player : MonoBehaviour, IPlayer
    {
        public event Action OnKilled;

        [SerializeField] private float _speed;
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _returnDelay;
        [SerializeField] private AudioSource _missSound;
        [SerializeField] private AudioSource _playerSound;
        [SerializeField] private GameObject _missMenu;

        private Animator _animator;
        private int _rightHash;
        private int _leftHash;
        private Vector3 _initialPosition;
        private bool _isMoving = false;
        private bool _canMove = true;

        private void Start()
        {
            _initialPosition = transform.position;

            _animator = GetComponent<Animator>();
            _rightHash = Animator.StringToHash("Right");
            _leftHash = Animator.StringToHash("Left");
        }

        public void MakeDamage()
        {
            GetComponent<Collider2D>().isTrigger = true;
            OnKilled?.Invoke();
            enabled = false;
            _missMenu.SetActive(true);
            _missSound.Play();
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _initialPosition, _speed * Time.deltaTime);

                if (transform.position == _initialPosition)
                {
                    _isMoving = false;
                    _canMove = true;
                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        if (_canMove)
                        {
                            transform.position += Vector3.left * Mathf.Min(_moveDistance, transform.position.x - (_initialPosition.x - _moveDistance));

                            _animator.SetBool(_leftHash, true);
                            _isMoving = true;
                            _canMove = false;

                            _playerSound.Play();

                            StartCoroutine(ReturnToStartPosition());
                        }
                    }

                    else
                    {
                        if (_canMove)
                        {
                            transform.position += Vector3.right * Mathf.Min(_moveDistance, _initialPosition.x + _moveDistance - transform.position.x);

                            _animator.SetBool(_rightHash, true);
                            _isMoving = true;
                            _canMove = false;

                            _playerSound.Play();

                            StartCoroutine(ReturnToStartPosition());
                        }
                    }
                }
            }
        }

        IEnumerator ReturnToStartPosition()
        {
            yield return new WaitForSeconds(_returnDelay);
            transform.position = _initialPosition;
            _animator.SetBool(_leftHash, false);
            _animator.SetBool(_rightHash, false);
        }
    }
}