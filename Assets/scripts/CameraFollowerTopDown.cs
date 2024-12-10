using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollowerTopDown : MonoBehaviour
{
    public Transform Target;
    public float cameraSpeed;

    public float minX, maxX;
    public float minY, maxY;
    public float offset=0;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        if(Target != null){
            Vector2 newCamPosition = Vector2.Lerp(transform.position, Target.position, Time.deltaTime * cameraSpeed);

            float ClampX = Mathf.Clamp(newCamPosition.x, minX, maxX);
            float ClampY = Mathf.Clamp(newCamPosition.y, minY, maxY);

            transform.position = new Vector3(ClampX, ClampY+offset, -10f);
        }
    }
}
