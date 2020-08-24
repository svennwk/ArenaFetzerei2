using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public int PlayerHealth = 100;


    public static PlayerManager instance;

    void Awake() {
        instance  = this;  
    }

    public GameObject player;


    void Update() {

        //Debug.Log(PlayerHealth);
        
        if (PlayerHealth <= 0) {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
