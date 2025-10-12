using UnityEngine;

namespace myProject{
    public class CameraManager : MonoBehaviour{
        private float _zoom = 1f;

        [SerializeField] private float moveSpeedMinZoom;
        [SerializeField] private float moveSpeedMaxZoom;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;


        private CameraData _cameraData;
        private Vector2Int _intCoordinateCamera;
        private Vector3 _cameraPosition;
        private int _height;
        private int _width;

        public void Initialize(CameraData cameraData){
            _zoom = cameraData.zoom;
            _cameraData = cameraData;
            transform.position = _cameraPosition = _cameraData.position;
        }

        private void Update(){
            float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if (zoomDelta != 0f){
                AdjustZoom(zoomDelta);
            }

            float xDelta = Input.GetAxis("Horizontal");
            float yDelta = Input.GetAxis("Vertical");

            if (xDelta != 0 || yDelta != 0){
                AdjustPosition(xDelta, yDelta);
            }
        }

        private void AdjustZoom(float delta){
            _zoom = Mathf.Clamp01(_zoom + delta);
            _cameraData.zoom = _zoom;

            float distance = Mathf.Lerp(minZoom, maxZoom, _zoom);
            _cameraPosition.z = distance;
            _cameraData.position = transform.localPosition = _cameraPosition;
        }

        private void AdjustPosition(float xDelta, float yDelta){
            Vector3 direction = transform.localRotation * new Vector3(xDelta, yDelta, 0f).normalized;
            float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(yDelta));
            float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, _zoom) * damping * Time.deltaTime;

            _cameraPosition = transform.localPosition;
            _cameraPosition += direction * distance;
            transform.localPosition = _cameraPosition;
            _cameraData.position = _cameraPosition;
        }
    }
}