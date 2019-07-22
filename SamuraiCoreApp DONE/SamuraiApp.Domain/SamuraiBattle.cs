namespace SamuraiApp.Domain
{
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public virtual Samurai Samurai { get; set; }
        public int BattleId { get; set; }
        public virtual Battle Battle { get; set; }
        public int KillStreak { get; set; }
    }
}
