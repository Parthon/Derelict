using UnityEngine;
using System.Collections;

public class StationGenerator : MonoBehaviour {

	public GameObject[] pieces;
	
	public GameObject deadend;
	public GameObject bigfourway;
	public GameObject corridor;
	public int squareradius = 4;
	public float offset = 16;
	
	public GameObject player;
	public GameObject robot;
	
	public float robotchance = 5;
	
	
	//public float corridorchance = 0.6f;

	// Use this for initialization
	void Start () 
	{
		if ( squareradius < 4 ) squareradius = 4;
		for ( int z = 0; z <= squareradius; z ++ )
		{
			for ( int x = 0; x <= squareradius; x ++ )
			{
				Vector3 v = new Vector3(x*offset, 0, z*offset);
				if ( ( Mathf.Abs(x) >= squareradius || Mathf.Abs(z) >= squareradius ) )
				{
					GameObject.Instantiate(deadend, new Vector3( v.x,0, v.z), Quaternion.identity);
					if ( x != 0 ) GameObject.Instantiate(deadend, new Vector3(-v.x,0, v.z), Quaternion.identity);
					if ( z != 0 ) GameObject.Instantiate(deadend, new Vector3( v.x,0,-v.z), Quaternion.identity);
					if ( x != 0 && z != 0 ) GameObject.Instantiate(deadend, new Vector3(-v.x,0,-v.z), Quaternion.identity);
				}
				else if ( Mathf.Abs(x) <= 1 && Mathf.Abs(z) <= 1 )
				{
					if ( x == 0 && z == 0 )
						GameObject.Instantiate(bigfourway, v, Quaternion.identity);
				}
				else 
				{
					if ( x == 0 || z == 0 )
					{
						GameObject.Instantiate(corridor, v, Quaternion.identity);
						GameObject.Instantiate(corridor, -v, Quaternion.identity);
					}
					else 
					{
						int r = (int)(Random.value * pieces.Length);
						GameObject.Instantiate(pieces[r], new Vector3( v.x,0, v.z), Quaternion.identity);
						GameObject.Instantiate(pieces[r], new Vector3(-v.x,0, v.z), Quaternion.identity);
						GameObject.Instantiate(pieces[r], new Vector3( v.x,0,-v.z), Quaternion.identity);
						GameObject.Instantiate(pieces[r], new Vector3(-v.x,0,-v.z), Quaternion.identity);
						
						if ( Random.value * 100 < robotchance )
							GameObject.Instantiate(robot, new Vector3( v.x,0, v.z), Quaternion.identity);
						if ( Random.value * 100 < robotchance )
							GameObject.Instantiate(robot, new Vector3(-v.x,0, v.z), Quaternion.identity);
						if ( Random.value * 100 < robotchance )
							GameObject.Instantiate(robot, new Vector3( v.x,0,-v.z), Quaternion.identity);
						if ( Random.value * 100 < robotchance )
							GameObject.Instantiate(robot, new Vector3(-v.x,0,-v.z), Quaternion.identity);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 p = player.transform.position;
		p.y -= 0.5f;
		Vector3 d = -p;
		d.y = 0.0f;
		d = d.normalized * 0.25f;
		LineRenderer lr = (LineRenderer)renderer;
		lr.SetPosition(0,p - d);
		lr.SetPosition(1,p + (d * 3));
	}
}
