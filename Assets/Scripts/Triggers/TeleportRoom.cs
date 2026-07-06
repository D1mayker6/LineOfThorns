using System;
using System.Linq;
using Player;
using Tools;
using UnityEngine;

namespace Triggers
{
    public class TeleportRoom : MonoBehaviour
    {

        private PlayerMovement _player;
        public GameObject[] spawnPoints;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerMovement>(out var player))
            {
                _player = player;
                GoToNextRoom();
                DeletePreviousRoom();
            }
        }

        private void GoToNextRoom()
        {
            var spawnpoint = FindSpawnpoint();
            _player.transform.position = spawnpoint.transform.position;
        }

        private void DeletePreviousRoom()
        {
            var pr = transform.parent.gameObject;
            Destroy(pr);
        }

        private GameObject FindSpawnpoint()
        {
            var spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
            spawnpoints = spawnpoints
                .OrderByDescending(x => x.transform.position.x)
                .ToArray();
            spawnPoints = spawnpoints;
            return spawnpoints[0];
        }
    }
}
