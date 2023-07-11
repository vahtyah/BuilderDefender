using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public enum Scene
    {
        GameScene,
        MainMenuScene
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}