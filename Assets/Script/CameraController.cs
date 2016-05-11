using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int LevelArea = 100;

    private const int ScrollArea = 25;
    private const int ScrollSpeed = 25;

    private const int DragSpeed = 100;

    private const int ZoomSpeed = 25;
    private const int ZoomMin = 15;
    private const int ZoomMax = 100;

    private const int PanSpeed = 50;
    private const int PanAngleMin = 30;
    private const int PanAngleMax = 80;

    public GameObject cameraBoundsObject;
    Transform cameraTransform;
    Bounds bounds;

    Transform transform;
    float rotZoom = 0f;
    void Start()
    {
        transform = gameObject.transform;
        bounds = cameraBoundsObject.GetComponent<Renderer>().bounds;
        cameraTransform = GetComponentsInChildren<Camera>(true)[0].transform;
        //transform.Rotate(transform.right, -30);
    }
    float lastX = 0f;
    float rotation = 0f;
    // Update is called once per frame

    Vector3 getPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height,0f) / 2f);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            hit = hits[i];
            if (hit.transform.gameObject.name.Equals(cameraBoundsObject.name))
            {
                return hit.point;
            }
        }
        return new Vector3();
    }
    void Update()
    {
        var translation = Vector3.zero;
        float rotnow = -(float)Input.mouseScrollDelta.y * 15f;
        rotZoom += rotnow;
        float deltascroll = (float)Input.mouseScrollDelta.y / 5f * ZoomSpeed;
        if (rotZoom > 0 || transform.position.y > 40)
        {
            rotZoom = 0;
            rotnow = 0;
            translation -= Vector3.up * deltascroll;
        }
        else
        {

                translation += transform.forward * deltascroll;
                transform.RotateAround(getPoint(), transform.right, rotnow);
            
            
        }
        if (Input.GetMouseButton(1)) // MMB
        {
            float deltarot = lastX - Input.mousePosition.x;
            rotation += deltarot;
            Vector3 point = getPoint();
            transform.RotateAround(point,Vector3.up, deltarot);
            lastX = Input.mousePosition.x;
        }
        else
        {
            Vector3 center = new Vector3(Screen.width,0, Screen.height) / 2f;
            Vector3 mouse = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
            Vector3 dist = (mouse - center);
            if (dist.magnitude > Mathf.Min(center.x, center.z)/2f)
            {
                Vector3 v = dist * 1f / 10f * Time.deltaTime;
                v = Quaternion.AngleAxis(rotation, Vector3.up) * v;
                translation += v;
            }
        }

        lastX = Input.mousePosition.x;
        transform.position += translation;

        float x = transform.position.x, z = transform.position.z;
        x = Mathf.Clamp(x, bounds.min.x, bounds.max.x);
        z = Mathf.Clamp(z, bounds.min.z, bounds.max.z);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
