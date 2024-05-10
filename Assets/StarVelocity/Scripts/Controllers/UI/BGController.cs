using UnityEngine;

namespace StarVelocity.Controllers
{
    public class BGController : MonoBehaviour
    {
        [SerializeField] private Material _materialToModify;
        [SerializeField] private Vector2 _offsetAmount;
        [SerializeField] private float _speed;

        void Start()
        {
            if (_materialToModify == null)
            {
                Debug.LogError("Material to modify is not assigned!");
                return;
            }
        }

        void Update()
        {
            if (_materialToModify != null)
            {
                _materialToModify.SetTextureOffset("_MainTex", _offsetAmount);
                _offsetAmount = _offsetAmount + new Vector2(0, _speed);
            }
        }
    }
}