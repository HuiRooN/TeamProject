using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform cameraRig;
    [SerializeField] Vector3 offset;
    [SerializeField] float dist = 5f;

    [SerializeField] float xSpeed = 90f;
    [SerializeField] float ySpeed = 70f;

    [SerializeField] float yMinLimit = -1f;
    [SerializeField] float yMaxLimit = 45f;

    private float x;
    private float y;


    // Start is called before the first frame update
    void Start()
    {
        //Vector3 angles = transform.eulerAngles;

        x = target.rotation.x;
        y = target.rotation.y;

        //x = angles.x;
        //y = angles.y;
    }


    // Update is called once per frame
    private void Update()
    {
        if(target)
        {
            if(dist >=5f)
            {
                dist = 5f;
            }

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0f, 0f, -dist) + target.position + new Vector3(0f, 0f, 0f);
            position.y += 1.5f;

            transform.rotation = rotation;
            transform.position = position;

        }

        RaycastHit hit;
        Vector3 rayDir = (transform.position - cameraRig.position).normalized;
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Player");
        if (Physics.Raycast(cameraRig.position, rayDir, out hit, 5f, ~layerMask))
        {
            dist = Vector3.Distance(cameraRig.position, hit.point) - 2f;
        }
        else
            dist = 5f;

    }

    float ClampAngle(float anlge, float min, float max)
    {
        if (anlge < -360f)
        {
            anlge += 360f;
        }
        if (anlge > 360f)
        {
            anlge -= 360f;
        }

        return Mathf.Clamp(anlge, min, max);


    }
}
