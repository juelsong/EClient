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
namespace ESys.Service
{
    using ESys.Contract.Service;
    using ESys.Utilty.Defs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ESignDataHelper : IDataInjector, IDataProvider
    {
        private ESignData[] data = null;
        private int userId = 0;
        public void InjectESignData(IEnumerable<ESignData> eSign)
        {
            this.data = this.data == null
                        ? (eSign ?? Enumerable.Empty<ESignData>()).ToArray()
                        : throw new Exception("already inject esign");
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
            return this.data != null && this.data.Length > 0;
        }


        public bool TryGetCurrentUserId(out int userId)
        {
#if DEBUG
            userId = this.userId == 0 ? ConstDefs.SystemUserId : this.userId;
#else
            userId = this.userId;
#endif
            return userId != 0;
        }
    }
}
