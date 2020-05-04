using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentRecol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PropTransform pt;
    void Start()
    {
        pt = this.GetComponent<PropTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pt.actualPrefab.transform.position;
    }
}
