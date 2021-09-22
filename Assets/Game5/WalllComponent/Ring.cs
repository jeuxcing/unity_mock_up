using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{

    public int id;

    public bool blink = false;
    public Color[] colors;
    private Component[] led;
    private int size;

    private int loop=0;

    // Start is called before the first frame update
    void Start()
    {
        led = GetComponentsInChildren<SpriteRenderer>();
        size = led.Length;
        colors = new Color[size];      
    }

    // Update is called once per frame
    void Update()
    {
        loop++;

        if (blink) Blink();
    }

    void SetColors(Color[] c)
    {
        for (var i=0; i<size; i++)
            colors[i] = c[i]; 
        
        for (var i=0; i<size; i++)
            led[i].GetComponent<SpriteRenderer>().color = colors[i];
    }

    void Blink(){
        for (var i=0; i<size; i++)
            colors[i] = new Color(1, ((float)((i+loop)%size)/size), 0, 1); 
        
        for (var i=0; i<size; i++)
            led[i].GetComponent<SpriteRenderer>().color = colors[i];
    }

}
