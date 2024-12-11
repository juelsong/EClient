/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting       
 *         ┃   ┃ +     神兽保佑,代码无bug  
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.UnitTest
{
    using ESys.Contract.Service;
    using ESys.Utilty.Defs;
    using Furion.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ESignDataHelper : IDataInjector, IDataProvider, IScoped
    {
        private ESignData[] data = null;
        private int userId = 0;
        public void InjectESignData(IEnumerable<ESignData> eSign)
        {
            this.data = this.data == null ? eSign.ToArray() : throw new Exception("already inject esign");
        }
        public void InjectCurrentUserId(int userId)
        {
            this.userId =
                this.userId == 0 ? userId :
                this.userId == userId ? this.userId = userId : throw new Exception("already inject userid");
        }

        public bool TryGetESignData(out IEnumerable<ESignData> esign)
        {
            esign = this.data;
            return this.data != null;
        }


        public bool TryGetCurrentUserId(out int userId)
        {
#if DEBUG
            userId = ConstDefs.SystemUserId;
#else
            userId = this.userId;
#endif
            return userId != 0;
        }
    }
}
