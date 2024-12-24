namespace CardDeck
{


        [System.Serializable]
        public class CardString
        {
            public string point;
            public CardSuit suit;
            public bool isTreasure;
    
            public CardString(string point,CardSuit suit)
            {
                this.point = point;
                this.suit = suit;
                isTreasure = false;
            }
        }
}

    public enum CardSuit
    {
        黑桃,红桃,梅花,方块
    }