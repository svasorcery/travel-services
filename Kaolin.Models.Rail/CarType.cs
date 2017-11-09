namespace Kaolin.Models.Rail
{
    public abstract class CarType
    {
        internal int Id { get; set; }
        public string Category { get; set; }
        internal string Name { get; set; }
        internal string DisplayName { get; set; }
    }


    public class LuxuryCarType : CarType
    {
        public LuxuryCarType()
        {
            Id = 1;
            Category = "М";
            Name = "Мягкий";
            DisplayName = "Люкс";
        }
    }

    public class SleepingCarType : CarType
    {
        public SleepingCarType()
        {
            Id = 2;
            Category = "Л";
            Name = "Люкс";
            DisplayName = "СВ";
        }
    }

    public class CompartmentCarType : CarType
    {
        public CompartmentCarType()
        {
            Id = 3;
            Category = "К";
            Name = "Купе";
            DisplayName = "Купе";
        }
    }

    public class EconomCarType : CarType
    {
        public EconomCarType()
        {
            Id = 4;
            Category = "П";
            Name = "Плац";
            DisplayName = "Плацкартный";
        }
    }

    public class SittingCarType : CarType
    {
        public SittingCarType()
        {
            Id = 5;
            Category = "С";
            Name = "Сид";
            DisplayName = "Сидячий";
        }
    }

    public class CommonCarType : CarType
    {
        public CommonCarType()
        {
            Id = 6;
            Category = "О";
            Name = "Общ";
            DisplayName = "Общий";
        }
    }
}
