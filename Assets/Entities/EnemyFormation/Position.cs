using UnityEngine;
using System.Collections;

//Enemy Position Script
public class Position : MonoBehaviour {


	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 1);	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col){
		
		//if(col.gameObject == gameObject.GetComponent<Projectile>()){
			print ("Collision");
		//}
	}
}
