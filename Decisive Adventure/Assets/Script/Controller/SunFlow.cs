using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlow : MonoBehaviour
{
    private Transform target;
    private float playerStartx;
    [SerializeField] GameObject startPosition;
    private float starty;
    private Vector3 _Velocity = Vector3.zero;
    [SerializeField] private Rigidbody2D rg2d;

    enum SunState { SetUp, Iddle};

    private SunState sun;

    IEnumerator Intro()
	{
		yield return new WaitForSeconds(2f);
		sun = SunState.Iddle;
	}

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
        playerStartx = target.position.x;
        starty = startPosition.transform.position.y;
        sun = SunState.SetUp;
        StartCoroutine(Intro());
	}

    void Update(){
        if(sun == SunState.SetUp){
            Vector3 targetVelocity = new Vector2(rg2d.velocity.x, 1f * 10f);
			// And then smoothing it out and applying it to the character
			rg2d.velocity = Vector3.SmoothDamp(rg2d.velocity, targetVelocity, ref _Velocity, 0.05f);
        }
        if(transform.position.y >= starty){
            rg2d.velocity = Vector3.zero;
        }

    }
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(target != null){
            float factor = 0.07f * (target.position.x - playerStartx);
            if(factor > 0){
                factor *= -1;
            }
            Rigidbody2D rb2d = target.GetComponent<Rigidbody2D>();
            float playerVelocity = rb2d.velocity.x;
            if(playerVelocity > 0){
                transform.position = new Vector3((target.position.x - 7f), (starty + factor), transform.position.z);
			//Debug.Log(target.position.x);
            }
            // else if(target.position.x > playerStartx){
            //     transform.position = new Vector3((target.position.x - 7f), transform.position.y, transform.position.z);
            // }
		}
	}
}
