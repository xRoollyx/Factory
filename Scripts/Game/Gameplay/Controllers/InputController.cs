using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace myProject{
    public class InputController : MonoBehaviour{
        public static Action<Vector2Int> OnMouseChangePosition;
        public static Action<Vector2Int> OnLeftClick;
        public static Action<Vector2Int> OnRightClick;
        
       private Vector3 _worldPosition;
        private Camera _camera;
        private Plane _plane;
        private RaycastHit _hit;

        private Vector2Int _currentMousePosition;
        private Vector2Int _startDragPosition;
        private Vector2Int _endDragPosition;

        private List<Vector2Int> _dragCoordinates;

        private void Start(){
            _camera = Camera.main;
            _plane = new Plane(Vector3.forward, Vector3.zero);
            _dragCoordinates = new List<Vector2Int>();
            
        }

        public void Initialize(){
            _currentMousePosition = GetMousePosition();
            OnMouseChangePosition?.Invoke(_currentMousePosition);
        }

        private void Update(){
            if (_currentMousePosition != GetMousePosition()){
                _currentMousePosition = GetMousePosition();
                OnMouseChangePosition?.Invoke(_currentMousePosition);
            }

            if (EventSystem.current.IsPointerOverGameObject()){
                return;
            }
            
            if (Input.GetMouseButtonDown(0)){
                OnLeftClick?.Invoke(_currentMousePosition);
            }
            
            if (Input.GetMouseButtonDown(1)){
                OnRightClick?.Invoke(_currentMousePosition);
            }
            
            if (Input.GetKeyDown(KeyCode.G)){
                Bank.AddGold(this, 10);
            }
            
        }
        

        private Vector2Int GetMousePosition(){
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector2Int mousePosition = new Vector2Int();
            if (_plane.Raycast(ray, out float position)){
                Vector3 worldPosition = ray.GetPoint(position);

                mousePosition.x = Mathf.RoundToInt(worldPosition.x);
                mousePosition.y = Mathf.RoundToInt(worldPosition.y);
            }
            return mousePosition;
        }
    }
}

