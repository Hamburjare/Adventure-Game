using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Pyörittää GameObjectia
/// </summary>
public class RotateObject : MonoBehaviour
{
    [SerializeField] private int rotateSpeed;
    [SerializeField] private Vector3 rotateAxis;
    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * rotateAxis,
        Space.World);
    }
}