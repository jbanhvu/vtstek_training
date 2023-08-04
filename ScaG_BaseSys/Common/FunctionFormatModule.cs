using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public static class FunctionFormatModule
    {
        public static string StrFormatRateGramValueDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatRateGramValueDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatRateGramValueDecimal, isN);
        }

        public static string StrFormatCodePackingFactorDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatCodePackingFactorDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatCodePackingFactorDecimal, isN);
        }


        public static string StrFormatDefaultDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatDefaultDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatDefaultDecimal, isN);
        }




        public static string StrFormatUcDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatUcDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatUcDecimal, isN);
        }
        public static string StrFormatOrderQtyDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatOrderQtyDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatOrderQtyDecimal, isN);
        }
        public static string StrFormatFactorDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatFactorDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatFactorDecimal, isN);
        }
        public static string StrFormatVolumnDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatVolumnDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatVolumnDecimal, isN);
        }

        public static string StrFormatWeightDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatWeightDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatWeightDecimal, isN);
        }

        public static string StrFormatFinishingValueDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatFinishingValueDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatFinishingValueDecimal, isN);
        }
        public static string StrFormatPrQtyDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatPrQtyDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatPrQtyDecimal, isN);
        }

        public static string StrFormatPoQtyDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatPoQtyDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatPoQtyDecimal, isN);
        }
        public static string StrFormatPackingFactorDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatPackingFactorDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatPackingFactorDecimal, isN);
        }
        public static string StrFormatStockQtyDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(ConstSystem.FormatStockQtyDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(ConstSystem.FormatStockQtyDecimal, isN);
        }
    }
}
