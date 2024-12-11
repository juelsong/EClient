using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHost.Security.Entity;

namespace EHost.Contract.Entity
{
    public abstract class EntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
           where TEntity : EntityBase<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(x => x.Id);

        }
    }
}
