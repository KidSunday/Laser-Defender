using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject Projectile;
	
	public float speed = 15.0f;
	public float padding = 0.5f;
	public float projectileSpeed;
	public float firingRate=0.2f;
	public float health = 250f;
	float xmin;
	float xmax;

	void Start(){
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	void Fire(){
		//GameObject laser = Instantiate(Projectile, this.transform.position, Quaternion.identity) as GameObject;
		
		//Offset b/c player laser was killing himself
		Vector3 offset = new Vector3(0,1,0);
		
		GameObject beam = Instantiate(Projectile, transform.position+offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
	}

		// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			//Fire();
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			//Fire();
			//
			CancelInvoke("Fire");
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			//transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}		
				
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile){
			Debug.Log ("Player Hit");
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0){
				Destroy(gameObject);
			}
		}
	}
}
