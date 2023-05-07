public class Card
{
    public int cardId { get; set; }
    public int cardNum { get; set; }
    public CardColor color { get; set; }
}

public enum CardColor
{
    red,
    green,
    blue,
    yellow
}