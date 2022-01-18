using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float _maxCameraSize;
    [SerializeField]
    private float _minCameraSize;

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentMagnitude = (touch1.position - touch2.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            ZoomCamera(difference * 0.01f);
        }

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0)
        {
            ZoomCamera(mw * 10f);
        }
        else if (mw < 0)
        {
            ZoomCamera(mw * 10f);
        }
    }

    private void ZoomCamera(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, _minCameraSize, _maxCameraSize);
    }
}
