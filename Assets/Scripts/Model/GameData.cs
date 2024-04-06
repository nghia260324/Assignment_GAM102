[System.Serializable]
public class GameData
{
    public float distance;
    public float time;
    public float highScore;

    public GameData(float d, float t, float h)
    {
        distance = d;
        time = t;
        highScore = h;
    }
}