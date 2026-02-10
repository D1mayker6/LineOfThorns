using Player;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {

        private UnityEngine.Camera _camera;

        private PlayerMovement _player;

        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDistance;

        private void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void FixedUpdate()
        {
            var playerPos = _player.transform.position;
            var cameraPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Debug.Log($"player:{playerPos.x} and camera:{cameraPos.x} and distance: {Mathf.Abs(playerPos.x - cameraPos.x)}");
            if (Mathf.Abs(playerPos.x - cameraPos.x) >= _maxDistance || Mathf.Abs(playerPos.x - cameraPos.x) < _minDistance)
                _camera.transform.position += Vector3.right * ((_maxDistance) * Time.deltaTime);
            Debug.Log(_camera.transform.position);
        }
    }
}
