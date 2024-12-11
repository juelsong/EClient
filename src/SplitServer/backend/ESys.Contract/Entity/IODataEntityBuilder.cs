using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;

namespace ESys.Contract.Entity
{
    /// <summary>
    /// OData实体构造器接口
    /// </summary>
    public interface IODataEntityBuilder
    {
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name="builder"></param>
        void BuildODataModel(ODataModelBuilder builder);
    }
}
