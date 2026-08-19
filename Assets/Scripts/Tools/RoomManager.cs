using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Player;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Tools
{
    public class RoomManager: MonoBehaviour
    {
        [SerializeField] private List<GameObject> _roomPrefabs;
        [SerializeField] private ScoreCounter  _scoreCounter;
        [SerializeField] private GameObject _firstRoom;
        [SerializeField] private GameManager _gameManager; 
        [SerializeField] private PlayerMovement _playerMovement;
        private Stack<GameObject> _roomsHistory = new();
        private Vector3 _roomBasePosition;
        private GameObject _currentRoom;
        
        private void Start()
        {
            AddFirstRoom();
            _playerMovement.OnPlayerLevelReached += GoToNextLevel;
        }



        public void GoToNextLevel()
        {
            Debug.Log("Go to next level");
            AddRoom();
            AddScoreForRoom();
        }

        private void AddScoreForRoom()
        {
            _scoreCounter.AddScore();
        }

        private void AddFirstRoom()
        {
            var room = Instantiate(_firstRoom);
            _gameManager.RecolorRoom(room);
        }


        private void AddRoom()
        {
            _currentRoom = Instantiate(RandomizeRoom());
            _gameManager.RecolorRoom(_currentRoom);
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

        private void OnDisable()
        {
            _playerMovement.OnPlayerLevelReached -= GoToNextLevel;
        }
    }
}