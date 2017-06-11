using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;
	
	public float speed = 4.5f;
		
	//Formation Size
	public float width = 10f;
	public float height = 5f;
	
	private float xmax;
	private float xmin;
	
	private bool movingRight = false;
	
	// Use this for initialization
	void Start () {
		
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));				
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		
		//For each Child object in the attached scripts gameobject container (Get its transform)
		foreach( Transform child in transform){
			//Instantiate an enemy at the child Position
			//Set its transform to the child's
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;			
		}else{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		
		if ( leftEdgeOfFormation < xmin){
			movingRight = true;
		} 
		else if(rightEdgeOfFormation > xmax)
		{
		 	movingRight = false;
		}
		//float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		
		//transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
