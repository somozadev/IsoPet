using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes_Controller : MonoBehaviour
{
    void Awake()
    {

        if(GameObject.FindObjectsOfType<Scenes_Controller>().Length > 1)
        {
            foreach(Scenes_Controller sceneController in GameObject.FindObjectsOfType<Scenes_Controller>())
            {
                Destroy(sceneController);
            }
        }
        else
            DontDestroyOnLoad(this.gameObject);
    
    }


    public void ExitGame()
    {
        //here mayb save game
        Application.Quit();
    }


    public void LoadMainScene(float waitTime) => StartCoroutine(WaitToLoadScene(waitTime));
    
    public IEnumerator WaitToLoadScene(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene("MainScene",LoadSceneMode.Single);
    
    }

    
}
