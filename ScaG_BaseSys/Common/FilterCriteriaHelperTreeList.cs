using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using FilterCondition = DevExpress.Data.Filtering.FilterCondition;

namespace CNY_BaseSys.Common
{
    public static class FilterCriteriaHelperTreeList
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

        public static CriteriaOperator ReplaceFindPanelCriteria(CriteriaOperator source, TreeList view, CriteriaOperator newOperand, bool isAndSearchNew)
        {
            return ReplaceFilterCriteria(source, ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, true, isAndSearchNew), newOperand);
        }
       
        public static CriteriaOperator ConvertFindPanelTextToCriteriaOperator(string findPanelText, TreeList tl, bool applyPrefixes, bool isAndSearchNew)
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

                FindSearchParserResults parseResult = new FindSearchParser().Parse(newFilterString);
              //  FindSearchParserResults parseResult = new FindSearchParser().Parse(newFilterString, GetFindToColumnsCollection(view));
                if (applyPrefixes)
                {
                    parseResult.AppendColumnFieldPrefixes();
                }
                string[] arrCol = GetArrayColumnFilter(tl);
                return DxFtsContainsHelper.Create(arrCol,parseResult, FilterCondition.Contains);




            }
            return null;



        }
        
       

        private static string[] GetArrayColumnFilter(TreeList tl)
        {

            //System.Reflection.MethodInfo mi = typeof(TreeList).GetMethod("GetFindColumnsNames", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //string[] arr = mi.Invoke(tl, null) as string[];
            //if (arr == null) return null;
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = arr[i].Remove(0, "DxFts_".Length);
            //}
            //return arr;


            string[] arr = tl.VisibleColumns.Select(p => p.FieldName).ToArray();
            return arr;






        }
        /*
        private static ICollection GetFindToColumnsCollection(TreeList view)
        {
            System.Reflection.MethodInfo mi = typeof(TreeList).GetMethod("GetFindToColumnsCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return mi.Invoke(view, null) as ICollection;
        }
        */

        public static CriteriaOperator MyConvertFindPanelTextToCriteriaOperator(TreeList view, bool isAndSearchNew)
        {
            return ConvertFindPanelTextToCriteriaOperator(String.Format("\"{0}\"", view.FindFilterText), view, false, isAndSearchNew);
        }

       

    }

   
}
