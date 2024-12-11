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
//    using System.Text.Json;
//    using System.Text.Json.Serialization;

//    /// <summary>
//    /// Graph反序列化
//    /// </summary>
//    public static class GraphDeserilizer
//    {
//        private static readonly JsonSerializerOptions GraphSerializerOptions = GetGraphSerializerOptions();
        
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public static JsonSerializerOptions GetGraphSerializerOptions()
//        {
//            var opt = new JsonSerializerOptions()
//            {
//                ReferenceHandler = ReferenceHandler.IgnoreCycles,
//                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//            };
//            opt.Converters.Add(new CellJsonConverter());
//            return opt;
//        }
//        /// <summary>
//        /// 反序列化
//        /// </summary>
//        /// <param name="graphJson"></param>
//        /// <returns></returns>
//        public static Graph Deserilize(string graphJson) => JsonSerializer.Deserialize<Graph>(graphJson, GraphSerializerOptions);
//    }
//}
