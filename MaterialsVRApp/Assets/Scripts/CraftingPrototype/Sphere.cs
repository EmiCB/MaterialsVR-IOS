using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    public bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Toggle();
        }
    }

    void Toggle() {
        if (toggle) {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
        } else {
            this.gameObject.transform.position = new Vector3(-15, 0, 0);
        }
        toggle = !toggle;
    }
}
