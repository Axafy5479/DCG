using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    [SerializeField] private WBTransition.Scene gameScene;

    public void OnButtonClicked()
    {
        WBTransition.SceneManager.LoadScene(gameScene);
    }
}
