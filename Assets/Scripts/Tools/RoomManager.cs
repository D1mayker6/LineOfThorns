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
        [SerializeField] private ScoreCounter  _scoreCounter;
        [SerializeField] private GameObject _firstRoom;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _blockcolor;
        private Stack<GameObject> _roomsHistory = new();
        private Vector3 _roomBasePosition;
        private GameObject _currentRoom;
        
        
        private List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>(64);

        private void Start()
        {
            _currentRoom = _firstRoom;
            RecolorRoom(_currentRoom);
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


        private void AddRoom()
        {
            _currentRoom = Instantiate(RandomizeRoom());
            RecolorRoom(_currentRoom);
        }

        private void RecolorRoom(GameObject room)
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = _backgroundColor;
            _spriteRenderers.Clear();
            _spriteRenderers.AddRange(_currentRoom.GetComponentsInChildren<SpriteRenderer>());
            foreach (var spriteRenderer in _spriteRenderers)
                    spriteRenderer.color = _blockcolor;
            
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