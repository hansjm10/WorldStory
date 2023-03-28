using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data.Interfaces.Stats;

namespace DnDWorldCreate.Data.Stats
{
    public class BaseStats : BaseEntity, IConstitution, IStrength, IDexterity, IIntelligence, IWisdom, ICharisma
    {
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public BaseStats()
        {
            Constitution = 0;
            Intelligence = 0;
            Strength = 0;
            Dexterity = 0;
            Wisdom = 0;
            Charisma = 0;
        }
    }
}
