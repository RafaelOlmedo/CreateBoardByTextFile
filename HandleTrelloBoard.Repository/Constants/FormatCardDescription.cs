namespace HandleTrelloBoard.Repository.Constants
{
    public static class FormatCardDescription
    {
        public static int ScoreCount;

        //public static string TopicLevel1 = $"+ **Ponto {ScoreCount}: **";
        public const string TopicLevel2 = " + ";
        public const string TopicLevel3 = "   +";
        public const string TopicLevel4 = "     +";
        public const string TopicLevel5 = "       + ";

        public static string GetTopicLevel1()
        {
            return $"+ **Ponto { ScoreCount}: **";
        }
    }
}
