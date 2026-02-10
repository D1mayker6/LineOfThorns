using System;
using Player;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private float _maxXDistance;
        [SerializeField] private float _maxYDistance;
        [SerializeField] private float _minYDistance;
        
        private UnityEngine.Camera _camera;
        private PlayerMovement _player;
        private Vector3 _startPosition;

        
        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _startPosition = gameObject.transform.position;
            _player.OnPlayerDied += RespawnCamera;
        }

        private void FixedUpdate()
        {
            var playerPos = _player.transform.position;
            var cameraPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Debug.Log($"player:{playerPos.y} and camera:{cameraPos.y} and distance: {Mathf.Abs(playerPos.y - cameraPos.y)}");
            var distanceX = Mathf.Abs(cameraPos.x - playerPos.x); 
            if (distanceX >= _maxXDistance)
                _camera.transform.position += Vector3.right * ((_maxXDistance) * Time.deltaTime);
            /*Debug.Log(_camera.transform.position);*/
        }

        private void RespawnCamera()
        {
            _camera.transform.position = _startPosition;
        }
    }
}
