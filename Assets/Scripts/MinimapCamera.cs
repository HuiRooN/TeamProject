using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y + 30, player.position.z);
    }

    public void MinimapCameraRotaiton(float y)
    {
        Quaternion rotation = Quaternion.Euler(90f, y, 0);

        transform.rotation = rotation;
    }
}
