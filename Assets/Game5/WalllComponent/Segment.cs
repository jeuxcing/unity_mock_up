using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{

    public int[] coordinates = new int[2];
    public bool blink = false;
    public Color[] colors;
    private Component[] led;
    private int size;

    private int loop=0;

    public enum Orientation
    {
        horizontal,
        vertical,
        ring
    }
    public Orientation orientation = Orientation.horizontal;

    // Start is called before the first frame update
    void Start()
    {
        led = GetComponentsInChildren<SpriteRenderer>();
        size = led.Length;
        colors = new Color[size];      
        for (var i=0; i<size; i++)
            colors[i] = new Color(1, 1, 1, 1); 
    }

    // Update is called once per frame
    void Update()
    {
        loop++;

        if (blink) Blink();

        for (var i=0; i<size; i++)
            led[i].GetComponent<SpriteRenderer>().color = colors[i];
    }

    public void SetColors(Color[] c, int start=0, int stop=24)
    {
        if (stop>size) stop = size;

        for (var i=start; i<stop; i++)
            colors[i] = c[i]; 
    }

    public void SetColor(Color c)
    {
        for (var i=0; i<size; i++)
            colors[i] = c; 
    }

    public void SetSingleColor(int index, Color c)
    {
        colors[index] = c; 
    }


    void Blink(){
        for (var i=0; i<size; i++)
            colors[i] = new Color(1, ((float)((i+loop)%size)/size), 0, 1); 
    }

}