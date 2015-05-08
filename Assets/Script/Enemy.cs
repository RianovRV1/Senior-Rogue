using UnityEngine;
using System.Collections;
/// <summary>
/// Contains AI logic for the Enemy to be able to chase and see player, uses constant ray casts to search for the layer the player is on.
/// if the player leaves the ray cast, the enemy will return to its start location which is set on start
/// also contains logic to hit the player.
/// 
/// </summary>
public class Enemy : MonoBehaviour {

	public float speed; // speed of enemy, can be changed in the inspector
	 // location of the player, can be set in inspector
    public Player player { get; private set; }
    public bool moving;
    public Vector3 startLocation;
    private Vector2
        _lastPosition,
        _velocity;
    void Start()
    {
        startLocation = transform.position;
        player = FindObjectOfType<Player>();
        moving = false;
    }
    

    public void LateUpdate()
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
    }
    void FixedUpdate()
	{
        var raycast = Physics2D.Raycast(transform.position, new Vector2(0, 1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast1 = Physics2D.Raycast(transform.position, new Vector2(0, -1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast2 = Physics2D.Raycast(transform.position, new Vector2(1, 0), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast3 = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast4 = Physics2D.Raycast(transform.position, new Vector2(1, -1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast5 = Physics2D.Raycast(transform.position, new Vector2(1, 1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast6 = Physics2D.Raycast(transform.position, new Vector2(-1, 1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast7 = Physics2D.Raycast(transform.position, new Vector2(-1, -1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast8 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, -1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast9 = Physics2D.Raycast(transform.position, new Vector2(0.5f, -1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast10 = Physics2D.Raycast(transform.position, new Vector2(-1, 0.5f), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast11 = Physics2D.Raycast(transform.position, new Vector2(-1, -0.5f), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast12 = Physics2D.Raycast(transform.position, new Vector2(0.5f, 1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast13 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 1), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast14 = Physics2D.Raycast(transform.position, new Vector2(1, 0.5f), 35, 1 << LayerMask.NameToLayer("Player"));
        var raycast15 = Physics2D.Raycast(transform.position, new Vector2(1, -0.5f), 35, 1 << LayerMask.NameToLayer("Player"));
        if (raycast || raycast2 || raycast3 || raycast4 || raycast5 || raycast6 || raycast7 || raycast8 || raycast9 || raycast10 || raycast11 || raycast12 || raycast13 || raycast14 || raycast1 || raycast15)
            moving = true;
        else
            moving = false;
        if (moving)
            {
                float z = Mathf.Atan2((player.transform.position.y - transform.position.y),
                                       (player.transform.position.x - transform.position.x))
                    * Mathf.Rad2Deg - 90; // set a float z based on its transform and the players transform, set to degrees from radians

                transform.eulerAngles = new Vector3(0, 0, z); // rotate enemy based on Z result

                GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed); // move enemy towards player
            }
            else if (transform.position != startLocation)
            {
                float z = Mathf.Atan2((startLocation.y - transform.position.y),
                                   (startLocation.x - transform.position.x))
                * Mathf.Rad2Deg - 90; // set a float z based on its transform and the players transform, set to degrees from radians

                transform.eulerAngles = new Vector3(0, 0, z); // rotate enemy based on Z result

                GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 0);
                transform.rotation = rot;
                
            }
            if (transform.position.x <= (startLocation.x + 0.01f) && transform.position.x >= (startLocation.x - 0.01f) && transform.position.y >= (startLocation.y - 0.01f) && transform.position.y <= (startLocation.y + 0.01f))
                transform.position = startLocation;

        
        
        
	}
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            RoomManager.Instance.Player.TakeDamage(5, gameObject);
            var totalVelocity = other.rigidbody.velocity + _velocity;
            other.rigidbody.AddForce(new Vector2(-1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * 6, 1000, 4500), -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 6, 1000, 4500)));
        }
      
        
    }
}
