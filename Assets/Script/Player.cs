using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage 
{
	public float speed;
    public bool dead;
    public int MaxHealth = 10;
    public int Health { get; private set; }
	Animator anim;
	void Start() //gets animator componant 
	{
		anim = GetComponent<Animator> ();
        dead = false;
        Health = MaxHealth;
	}

	void Update()
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
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, -90);
                transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.A))//setting character to facing left
            {
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 90);
                transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.W))//setting character to facing up
            {
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 0);
                transform.rotation = rot;
            }
            if (Input.GetKey(KeyCode.S)) //setting character to facing down
            {
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 180);
                transform.rotation = rot;
            }
            if(Input.GetKey(KeyCode.K))
            {
                RoomManager.Instance.KillPlayer();
            }
        }
	}


    public void  Kill()
    {
        //anim.SetTrigger("died");
        dead = true;
        

    }
    public void TakeDamage(int damage, GameObject instagator)
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
