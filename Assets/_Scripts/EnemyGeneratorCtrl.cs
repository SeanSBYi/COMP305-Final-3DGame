using UnityEngine;
using System.Collections;

public class EnemyGeneratorCtrl : MonoBehaviour {
	// Public Instance Values.
	public GameObject enemyPrefab;
	public int maxEnemy = 5;

	// Private Instance Values.
	GameObject[] existEnemys;

	// Use this for initialization
	void Start() {
		existEnemys = new GameObject[maxEnemy];
		StartCoroutine(Exec());
	}

	IEnumerator Exec() {
		while(true){ 
			Generate();
			yield return new WaitForSeconds( 3.0f );
		}
	}

	void Generate() {
		for(int enemyCount = 0; enemyCount < existEnemys.Length; ++ enemyCount)
		{
			if( existEnemys[enemyCount] == null ){
				Vector3 tempVec3 = transform.position;
				tempVec3.x += Random.Range(-10.0F, 10.0f);
				tempVec3.y = 1.0f;
				//tempVec3.y = Random.Range(-10.0F, 10.0f);
				tempVec3.z += Random.Range(-10.0F, 10.0f);

				existEnemys[enemyCount] = Instantiate(enemyPrefab, tempVec3, transform.rotation) as GameObject;
				return;
			}
		}
	}
	
}
