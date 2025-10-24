using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public void SetDifficulty(int difficultyIndex)
    {
        switch (difficultyIndex) //segun el valor cambia la dificultad
        {
            case 0: //normal
                GlobalVariables.ChangeDifficulty(1.0f);
                break;
            case 1: //hard
                GlobalVariables.ChangeDifficulty(1.5f);
                break;
            case 2: //nightmare
                GlobalVariables.ChangeDifficulty(2.0f);
                break;
            default:
                Debug.LogWarning("Invalid difficulty index");
                break;
        }
        
    }
    
    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex); 
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
