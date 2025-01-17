using UnityEngine;

namespace Game
{
    public class NLO : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Joystick _joystick;
        private Vector3 _moveDirection = Vector3.zero;
        internal bool isCatchingCow = false;

        [SerializeField] private StartEndGame _game; 

        [SerializeField] private Vector3 _grap;

        private CharacterController _controller;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(isCatchingCow) return;

            Move();
            CheckCow();
        }

        private void Move()
        {
            float horizontal = _joystick.Horizontal;
            float vertical = _joystick.Vertical;

            _moveDirection = new Vector3(horizontal, 0, vertical);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= _speed;

            _controller.Move(_moveDirection * Time.deltaTime);
        }

        private void CheckCow()
        {
            var colls = Physics.OverlapBox(transform.position, _grap);
            foreach(var col in colls)
            {
                if(col.TryGetComponent<Cow>(out Cow cow))
                {
                    _game.IsCorrectCow(cow.skinMeshRenderer.sharedMesh);
                    isCatchingCow = true;
                    _rigidbody.velocity = Vector3.zero;
                    cow.Die();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.CompareTag("Windmill")) _game.End();
    }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, _grap);
        }
    }
}