using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
   
    public Animator anim;
    public SceneManagement scenemngr;

    // Start is called before the first frame update
    void Start()
    {
       
        anim.GetComponent<Animator>();
        
        StartCoroutine("playAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
   private IEnumerator playAnim()
   {
        anim.SetInteger("load", SceneManagement.level);
        Debug.Log(SceneManagement.level + " + " + SceneManagement.levelno);
        Debug.Log(anim.GetCurrentAnimatorClipInfo(0).Length + 1);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length + 6);
        if(SceneManagement.level == 0 && SceneManagement.levelno == 0)
        {
            scenemngr.LoadScene("mars");
        }
        if(SceneManagement.level == 0 && SceneManagement.levelno == 1)
        {
            scenemngr.LoadScene("afterMars");
        }
        if (SceneManagement.level == 0 && SceneManagement.levelno == 2)
        {
            scenemngr.LoadScene("Asteroid1");
        }
        if (SceneManagement.level == 1 && SceneManagement.levelno == 3)
        {
            scenemngr.LoadScene("afterasteroid1");
        }
        if (SceneManagement.level == 1 && SceneManagement.levelno == 4)
        {
            scenemngr.LoadScene("europa");
        }
        if (SceneManagement.level == 2 && SceneManagement.levelno == 5)
        {
            scenemngr.LoadScene("europatotitan");
        }
        if (SceneManagement.level == 2 && SceneManagement.levelno == 6)
        {
            scenemngr.LoadScene("titan");
        }
        if (SceneManagement.level == 3 && SceneManagement.levelno == 7)
        {
            scenemngr.LoadScene("Asteroid2");
        }
        if (SceneManagement.level == 5 && SceneManagement.levelno == 8)
        {
            scenemngr.LoadScene("pluto");
        }
       


    }

    
}
