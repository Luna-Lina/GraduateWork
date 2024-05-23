using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using StarVelocity.Data;

namespace StarVelocity.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent (typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour, IPlayer
    {
        public event Action OnPlayerDisabled;
        public event Action<int> OnCurrentScore;
        public event Action OnKilled;

        private PlayerControllerSettings _settings;
        [SerializeField] private AudioSource _missSound;
        [SerializeField] private AudioSource _playerSound;
        [SerializeField] private LosingPanel _missMenu;

        private Animator _animator;
        private int _rightHash;
        private int _leftHash;
        private Vector3 _initialPosition;
        private bool _isMoving = false;
        private bool _canMove = true;

        private int _score;

        private void Start()
        {
            InitPlayer();
            _initialPosition = transform.position;
            _animator = GetComponent<Animator>();
            _rightHash = Animator.StringToHash("Right");
            _leftHash = Animator.StringToHash("Left");
        }
        void InitPlayer()
        {
            _settings = Resources.Load<PlayerControllerSettings>("PlayerControllerSettings");
        }

        public void IncreaseSpeed(float amount)
        {
            _settings.Speed += amount;
        }

        public void MakeDamage()
        {
            GetComponent<Collider2D>().isTrigger = true;
            OnKilled?.Invoke();
            enabled = false;
            _missMenu.gameObject.SetActive(true);
            _missSound.Play();
        }

        private void Update()
        {

            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _initialPosition, _settings.Speed * Time.deltaTime);

                if (transform.position == _initialPosition)
                {
                    _isMoving = false;
                    _canMove = true;
                }
            }
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) || EventSystem.current.currentSelectedGameObject != null)
                {
                    return;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        if (_canMove)
                        {
                            Move(Vector3.left);
                        }
                    }
                    else
                    {
                        if (_canMove)
                        {
                            Move(Vector3.right);
                        }
                    }
                }
            }
        }

        private void Move(Vector3 Derection)
        {
            if (Derection == Vector3.left)
            {
                transform.position += Vector3.left * Mathf.Min(_settings.MoveDistance, transform.position.x - (_initialPosition.x - _settings.MoveDistance));
                _animator.SetBool(_leftHash, true);
            }
            else if (Derection == Vector3.right)
            {
                
                transform.position += Vector3.right * Mathf.Min(_settings.MoveDistance, _initialPosition.x + _settings.MoveDistance - transform.position.x);
                _animator.SetBool(_rightHash, true);
            }
            _isMoving = true;
            _canMove = false;

            _playerSound.Play();

            StartCoroutine(ReturnToStartPosition());
        }

        IEnumerator ReturnToStartPosition()
        {
            yield return new WaitForSeconds(_settings.ReturnDelay);
            transform.position = _initialPosition;
            _animator.SetBool(_leftHash, false);
            _animator.SetBool(_rightHash, false);
        }

        public void CurrentScore()
        {
            _score++;
            OnCurrentScore?.Invoke(_score);
        }

        private void OnDisable()
        {
            FirebaseWrapper.SaveData(PlayerPrefs.GetString("User"), _score.ToString());
            OnPlayerDisabled?.Invoke();
        }
    }
}