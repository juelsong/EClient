///*
// *        ┏┓   ┏┓+ +
// *       ┏┛┻━━━┛┻┓ + +
// *       ┃       ┃
// *       ┃   ━   ┃ +++ + +
// *       ████━████ ┃+
// *       ┃       ┃ +
// *       ┃   ┻   ┃
// *       ┃       ┃ + +
// *       ┗━┓   ┏━┛
// *         ┃   ┃
// *         ┃   ┃ + + + +
// *         ┃   ┃    Code is far away from bug with the animal protecting
// *         ┃   ┃ +     神兽保佑, 代码无bug
// *         ┃   ┃
// *         ┃   ┃  +
// *         ┃    ┗━━━┓ + +
// *         ┃        ┣┓
// *         ┃        ┏┛
// *         ┗┓┓┏━┳┓┏┛ + + + +
// *          ┃┫┫ ┃┫┫
// *          ┗┻┛ ┗┻┛+ + + +
// */

//namespace ESys.Utilty.Serialization
//{
//    using ESys.Contract.Workflow;
//    using System;
//    using System.Linq;
//    using System.Text.Json;
//    using System.Text.Json.Serialization;

//    internal class CellJsonConverter : JsonConverter<Cell>
//    {
//        public override Cell Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            if (reader.TokenType != JsonTokenType.StartObject)
//            {
//                throw new JsonException();
//            }
//            var doc = JsonDocument.ParseValue(ref reader);
//            var shapeElement = doc.RootElement.EnumerateObject().FirstOrDefault(e => e.NameEquals("shape"));
//            return "edge".Equals(shapeElement.Value.GetString())
//                   ? doc.Deserialize<Edge>(options)!
//                   : doc.Deserialize<Node>(options)!;
//        }

//        public override void Write(Utf8JsonWriter writer, Cell value, JsonSerializerOptions options)
//        {
//            throw new NotImplementedException();
//        }
//    }

//}
