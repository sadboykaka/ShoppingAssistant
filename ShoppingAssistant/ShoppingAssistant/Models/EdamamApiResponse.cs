using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShoppingAssistant.Models
{
    /// <summary>
    /// Edamam Response class 
    /// Generated using quicktype.io
    /// </summary>
    public partial class EdamamResponse
    {
        /// <summary>
        /// Query string
        /// </summary>
        [JsonProperty("q")]
        public string Q { get; set; }

        /// <summary>
        /// Index of first
        /// </summary>
        [JsonProperty("from")]
        public long From { get; set; }

        /// <summary>
        /// Index of last
        /// </summary>
        [JsonProperty("to")]
        public long To { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        [JsonProperty("params")]
        public Params Params { get; set; }

        /// <summary>
        /// True if more than the maximum number of results in a single response are found
        /// </summary>
        [JsonProperty("more")]
        public bool More { get; set; }

        /// <summary>
        /// Number of results found
        /// </summary>
        [JsonProperty("count")]
        public long Count { get; set; }

        /// <summary>
        /// The results (hits)
        /// </summary>
        [JsonProperty("hits")]
        public List<Hit> Hits { get; set; }
    }

    /// <summary>
    /// Edamam Parameters class
    /// Generated using quicktype.io
    /// </summary>
    public class Params
    {
        [JsonProperty("sane")]
        public List<object> Sane { get; set; }

        /// <summary>
        /// Query strings
        /// </summary>
        [JsonProperty("q")]
        public List<string> Q { get; set; }

        /// <summary>
        /// Index of first
        /// </summary>
        [JsonProperty("from")]
        public List<string> From { get; set; }

        /// <summary>
        /// Index of last
        /// </summary>
        [JsonProperty("to")]
        public List<string> To { get; set; }
    }

    /// <summary>
    /// Edamam Hit class
    /// Generated using quicktype.io
    /// </summary>
    public class Hit
    {
        /// <summary>
        /// The recipe
        /// </summary>
        [JsonProperty("recipe")]
        public Recipe Recipe { get; set; }

        /// <summary>
        /// Bookmarked by the user
        /// </summary>
        [JsonProperty("bookmarked")]
        public bool Bookmarked { get; set; }

        /// <summary>
        /// Bought by the user
        /// </summary>
        [JsonProperty("bought")]
        public bool Bought { get; set; }
    }

    /// <summary>
    /// Edamam Recipe class
    /// Generated using quicktype.io
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Edamam URI for the recipe
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Recipe title
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Source site identifier
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Recipe URL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("shareAs")]
        public string ShareAs { get; set; }

        /// <summary>
        /// Number of servings
        /// </summary>
        [JsonProperty("yield")]
        public long Yield { get; set; }

        [JsonProperty("dietLabels")]
        public List<string> DietLabels { get; set; }

        [JsonProperty("healthLabels")]
        public List<string> HealthLabels { get; set; }

        [JsonProperty("cautions")]
        public List<object> Cautions { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("ingredientLines")]
        public List<string> IngredientLines { get; set; }

        /// <summary>
        /// List of Ingredients
        /// </summary>
        [JsonProperty("ingredients")]
        public List<Ingredient> Ingredients { get; set; }

        [JsonProperty("calories")]
        public double Calories { get; set; }

        [JsonProperty("totalWeight")]
        public double TotalWeight { get; set; }

        [JsonProperty("totalNutrients")]
        public Total TotalNutrients { get; set; }

        [JsonProperty("totalDaily")]
        public Total TotalDaily { get; set; }

        [JsonProperty("digest")]
        public List<Digest> Digest { get; set; }
    }

    /// <summary>
    /// Edamam Total Nutrient class
    /// Generated using quicktype.io
    /// </summary>
    public class Total
    {
        [JsonProperty("ENERC_KCAL")]
        public Nutrient EnercKcal { get; set; }

        [JsonProperty("FAT")]
        public Nutrient Fat { get; set; }

        [JsonProperty("FASAT")]
        public Nutrient Fasat { get; set; }

        [JsonProperty("FATRN")]
        public Nutrient Fatrn { get; set; }

        [JsonProperty("FAMS")]
        public Nutrient Fams { get; set; }

        [JsonProperty("FAPU")]
        public Nutrient Fapu { get; set; }

        [JsonProperty("CHOCDF")]
        public Nutrient Chocdf { get; set; }

        [JsonProperty("FIBTG")]
        public Nutrient Fibtg { get; set; }

        [JsonProperty("SUGAR")]
        public Nutrient Sugar { get; set; }

        [JsonProperty("PROCNT")]
        public Nutrient Procnt { get; set; }

        [JsonProperty("CHOLE")]
        public Nutrient Chole { get; set; }

        [JsonProperty("NA")]
        public Nutrient Na { get; set; }

        [JsonProperty("CA")]
        public Nutrient Nutrient { get; set; }

        [JsonProperty("MG")]
        public Nutrient Mg { get; set; }

        [JsonProperty("K")]
        public Nutrient K { get; set; }

        [JsonProperty("FE")]
        public Nutrient Fe { get; set; }

        [JsonProperty("ZN")]
        public Nutrient Zn { get; set; }

        [JsonProperty("P")]
        public Nutrient P { get; set; }

        [JsonProperty("VITA_RAE")]
        public Nutrient VitaRae { get; set; }

        [JsonProperty("VITC")]
        public Nutrient Vitc { get; set; }

        [JsonProperty("THIA")]
        public Nutrient Thia { get; set; }

        [JsonProperty("RIBF")]
        public Nutrient Ribf { get; set; }

        [JsonProperty("NIA")]
        public Nutrient Nia { get; set; }

        [JsonProperty("VITB6A")]
        public Nutrient Vitb6A { get; set; }

        [JsonProperty("FOLDFE")]
        public Nutrient Foldfe { get; set; }

        [JsonProperty("FOLFD")]
        public Nutrient Folfd { get; set; }

        [JsonProperty("FOLAC")]
        public Nutrient Folac { get; set; }

        [JsonProperty("VITB12")]
        public Nutrient Vitb12 { get; set; }

        [JsonProperty("VITD")]
        public Nutrient Vitd { get; set; }

        [JsonProperty("TOCPHA")]
        public Nutrient Tocpha { get; set; }

        [JsonProperty("VITK1")]
        public Nutrient Vitk1 { get; set; }

        [JsonProperty("SUGAR.added")]
        public Nutrient SugarAdded { get; set; }
    }

    /// <summary>
    /// Edamam Nutrient Info class
    /// Generated using quicktype.io
    /// </summary>
    public class Nutrient
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    /// <summary>
    /// Edamam Ingredient class
    /// Generated using quicktype.io
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Ingredient text, concatenation of measure, quantity, and name
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Ingredient quantity for the given measure
        /// </summary>
        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        /// <summary>
        /// Ingredient measure for the given quantity
        /// </summary>
        [JsonProperty("measure")]
        public string Measure { get; set; }

        /// <summary>
        /// Ingredient name
        /// </summary>
        [JsonProperty("food")]
        public string Food { get; set; }

        /// <summary>
        /// Ingredient weight in grams
        /// </summary>
        [JsonProperty("weight")]
        public double Weight { get; set; }
    }

    /// <summary>
    /// Edamam Digest class
    /// Generated using quicktype.io
    /// </summary>
    public class Digest
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("schemaOrgTag")]
        public string SchemaOrgTag { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("hasRDI")]
        public bool HasRdi { get; set; }

        [JsonProperty("daily")]
        public double Daily { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("sub")]
        public List<Digest> Sub { get; set; }
    }

    /// <summary>
    /// Edamam Response class
    /// Generated using quicktype.io
    /// </summary>
    public partial class EdamamResponse
    {
        /// <summary>
        /// Converts the given json string to an EdamamResponse object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static EdamamResponse FromJson(string json) => JsonConvert.DeserializeObject<EdamamResponse>(json, Converter.Settings);
    }

    /// <summary>
    /// Serializer class
    /// Generated using quicktype.io
    /// </summary>
    public static class Serialize
    {
        public static string ToJson(this EdamamResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    /// <summary>
    /// Convertor class
    /// Generated using quicktype.io
    /// </summary>
    public static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
