using System.Linq;
using Player;
using UnityEngine;

namespace Triggers
{
    public class TeleportRoom : MonoBehaviour
    {

        private PlayerMovement _player;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerMovement>(out var player))
            {
                _player = player;
                GoToNextRoom();
            }
        }

        private void GoToNextRoom()
        {
            var spawnpoint = FindSpawnpoint();
            _player.transform.position = spawnpoint.transform.position;
            UnityEngine.Camera.main.transform.position += Vector3.right * 30;
        }

        private GameObject FindSpawnpoint()
        {
            var spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
            spawnpoints = spawnpoints
                .OrderByDescending(x => x.transform.position.x)
                .ToArray();
            return spawnpoints[0];
        }
    }
}
