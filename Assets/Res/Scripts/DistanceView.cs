using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceView : MonoBehaviour
{
    // Drag & Drop the player gameobject using the inspector
    public Transform objectA;

    // Drag & Drop the other object
    public Transform objectB;

    // Drag & Drop the gameobject, child of a Canvas holding a Text component
    public Text UIText;

    // Use LateUpdate to compute the distance **after** the player / object has moved
    private void LateUpdate()
    {
        UIText.text = Vector3.Distance(objectA.position, objectB.position).ToString("f0");
    }

}
