using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToPosition : MonoBehaviour
{

    [SerializeField] private Vector3 homePosition;
    // Start is called before the first frame update
    public void Start()
    {
        homePosition = gameObject.transform.position;
    }
    public void ResetPosition()
    {
        transform.position = homePosition;
    }
}
