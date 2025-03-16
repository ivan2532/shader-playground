using UnityEngine;

public class PreviewRotator : MonoBehaviour
{
    [SerializeField] private Transform previewTransform;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private float zoomSpeed = 25f;
    [SerializeField] private float minFov = 5f;
    [SerializeField] private float maxFov = 60f;

    private Vector3 _currentRotationVelocity;
    private Vector3 _smoothedRotationVelocity;
    private Vector3 _smoothDampVelocity;
    
    private bool _isDragging;
    private bool _isPanning;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleInput();
        ApplyRotation();
        HandleZoom();
        HandlePanning();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1)) previewTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        
        _isDragging = Input.GetMouseButton(0);
        _isPanning = Input.GetMouseButton(2);
        _currentRotationVelocity = _isDragging ? Input.mousePositionDelta * rotationSpeed : Vector3.zero;
    }

    private void ApplyRotation()
    {
        _smoothedRotationVelocity = Vector3.SmoothDamp(
            _smoothedRotationVelocity, 
            _currentRotationVelocity,
            ref _smoothDampVelocity, 
            rotationSmoothTime);
        
        previewTransform.Rotate(Vector3.up, -_smoothedRotationVelocity.x * Time.deltaTime, Space.World);
        previewTransform.Rotate(Vector3.right, _smoothedRotationVelocity.y * Time.deltaTime, Space.World);
    }

    private void HandleZoom()
    {
        var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - scrollDelta * zoomSpeed, minFov, maxFov);
    }

    private void HandlePanning()
    {
        if (!_isPanning) return;
        
        var distanceToModel = previewTransform.position.z - _camera.transform.position.z;
        var cameraToModelVector = Vector3.forward * distanceToModel;
        var offset = _camera.ScreenToWorldPoint(cameraToModelVector) -
                       _camera.ScreenToWorldPoint(Input.mousePositionDelta + cameraToModelVector);
        
        _camera.transform.Translate(offset, Space.World);
    }
}
