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
        private List<GameObject> _unusedRooms = new List<GameObject>();
        private GameObject _lastSelectedRoom = null;
        [SerializeField] private List<GameObject> _firstroomPrefabs;
        [SerializeField] private List<GameObject> _secondroomPrefabs;
        [SerializeField] private List<GameObject> _thirdroomPrefabs;
        [SerializeField] private List<GameObject> _fourthroomPrefabs;
        [SerializeField] private ScoreCounter  _scoreCounter;
        [SerializeField] private GameObject _firstRoom;
        [SerializeField] private GameManager _gameManager; 
        [SerializeField] private PlayerMovement _playerMovement;
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
                    _unusedRooms.Clear();
                    _lastSelectedRoom = null;
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
                _currentroomPrefabs.AddRange(rooms);
                _unusedRooms.AddRange(rooms);
            
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


        private void AddRoom()
        {
                _currentRoom = Instantiate(RandomizeRoom());
                _gameManager.RecolorRoom(_currentRoom);
        }

        private GameObject RandomizeRoom()
        {
            if (_currentroomPrefabs == null || _currentroomPrefabs.Count == 0)
            {
                Debug.LogError("Список префабов комнат пуст!");
                return null;
            }

            if (_unusedRooms.Count == 0)
            {
                _unusedRooms.AddRange(_currentroomPrefabs);
            }

            int randomIndex = UnityEngine.Random.Range(0, _unusedRooms.Count);
            GameObject selectedRoom = _unusedRooms[randomIndex];
            
            if (selectedRoom == _lastSelectedRoom && _unusedRooms.Count > 1)
            {
                randomIndex = (randomIndex + 1) % _unusedRooms.Count;
                selectedRoom = _unusedRooms[randomIndex];
            }

            _unusedRooms.RemoveAt(randomIndex);
    
            _lastSelectedRoom = selectedRoom;

            return selectedRoom;
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerLevelReached -= GoToNextLevel;
            _scoreCounter.OnDiffReached -= UpdateDiff;
        }
    }
}