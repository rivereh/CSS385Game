using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadNextScene), 14f);
    }

    // Update is called once per frame
    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
