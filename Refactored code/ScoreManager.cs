public static class ScoreManager {

    public static int score; //////The current score


    public static void AddScore(int _scoreToAdd = 1)
    {
        score += _scoreToAdd;
    }

    public static void ResetScore()
    {
        score = 0;
    }

}
