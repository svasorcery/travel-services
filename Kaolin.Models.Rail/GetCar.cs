namespace Kaolin.Models.Rail
{
    public class GetCar
    {
        public class Request
        {
            public int TrainRef { get; set; }
            public int OptionRef { get; set; }
        }

        public class Result
        {
            public QueryTrain.Result.TrainOption Train { get; set; }
            public GetCars.Result.Car Car { get; set; }
            // TODO: add Car schemes
        }
    }
}
