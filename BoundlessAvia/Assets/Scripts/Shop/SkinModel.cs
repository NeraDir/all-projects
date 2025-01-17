using UnityEngine;

namespace Game
{
    public class SkinModel : MonoBehaviour
    {
        [SerializeField] private Skin _thisSkin;
        [SerializeField] private GameObject[] _allSkinModels;

        private void Start()
        {
            _thisSkin.GetAndSet();
            if(_thisSkin.isSelected)
            {
                foreach(var model in _allSkinModels) model.gameObject.SetActive(false);
                gameObject.SetActive(true);
            }
        }
    }
}