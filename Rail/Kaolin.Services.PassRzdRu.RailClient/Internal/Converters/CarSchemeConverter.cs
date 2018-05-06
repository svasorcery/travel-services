using System;
using System.Linq;
using Newtonsoft.Json;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    using Kaolin.Models.Rail;

    public class CarSchemeConverter
    {
        public static QueryCars.Result.CarScheme Convert(long id, string json)
        {
            var result = JsonConvert.DeserializeObject<RzdRuClientCarScheme>(json);

            var cells = result.Cells.Select(x => new QueryCars.Result.CarSchemeCell
            {
                Type = x.Type,
                Place = x.Number.HasValue ? new QueryCars.Result.CarPlace(x.Number.Value, false) : null,
                Content = x.Content,
                Border = GetCellBorder(x.Style),
            });

            var rowsCount = result.Cells.Length / result.Len;

            var rows = new QueryCars.Result.CarSchemeCell[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                rows[i] = new QueryCars.Result.CarSchemeCell[result.Len];
                for (int j = 0; j < result.Len; j++)
                {
                    var itemIndex = i * result.Len + j;
                    rows[i][j] = cells.ElementAt(itemIndex);
                }
            }

            return new QueryCars.Result.CarScheme
            {
                Id = id,
                Rows = rows
            };
        }


        private static string GetCellBorder(string rawStyle)
        {
            if (String.IsNullOrEmpty(rawStyle))
                return null;

            if (rawStyle.Contains("border-top")) return "top";
            if (rawStyle.Contains("border-right")) return "right";
            if (rawStyle.Contains("border-left")) return "left";
            if (rawStyle.Contains("border-bottom")) return "bottom";
            return null;
        }
    }


    public class RzdRuClientCarScheme
    {
        public int Len { get; set; }
        public Cell[] Cells { get; set; }

        public class Cell
        {
            public string Type { get; set; }
            public int? Number { get; set; }
            public string Content { get; set; }
            public string Style { get; set; }
        }
    }
}
