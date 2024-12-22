namespace CardDeck
{


        [System.Serializable]
        public struct CardString
        {
            public string point;
            public CardSuit suit;
    
            public CardString(string point,CardSuit suit)
            {
                this.point = point;
                this.suit = suit;
            }
        }
}

    public enum CardSuit
    {
        黑桃,红桃,梅花,方块
    }