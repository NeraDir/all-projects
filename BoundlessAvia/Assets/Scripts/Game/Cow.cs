using UnityEngine;

namespace Game
{
    public class Cow : MonoBehaviour
    {
        private Animator _animator;
        internal SkinnedMeshRenderer skinMeshRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void Die() => _animator.SetTrigger("die");

        public void Destroy()
        {
            FindObjectOfType<NLO>().isCatchingCow = false;
            Destroy(gameObject);
        }
    }
}