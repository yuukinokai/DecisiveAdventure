using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFlow : MonoBehaviour
{
    private Transform target;
    private float startx;
    private float starty;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
        startx = target.position.x;
        starty = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(target != null){
            float factor = 0.1f * (target.position.x - startx);
            if(factor < 0){
                factor *= -1;
            }
			transform.position = new Vector3((target.position.x + 7f), (starty + factor), transform.position.z);
			//Debug.Log(target.position.x);
		}
	}
}
