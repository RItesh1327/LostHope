using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum allScenes {marsToAsteroid,AsteroidtoJupiter,Jupitertosaturn,saturntoAsteroids,asteridstoneptune,neptunetopluto }
public class SceneManagement : MonoBehaviour
{
    public SoundManager soundmanager;

    public static int level = 0;
    public static int levelno = 0;
   
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator mars()
    {
        SceneManagement.level = 0;
        levelno = 0;
        yield return new WaitForSeconds(0.2f);

    }
    public IEnumerator aftermars()
    {
        
        soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 0;
        levelno = 1;
    }
    public IEnumerator Asteroid1()
    {
       
      //  soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 0;
        levelno = 2;
    }
    public IEnumerator afterasteroid1()
    {
        
        //   soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 1;
        levelno = 3;
    }
    public IEnumerator europa()
    {
       
        //   soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 1;
        levelno = 4;
    }
    public IEnumerator europaTotitan()
    {
       
        //   soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 2;
        levelno = 5;
    }
    public IEnumerator Titan()
    {
       
        //    soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 2;
        levelno = 6;
    }
    public IEnumerator asteroid2()
    {
        
        //    soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 3;
        levelno = 7;
    }
    public IEnumerator pluto()
    {
       
        //   soundmanager.PlayJetStartSound();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("loading");
        SceneManagement.level = 5;
        levelno = 8;
    }
   
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
