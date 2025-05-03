using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector3 mOffset;
    float coorZ;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        transform.position = GetMPosition() - mOffset;
    }

    private void OnMouseDown()
    {
        if (rb && rb.useGravity)
        {
            rb.useGravity = false;
        }
        coorZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMPosition();
    }

    private void OnMouseUp()
    {
        if(rb && !rb.useGravity)
        {
            rb.useGravity = true;
        }
    }

    private Vector3 GetMPosition()
    {
        Vector3 mouseP = Input.mousePosition;
        mouseP.z = coorZ;
        return Camera.main.ScreenToWorldPoint(mouseP);
    }
}
