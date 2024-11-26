using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        Debug.Log(gameObject.tag);
        switch(gameObject.tag){
            case "Constructor":
                PlayerPrefs.SetInt("character", 1);
                break;
            case "Nature":
                PlayerPrefs.SetInt("character", 2);
                break;
            case "Nerd":
                PlayerPrefs.SetInt("character", 3);
                break;
            case "Handler":
                PlayerPrefs.SetInt("character", 4);
                break;
            case "Enchanter":
                PlayerPrefs.SetInt("character", 5);
                break;
        }

        SceneManager.LoadSceneAsync(1);
    }
}
