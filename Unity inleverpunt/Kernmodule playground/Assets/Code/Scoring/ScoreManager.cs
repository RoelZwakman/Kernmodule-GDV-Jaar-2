public static class ScoreManager {

    private static int score; //////The current score


    public static void AddScore(int _scoreToAdd = 1)
    {
        score += _scoreToAdd;
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public static int GetScore()
    {
        return score;
    }

}
