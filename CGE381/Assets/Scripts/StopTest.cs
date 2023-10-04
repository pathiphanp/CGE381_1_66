using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTest : MonoBehaviour
{
    [SerializeField] float a;
    [SerializeField] float[] stoptime;
    [SerializeField] int index;


    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        a += Time.fixedDeltaTime;
        if (a >= stoptime[index])
        {
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            Time.timeScale = 1;
        }
    }
}
