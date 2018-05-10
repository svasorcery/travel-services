namespace Kaolin.Models.Rail
{
    public class QueryCar
    {
        public class Request
        {
            public int TrainRef { get; set; }
            public int OptionRef { get; set; }


            public Request()
            {

            }

            public Request(int trainRef, int optionRef)
            {
                TrainRef = trainRef;
                OptionRef = optionRef;
            }
        }

        public class Result
        {
            public QueryTrain.Result.TrainOption Train { get; set; }
            public QueryCars.Result.Car Car { get; set; }
            public QueryCars.Result.CarScheme Scheme { get; set; }
        }
    }
}
