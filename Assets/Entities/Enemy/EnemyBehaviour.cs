using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float health = 150;
	public float projectileSpeed = 15;
	public float firingRate=0.2f;
	public float shotsPerSeconds = 0.5f;
	
	void Update(){
		//p(fire this frame) = frequency x time elapsed
		float probability = shotsPerSeconds * Time.deltaTime;
		//Random is between 0 and 1
		if(Random.value < probability){
			Fire ();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0,-1.5f,0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0,-projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0){
				Destroy(gameObject);
			}
		}
	}
}
