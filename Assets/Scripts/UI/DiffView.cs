using UnityEngine;

namespace UI
{
    public class DiffView : MonoBehaviour
    {

        [SerializeField] private GameObject _speedView;
        [SerializeField] private GameObject _directionView;
        [SerializeField] private GameObject _gravityView;

        public void Initialize(int diff)
        {
            switch (diff)
            {
                case 1:
                    _speedView.SetActive(true);
                    break;
                case 2:
                    _directionView.SetActive(true);
                    break;
                case 3:
                    _gravityView.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void OnAnimEnded()
        {
            Destroy(gameObject);
        }
    }
}
