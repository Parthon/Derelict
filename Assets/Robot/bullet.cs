using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour 
{

	public float speed = 16;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * speed;
	}
}
