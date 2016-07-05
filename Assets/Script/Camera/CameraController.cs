using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int ZoomSpeed = 25;

    public GameObject cameraBoundsObject;
    Transform cameraTransform;
    Bounds bounds;
    float distanceToFocusPoint = 60f;
    float scrollSpeed = 560f;

    float rotZoom = 0f;
    void Start()
    {
        bounds = cameraBoundsObject.GetComponent<Renderer>().bounds;
        cameraTransform = GetComponentsInChildren<Camera>(true)[0].transform;
    }
    float lastMousePositionX = 0f;
    float rotation = 0f;
    Vector3 CameraFocusPoint()
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
    Vector3 FocusPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            hit = hits[i];
            // if (hit.transform.gameObject.name.Equals(cameraBoundsObject.name))
            {
                return hit.point;
            }
        }
        return new Vector3();
    }
    Vector3 heightTranslation = Vector3.zero;
    Vector3 lastTranslation = Vector3.zero;

    float f(float y){
        if (y == 0) return y;
        return Mathf.Sign(y)*Mathf.Sqrt(Mathf.Abs(y)) * 10f; ;
    }
    float scrolloffset = 0;
    float lasttranslationy = 0;
    Vector3 focused = Vector3.zero;
    bool isfocused = false;
    void Update()
    {
        var translation = Vector3.zero;
        float rotnow = -(float)Input.mouseScrollDelta.y * 15f;
        rotZoom += rotnow;
        float deltascroll = (float)Input.mouseScrollDelta.y / 5f * ZoomSpeed;
        deltascroll *= scrollSpeed;
        scrolloffset -= deltascroll;

        float step = scrollSpeed * 10000000f * Time.deltaTime * scrolloffset == 0 ? 0 : scrolloffset / Mathf.Abs(scrolloffset);
        Debug.Log("step" + step);
        scrolloffset -= step;
        translation.y += step;


        Vector3 forward = transform.forward;
        forward.y = 0f;// = Quaternion.AngleAxis(-transform.rotation.eulerAngles.z, transform.right) * forward;
        forward = forward.normalized;

        Vector3 zero = transform.position - heightTranslation;
        zero.y = CameraFocusPoint().y;
        transform.LookAt(zero + forward * distanceToFocusPoint);

        if (Input.GetMouseButton(1)) // MMB
        {
            if (!isfocused)
            {
                isfocused = true;
                focused = FocusPoint();
            }
            float deltarot = lastMousePositionX - Input.mousePosition.x;
            rotation += deltarot;
            transform.RotateAround(focused, Vector3.up, deltarot);
        }
        else
        {
            isfocused = false;
            if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height && Input.mousePosition.x >= 0 && Input.mousePosition.y >= 0) { 
                Vector3 center = new Vector3(Screen.width,0, Screen.height) / 2f;
                Vector3 mouse = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                Vector3 dist = (mouse - center);
                dist.x = dist.x / center.x * center.z;
                float minpix = Mathf.Min(center.x, center.z);
                float area = 0.8f;
                float strength = Mathf.Clamp(((dist.magnitude / minpix) - area) * (1f / (1f - area)), 0, 1);
                Vector3 v = dist.normalized * 44f * Time.deltaTime * strength;
                v = Quaternion.AngleAxis(rotation, Vector3.up) * v;
                translation += v;
            }
        }
        lastMousePositionX = Input.mousePosition.x;
        transform.position += translation;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(transform.position.y, 4,400),
            Mathf.Clamp(transform.position.z, bounds.min.z, bounds.max.z));
    }
}
