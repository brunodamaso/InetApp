using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace INetApp.APIWebServices.Helpers
{
    public static class JsonService
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
        public static TValue Deserialize<TValue>(string json, JsonSerializerSettings options = null)
        {
            try
            {
                return JsonConvert.DeserializeObject<TValue>(json, options ?? Settings);
            }
            catch (Exception ex)
            {
                return default;
            }

        }
        public static string Serialize(object value, JsonSerializerSettings options = null)
        {
            try
            {
                return JsonConvert.SerializeObject(value, options ?? Settings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    //
    //
    //
    //      System.Text.Json;
    //
    //
    //
    //private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions()
    //{
    //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    //    IncludeFields = true,
    //    PropertyNameCaseInsensitive = true,
    //    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    //    Converters = { new BoolConverter(), new DateTimeConverter(), new IntConverter() }
    //};

    //    public static TValue Deserialize<TValue>(string json, JsonSerializerOptions options = null)
    //    {
    //        try
    //        {
    //            return JsonSerializer.Deserialize<TValue>(json, options ?? Settings);
    //        }
    //        catch (Exception ex)
    //        {
    //            return default;
    //        }
    //    }

    //    public static string Serialize(object value, JsonSerializerOptions options = null)
    //    {
    //        try
    //        {
    //            return JsonSerializer.Serialize(value, options ?? Settings);
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }
    //}
    //public class IntConverter : JsonConverter<int>
    //{
    //    public override void Write(Utf8JsonWriter writer, int intValue, JsonSerializerOptions options)
    //    {
    //        writer.WriteNumberValue(intValue);
    //    }
    //    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        return reader.TokenType == JsonTokenType.Null ? default : reader.GetInt32();
    //    }
    //}

    //public class DateTimeConverter : JsonConverter<DateTime>
    //{
    //    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    //    {
    //        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    //    }
    //    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        if (reader.GetString() is null)
    //        {
    //            return default;
    //        }
    //        return DateTime.Parse(reader.GetString());
    //    }
    //}
    //public class BoolConverter : JsonConverter<bool>
    //{
    //    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
    //        writer.WriteBooleanValue(value);

    //    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        bool resultado;
    //        switch (reader.TokenType)
    //        {
    //            case JsonTokenType.String:
    //                resultado = bool.TryParse(reader.GetString(), out var b) ? b : throw new JsonException();
    //                break;
    //            case JsonTokenType.Number:
    //                resultado = reader.TryGetInt64(out long l) ? Convert.ToBoolean(l) : reader.TryGetDouble(out double d) ? Convert.ToBoolean(d) : false;
    //                break;
    //            case JsonTokenType.True:
    //                resultado = true;
    //                break;
    //            case JsonTokenType.False:
    //                resultado = false;
    //                break;
    //            default:
    //                throw new JsonException();
    //        }
    //        return resultado;
    //    }
    //}
}


