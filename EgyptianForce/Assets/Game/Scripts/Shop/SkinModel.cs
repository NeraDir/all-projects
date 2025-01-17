using UnityEngine;

namespace Game
{
    public class SkinModel : MonoBehaviour
    {
        [SerializeField] private Skin _thisSkin;
        [SerializeField] private GameObject[] _allSkinModels;
        private ThirdPersonController _parent;

        private void Start()
        {
            _parent = GetComponentInParent<ThirdPersonController>();

            _thisSkin.GetAndSet();
            if(_thisSkin.isSelected)
            {
                foreach(var model in _allSkinModels) model.gameObject.SetActive(false);
                gameObject.SetActive(true);
            }
        }

        public void AnimAttackEvent() => _parent.Attack();
    }
}