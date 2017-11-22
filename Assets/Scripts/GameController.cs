using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public int enemyShipAppear;
	public float spawnWave;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text waveCountText;
	private int score;
	private bool gameOver;
	private bool restart;
	private int waveCount;
	public float waveTextDisable;


	void Start(){
		Cursor.visible = false;
		waveCount=1;
		StartCoroutine(SpawnWaves());
		StartCoroutine(UpdateWaveCount());
		score = 0;
		UpdateScore();
		gameOver=false;
		restart=false;
		restartText.text="";
		gameOverText.text="";
	}

	void Update(){
		if(restart){
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	IEnumerator SpawnWaves(){

		yield return new WaitForSeconds(startWait);

		while(true){
			for(int i = 0 ; i<hazardCount ; i++)
			{	
				GameObject hazard = hazards [Random.Range(0,2)];
				if(waveCount>=enemyShipAppear){
					hazard = hazards [Random.Range(1,3)];
				}
				if(waveCount>=enemyShipAppear*2){
					hazard = hazards [Random.Range(0,hazards.Length)];
				}
				
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard,spawnPosition,spawnRotation);
				yield return new WaitForSeconds(spawnWave);	
				
			}

			yield return new WaitForSeconds(waveWait);
			waveCount++;
			
			StartCoroutine(UpdateWaveCount());

			if(gameOver){
				restartText.text="Press 'R' to Restart";
				restart=true;
				break;
			}
		}
	}

	void UpdateScore(){
		scoreText.text="Score: " + score;
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver(){
		gameOverText.text="Game Over";
		gameOver=true;
	}

	IEnumerator UpdateWaveCount(){
		if(gameOver==false){
		waveCountText.text="Wave "+waveCount;
		yield return new WaitForSeconds(waveTextDisable);
		waveCountText.text="";
		}
	}
}
