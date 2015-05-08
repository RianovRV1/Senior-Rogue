using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage 
{
	public float speed;
    public bool dead;
    public Transform sword;
    public int MaxHealth = 10;
    bool paused = false;
    public int Health { get; private set; }
	Animator anim;
    /// <summary>
    /// Has start function the grabs an animator for players animations
    /// Update function contains listening for pausing
    /// Fixed Update contains all the player logic for moving and calls the attack function
    /// the kill method merely sets the players death bool
    /// the take damage method applys damage to the players current health
    /// attack function calls an animations causing an attack
    /// </summary>
	void Start() //gets animator componant and sets max health
	{
		anim = GetComponent<Animator> ();
        dead = false;
        Health = MaxHealth;
        
	}
    void Update() //pause function listens for a key press every frame
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Paused();
        }
        if (Input.anyKey == false)
        {

            anim.SetTrigger("Stopped");
        }

    }
	void FixedUpdate() //every other player function which listens for a call every set time, so if time scale is set to 0, those calls arent listened to
	{
        if (!dead)
        {

            
            if (Input.GetKeyDown(KeyCode.Space))// call attack animation on space
            {
                Attack();
            }
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0); // W A S D movement set up
            GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * move.x * Time.deltaTime, speed * move.y * Time.deltaTime));
            //transform.position += move * speed * Time.deltaTime; // actual physics being applied 
            if (Input.GetKey(KeyCode.D)) // setting character to facing right
            {
                
                    anim.SetTrigger("WalkingRight");
                    
                
                
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 90);
                sword.transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.A))//setting character to facing left
            {
                
                    anim.SetTrigger("WalkingLeft");
               
                
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, -90);
                sword.transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.W))//setting character to facing up
            {
                anim.SetTrigger("WalkingUp");
                

                var rot = Quaternion.identity;
               rot.eulerAngles = new Vector3(0, 0, 180);
                sword.transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.S)) //setting character to facing down
            {
                anim.SetTrigger("WalkingDown");
                
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 0);
                sword.transform.rotation = rot;
            }
           
            if(Input.GetKey(KeyCode.K))
            {
                RoomManager.Instance.KillPlayer();
            }
        }
	}
    public void Paused() //pause function
    {
        if (!paused)
        {
            
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void  Kill()// sets players dead bool, plays death animation which isnt implemented
    {
        //anim.SetTrigger("died");
        dead = true;
        

    }
    public void TakeDamage(int damage, GameObject instagator) // Makes player take damage from some other external object, if they reach 0 HP, roommanager is called to kill player
    {
        /*if (PlayerHitSound != null)
            AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);*/

        FloatingText.Show(string.Format("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f)); // some of these parameters can be set in the code for easier changes in options

        //Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;
        if (Health <= 0)
        {
            RoomManager.Instance.KillPlayer();
        }
    }
    void Attack()//attack 
    {
        anim.SetTrigger("Attack");
    }
}
