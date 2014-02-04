private var motor : CharacterMotor;
private var direction : Quaternion;
private var timeout : float;
private var firetimeout : float;
private var dir : float;
private var player : GameObject;
public var bullet : GameObject;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
	timeout = 0;
	firetimeout = 0;
	turning = 0;
	player = GameObject.Find("Player");
}

// Update is called once per frame
function Update () 
{
	var d = player.transform.position - transform.position;
	d.y = 0;
	d.Normalize();
	var dist = d.magnitude;
	
	if ( dist > 48 ) return;
	
	var p = 0;
	timeout -= Time.deltaTime;
	firetimeout -= Time.deltaTime;
	
	if ( dist <= 16 && Vector3.Dot(transform.rotation*Vector3.forward,d) > 0.5f )
	{
		var hit : RaycastHit;
		Physics.Raycast(transform.position, player.transform.position - transform.position, hit, 16, ~(1<<8));
		if ( hit != null && hit.collider != null && hit.collider.gameObject == player )
		{
			direction = Quaternion.LookRotation(d);
			p = 1;
			if ( firetimeout < 0 && Vector3.Dot(transform.forward,d) > 0.99f )
			{
				GameObject.Instantiate(bullet,transform.position + transform.forward,direction);
				firetimeout = 0.5f;
			}
		}
	}
	if ( p == 0 && timeout < 0 ) 
	{
		timeout = Random.value*2.0f + 2.0f;
		dir += 90 + Random.value * 180.0f;
		if ( dir > 360.0f ) dir -= 360.0f;
		direction = Quaternion.Euler(0,dir,0);
	}
	transform.rotation = Quaternion.RotateTowards(transform.rotation,direction,Time.deltaTime*90.0f);
	// Apply the direction to the CharacterMotor
	if ( p == 0 ) motor.inputMoveDirection = transform.rotation * new Vector3(0, 0, 0.5);
	else motor.inputMoveDirection = new Vector3(0, 0, 0);
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
