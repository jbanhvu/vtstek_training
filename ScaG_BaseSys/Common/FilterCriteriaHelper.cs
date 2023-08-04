using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Data.Filtering.Helpers;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace CNY_BaseSys.Common
{
    public static class FilterCriteriaHelper
    {
        public static CriteriaOperator ReplaceFilterCriteria(CriteriaOperator source, CriteriaOperator prevOperand, CriteriaOperator newOperand)
        {
            GroupOperator groupOperand = source as GroupOperator;
            if (ReferenceEquals(groupOperand, null))
                return newOperand;
            GroupOperator clone = groupOperand.Clone();
            clone.Operands.Remove(prevOperand);
            if (clone.Equals(source))
                return newOperand;
            clone.Operands.Add(newOperand);
            return clone;
        }

        public static CriteriaOperator ReplaceFindPanelCriteria(CriteriaOperator source, GridView view, CriteriaOperator newOperand, bool isAndSearchNew)
        {
            return ReplaceFilterCriteria(source, ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, true, isAndSearchNew), newOperand);
        }

        public static CriteriaOperator ConvertFindPanelTextToCriteriaOperator(string findPanelText, GridView view, bool applyPrefixes, bool isAndSearchNew)
        {
            if (!string.IsNullOrEmpty(findPanelText))
            {
                //FindSearchParserResults parseResult = new FindSearchParser().Parse(findPanelText, GetFindToColumnsCollection(view));
                //if (applyPrefixes)
                //    parseResult.AppendColumnFieldPrefixes();

                //return DxFtsContainsHelperAlt.Create(parseResult, FilterCondition.Contains, false);

                string newFilterString;
                if (isAndSearchNew)
                {
                    string[] parts = findPanelText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    newFilterString = parts[0].Replace("\"", "").Trim();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        newFilterString = newFilterString + " +" + parts[i].Replace("\"", "").Trim();
                    }
                }
                else
                {
                    newFilterString = findPanelText;
                }

             
                FindSearchParserResults parseResult = new FindSearchParser().Parse(newFilterString, GetFindToColumnsCollection(view));
                if (applyPrefixes)
                {
                    parseResult.AppendColumnFieldPrefixes();
                }
                return DxFtsContainsHelperAlt.Create(parseResult, FilterCondition.Contains, false);




            }
            return null;



        }



       
        private static IEnumerable<IDataColumnInfo> GetFindToColumnsCollection(GridView view)
        {
            //System.Reflection.MethodInfo mi = typeof(ColumnView).GetMethod("GetFindToColumnsCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //return  mi.Invoke(view, null) as ICollection;

            System.Reflection.MethodInfo mi = typeof(ColumnView).GetMethod("GetFindToColumnsCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi == null) return null;
            List<IDataColumnInfo> iCollection = mi.Invoke(view, null) as List<IDataColumnInfo>;
            if (iCollection == null) return null;
            return iCollection.Where(p => p.FieldName != GridView.CheckBoxSelectorColumnName);


        }
        public static CriteriaOperator MyConvertFindPanelTextToCriteriaOperator(GridView view, bool isAndSearchNew)
        {
            return ConvertFindPanelTextToCriteriaOperator(String.Format("\"{0}\"", view.FindFilterText), view, false, isAndSearchNew);
        }

    
      

       
    }
}
