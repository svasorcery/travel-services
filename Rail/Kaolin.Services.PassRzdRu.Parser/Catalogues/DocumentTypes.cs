namespace Kaolin.Services.PassRzdRu.Parser.Catalogues
{
    /// <summary>
    /// Document types
    /// <para>GET https://pass.rzd.ru/catalogue/app_passport_type/ru</para>
    /// <para>GET https://pass.rzd.ru/catalogue/app_passport_type/en</para>
    /// </summary>
    public class DocumentTypes
    {
        public string Result { get; set; }
        public string Timestamp { get; set; }

        public Passport[] Data { get; set; }

        public class Passport
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Policy_code { get; set; }
            public string Pattern { get; set; }
            public string Tooltip { get; set; }

            // if prop eq 0 then prop_sort is null
            public int Std { get; set; }
            public int? Std_sort { get; set; }
            public int Foreign { get; set; }
            public int? Foreign_sort { get; set; }
            public int Baby { get; set; }
            public int? Baby_sort { get; set; }
            public int Handicapped { get; set; }
            public int? Handicapped_sort { get; set; }
        }
    }
}
