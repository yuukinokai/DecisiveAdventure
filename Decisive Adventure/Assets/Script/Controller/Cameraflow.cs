using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraflow : MonoBehaviour {

	[SerializeField] private float minX = 0f;
	[SerializeField] private float minY = 0f;
	[SerializeField] private float maxX = 0f;
	[SerializeField] private float maxY = 0f;

	private Transform target;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(target != null){
			transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY), transform.position.z);
			//Debug.Log(target.position.x);
		}
	}
}