using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public List<string> requiredItems; // Ahora es una lista
    public Transform door;
    public Vector3 openRotation;
    public float openSpeed = 2f;

    private bool isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        ObjetosRecolectables inventory = other.GetComponent<ObjetosRecolectables>();
        if (inventory && inventory.HasAllItems(requiredItems) && !isOpen)
        {
            StartCoroutine(OpenDoor());
        }
    }

    System.Collections.IEnumerator OpenDoor()
    {
        isOpen = true;
        Quaternion startRot = door.rotation;
        Quaternion endRot = Quaternion.Euler(openRotation);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            door.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}
