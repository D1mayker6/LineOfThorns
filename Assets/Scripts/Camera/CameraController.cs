using System;
using Player;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {

        private float _maxDistance;
        
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
            _maxDistance = _player.TranslationSpeed;
            _player.OnPlayerDied += RespawnCamera;
        }

        private void FixedUpdate()
        {
            var playerPos = _player.transform.position;
            var cameraPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            var distanceX = Mathf.Abs(cameraPos.x - playerPos.x); 
            if (distanceX >= _maxDistance)
                _camera.transform.position += Vector3.right * ((_maxDistance) * Time.deltaTime);
        }

        private void RespawnCamera()
        {
            _camera.transform.position = _startPosition;
        }
    }
}
