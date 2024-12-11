using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Contract.Entity
{
    public interface IKeyEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
