using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour
{

    [Range(0, 100)]
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        value = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pouet()
    {
     //GetComponent<Ring>().color = new Color(0.5f,0.5f,1.0f); 
    }
}
