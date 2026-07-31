using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Tools
{
    public class RoomManager: MonoBehaviour
    {
        [SerializeField] private List<GameObject> _roomPrefabs;
        private Stack<GameObject> _roomsHistory = new();
        private Vector3 _roomBasePosition;
        private int _roomValue = 100;
        [SerializeField] private ScoreCounter  _scoreCounter;

        public void GoToNextLevel()
        {
            AddRoom();
            AddScoreForRoom();
        }

        private void AddScoreForRoom()
        {
            _scoreCounter.AddScore(_roomValue);
        }


        private void AddRoom()
        {
            var room = Instantiate(RandomizeRoom());
        }

        private GameObject RandomizeRoom()
        {
            if (_roomsHistory.Count >= _roomPrefabs.Count)
                _roomsHistory.Clear();


            var availableRooms = _roomPrefabs.Where(r => !_roomsHistory.Contains(r)).ToList();

            int randomIndex = UnityEngine.Random.Range(0, availableRooms.Count);
            GameObject selectedRoom = availableRooms[randomIndex];

            _roomsHistory.Push(selectedRoom);

            return selectedRoom;
        }
        
    }
}