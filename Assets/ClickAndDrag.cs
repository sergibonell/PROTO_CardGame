using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private GameObject selected;
    private IDraggable interf;
    Vector3 offset;
    Vector3 mousePosition;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            grab();
        else if (Input.GetMouseButtonUp(0) && selected)
            release();
        if (selected)
            hold();
    }

    void grab()
    {
        Collider2D target = Physics2D.OverlapPoint(mousePosition);
        if (target != null && target.gameObject.TryGetComponent<IDraggable>(out interf))
        {
            interf.OnGrab();
            selected = target.transform.gameObject;
            offset = selected.transform.position - mousePosition;
        }
    }

    void hold()
    {
        selected.transform.position = mousePosition + offset;
    }

    void release()
    {
        interf.OnRelease();
        interf = null;
        selected = null;
    }
}
