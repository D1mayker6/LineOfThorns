using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class RoomManager: MonoBehaviour
    {
        public static RoomManager Instance;

        [SerializeField] private List<GameObject> _roomPrefabs;
        
        private float _roomDistance = 30f;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
        public void AddRoom(Vector3 position)
        {
            var roomPos = position + (Vector3.right * _roomDistance);
            var room = Instantiate(_roomPrefabs[0], roomPos, Quaternion.identity);
        }
    }
}