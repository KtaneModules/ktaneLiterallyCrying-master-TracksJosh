using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eulerScript : MonoBehaviour {

    public KMSelectable Bandage;
    private int multiple = 0;
    private int e = 10;

    // Use this for initialization
    void Start () {
        Bandage.OnInteract += delegate () { PressPlay(); return false; };
        while (e != 0)
        {
            for (int i = 0; i <= 100000; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (i % j == 0)
                    {
                        e--;
                    }
                    if (e == 0)
                    {
                        multiple = i;
                    }
                }
                e = 10;
            }
        }

    }
	
	private void PressPlay()
    {
        Debug.Log(multiple);
    }
}
