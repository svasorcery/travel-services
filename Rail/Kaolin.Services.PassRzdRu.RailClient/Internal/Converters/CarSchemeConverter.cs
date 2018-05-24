using System;
using System.Linq;
using System.Collections.Generic;
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
                Border = GetCellBorder(x.Style)
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

        public static QueryCars.Result.CarScheme SetCellStyleClasses(QueryCars.Result.CarScheme pattern, IEnumerable<QueryCars.Result.CarPlace> freeSeats)
        {
            foreach (var row in pattern.Rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    var place = row[i].Place != null ? freeSeats.FirstOrDefault(x => x.Number == row[i].Place?.Number) : null;
                    row[i] = SetCellStyleClasses(row[i], place);
                }
            }

            return pattern;
        }


        private static string GetCellBorder(string rawStyle) =>
            !String.IsNullOrEmpty(rawStyle) && rawStyle.Contains("border-") ?
                rawStyle.Replace("border-", "") :
                null;

        private static QueryCars.Result.CarSchemeCell SetCellStyleClasses(
            QueryCars.Result.CarSchemeCell cell,
            QueryCars.Result.CarPlace place = null
            )
        {
            cell.AppendStyleClass(cell.Type); //!= "XX" ? cell.Type : null;

            if (!String.IsNullOrEmpty(cell.Content))
            {
                if (cell.Content.Contains("Первый этаж"))
                {
                    cell.AppendStyleClass("first-floor");
                }
                else if (cell.Content.Contains("Второй этаж"))
                {
                    cell.AppendStyleClass("second-floor");
                }
            }

            if (cell.Place != null)
            {
                /*if (carCType == 1)
                {
                    cell.AppendStyleClass("plc");
                }*/

                if (cell.Place.Number.ToString().Length > 2)
                {
                    cell.AppendStyleClass("less-letter");
                }

                if (place != null)
                {
                    cell.Place = place;
                    cell.AppendStyleClass($"gender-{place.Gender}");
                    cell.AppendStyleClass("booking");
                }
                else
                {
                    cell.AppendStyleClass("non-booking");
                }
            }

            return cell;
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
