using System;
using System.Linq;

namespace CfmArt.ValueObject
{
    /// <summary>税率</summary>
    public class TaxRate
        : FractionValue<TaxRate>
    {
    }

    /// <summary>金額</summary>
    public class Money
    {
        /// <summary>税抜き</summary>
        public class ExcludingTax
            : IntegralValue<ExcludingTax>
        {
            /// <summary>足す</summary>
            public ExcludingTax Add(ExcludingTax rhs)
                => ExcludingTax.FromLong(ToLong() + rhs.ToLong());
            
            /// <summary>引く</summary>
            public ExcludingTax Subtract(ExcludingTax rhs)
                => ExcludingTax.FromLong(ToLong() - rhs.ToLong());

            /// <summary>税込み</summary>
            public IncludingTax IncludeTax(TaxRate rate)
                => new IncludingTax(this, rate);

            /// <summary>小数がない金額へ変換</summary>
            public long ToMoney() => ToLong();

            /// <summary>小数がある金額へ変換</summary>
            public decimal ToMoney(int fraction)
                => Enumerable.Range(0, fraction).Aggregate((decimal) ToLong(), (v, _) => v / 10M);

            /// <summary>小数がある金額から変換</summary>
            public static ExcludingTax FromMoney(decimal value, int fraction)
                => FromLong((long) Enumerable.Range(0, fraction).Aggregate(value, (v, _) => v * 10M));
        }

        /// <summary>税込み</summary>
        public class IncludingTax
        {
            /// <summary>税抜き</summary>
            private ExcludingTax Excluding { get; set; }
            /// <summary>税率</summary>
            private TaxRate Tax { get; set; }

            /// <summary></summary>
            public IncludingTax(ExcludingTax price, TaxRate tax)
            {
                Excluding = price;
                Tax = tax;
            }

            /// <summary>数値</summary>
            public long ToLong()
                => (long) Math.Floor(Excluding.ToLong() * (1 + Tax.ToDecimal()));

            /// <summary>小数がない金額へ変換</summary>
            public long ToMoney() => ToLong();

            /// <summary>小数がある金額へ変換</summary>
            public decimal ToMoney(int fraction)
                => Enumerable.Range(0, fraction).Aggregate((decimal) ToLong(), (v, _) => v / 10M);

            /// <summary>税抜き</summary>
            public ExcludingTax ExcludeTax() => Excluding;

            /// <summary>税率</summary>
            public TaxRate Rate() => Tax;

            /// <summary>税額</summary>
            public decimal TaxValue()
                => ToLong() - Excluding.ToLong();

            /// <summary>足す</summary>
            public IncludingTax Add(IncludingTax rhs)
                => rhs.Tax.Equals(Tax)
                    ? new IncludingTax(rhs.Excluding.Add(Excluding), Tax)
                    : throw new InvalidOperationException("Tax rates are different in " + Tax.ToString() + " and " + rhs.Tax.ToString());
            
            /// <summary>引く</summary>
            public IncludingTax Subtract(IncludingTax rhs)
                => rhs.Tax.Equals(Tax)
                    ? new IncludingTax(Excluding.Subtract(rhs.Excluding), Tax)
                    : throw new InvalidOperationException("Tax rates are different in " + Tax.ToString() + " and " + rhs.Tax.ToString());
        }
    }
}
