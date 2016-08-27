using Assets;
using UnityEngine.SceneManagement;

public interface ISceneScript
{
    LevelManager.SceneInfo GetScene();
    Music GetMusic();
    SceneType GetSceneType();
    float GetDuration();
    void LoadNextScene();
    void LoadPreviousScene();
    void LoadOptionsScene();
    void LoadGameScene();
    void LoadTitleScene();
    void LoadWinScene();
    void LoadLoseScene();
    
}