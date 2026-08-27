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
        [SerializeField] private List<GameObject> _currentroomPrefabs;
        [SerializeField] private List<GameObject> _firstroomPrefabs;
        [SerializeField] private List<GameObject> _secondroomPrefabs;
        [SerializeField] private List<GameObject> _thirdroomPrefabs;
        [SerializeField] private List<GameObject> _fourthroomPrefabs;
        [SerializeField] private ScoreCounter  _scoreCounter;
        [SerializeField] private GameObject _firstRoom;
        [SerializeField] private GameManager _gameManager; 
        [SerializeField] private PlayerMovement _playerMovement;
        private Stack<GameObject> _roomsHistory = new();
        private Vector3 _roomBasePosition;
        private GameObject _currentRoom;

        private void OnEnable()
        {
            _playerMovement.OnPlayerLevelReached += GoToNextLevel;
            _scoreCounter.OnDiffReached += UpdateDiff;
        }

        private void Start()
        {
            UpdateDiff(0);
            AddFirstRoom();
        }

        private void UpdateDiff(int diff)
        {
            switch (diff)
            {
                case 0:
                    _currentroomPrefabs.Clear();
                    PushingRooms(_firstroomPrefabs);
                    break;
                case 1:
                    PushingRooms(_secondroomPrefabs);
                    break;
                case 2:
                    PushingRooms(_thirdroomPrefabs);
                    break;
                case 3:
                    PushingRooms(_fourthroomPrefabs);
                    break;
                default:
                    break;
            }
        }

        private void PushingRooms(List<GameObject> rooms)
        {
            foreach (var room in rooms)
            {
                _currentroomPrefabs.Add(room);
            }
        }



        public void GoToNextLevel()
        {
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


        private void AddRoom(GameObject room = null)
        {
                _currentRoom = Instantiate(RandomizeRoom());
                _gameManager.RecolorRoom(_currentRoom);
        }

        private GameObject RandomizeRoom()
        {
            if (_roomsHistory.Count >= _currentroomPrefabs.Count)
                _roomsHistory.Clear();

            var availableRooms = _currentroomPrefabs.Where(r => !_roomsHistory.Contains(r)).ToList();

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