using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void LaunchOpenBarLive()
    {
        Application.OpenURL("https://www.twitch.tv/videos/2078216421");
        Application.Quit();
    }
}
