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

namespace ESys.Contract.Entity
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System;

    /// <summary>
    ///     Converts <see cref="TimeSpan" /> to and <see cref="TimeSpan.Ticks" />.
    /// </summary>
    public class NullableTimeSpanToTicksConverter : ValueConverter<TimeSpan?, long?>
    {
        /// <summary>
        ///     Creates a new instance of this converter.
        /// </summary>
        public NullableTimeSpanToTicksConverter(ConverterMappingHints mappingHints = null) :
          base(v => v.HasValue ? v.Value.Ticks : null, v => v.HasValue ? new TimeSpan(v.Value) : null, mappingHints)
        {
        }

        /// <summary>
        ///     A <see cref="ValueConverterInfo" /> for the default use of this converter.
        /// </summary>
        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(TimeSpan?), typeof(long?), i => new NullableTimeSpanToTicksConverter(i.MappingHints));
    }
}
