using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganOld : MonoBehaviour
{
    public Transform center;
    public Transform[] bones;
    public Vector3[] boneOffsets;

    // Start is called before the first frame update

    public float moveSpeed = 10f;
    public float followDistance = .5f;
    public float clampDistance = .5f;

    private Vector3 mousePosition;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(CorrectByRect(Camera.main, Input.mousePosition));
            center.position = Vector2.Lerp(center.position, mousePosition, moveSpeed * Time.deltaTime);       
        }

        for (int i = 0; i < bones.Length; i++)
        {
            Transform t = bones[i];
            Vector2 dir = (center.position - t.transform.position).normalized;
            if (Vector2.Distance(t.position, center.position) > followDistance)
                t.position = Vector2.Lerp(t.position, center.position + boneOffsets[i], moveSpeed * Time.deltaTime);

            if(Vector2.Distance(t.position, center.position + boneOffsets[i]) > clampDistance)
                t.position = Vector2.Lerp(t.position, center.position + boneOffsets[i], moveSpeed * 1.25f * Time.deltaTime);
        }
    }
    public Vector2 CorrectByRect(Camera camera, Vector2 pointer)
    {
        var size = camera.rect.size;
        var offset = camera.pixelRect.position;
        return new Vector2(pointer.x * size.x + offset.x, pointer.y * size.y + offset.y);
    }
}
