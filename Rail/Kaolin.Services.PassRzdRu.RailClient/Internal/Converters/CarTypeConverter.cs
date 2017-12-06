using System;
using Kaolin.Models.Rail;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public class CarTypeConverter
    {
        public CarType ByCType(int cType)
        {
            switch (cType)
            {
                case 1: return new CommonCarType();
                case 2: return new SittingCarType();
                case 3: return new EconomCarType();
                case 4: return new CompartmentCarType();
                case 5: return new LuxuryCarType();
                case 6: return new SleepingCarType();
                default: throw new ArgumentException($"Неизвестный тип вагона {cType}", nameof(cType));
            }
        }
        
        public CarType ByCTypeI(int cTypeI)
        {
            switch (cTypeI)
            {
                case 1: return new EconomCarType();
                case 0: return new CommonCarType();
                case 3: return new SittingCarType();
                case 4: return new CompartmentCarType();
                case 5: return new LuxuryCarType();
                case 6: return new SleepingCarType();
                default: throw new ArgumentException($"Неизвестный тип вагона {cTypeI}", nameof(cTypeI));
            }
        }
    }
}
