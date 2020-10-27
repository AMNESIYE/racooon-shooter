using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.Translate(new Vector3(0, 1, 0) * _speed * Time.deltaTime);

	    if (transform.position.y >= 5.5f)
	    {
		    if (this.transform.parent != null)
		    {
			    Destroy(this.transform.parent.gameObject);
		    }
		    
	        Destroy(this.gameObject);
	    }
	}
}
