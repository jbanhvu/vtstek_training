using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using CNY_BaseSys.Common;
using CNY_Report.Info;
using DevExpress.Charts.Native;
using DevExpress.XtraPrinting;

namespace CNY_Report
{
    public partial class rpt001SOReportSub1 : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly Int64 _primaryKey;
        private readonly Inf_001SOReport _inf = new Inf_001SOReport();
        public rpt001SOReportSub1(Int64 primaryKey)
        {
            InitializeComponent();
            _primaryKey = primaryKey;
            //this.Detail.Controls.Add(CreateXRTableAgreement());
            this.ReportHeader.Controls.Add(CreateXRTableAgreement());
        }
        public XRTable CreateXRTableAgreement()
        {
            DataTable dt = LoadDataAgreement();

            // Create an empty table and set its size.
            XRTable table = new XRTable();
            table.Width = 1080;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            table.Font = new Font(new FontFamily("Microsoft Sans Serif"), 6, FontStyle.Regular);
            int columnCount = dt.Columns.Count;
            int maxLengh = 0;
            // Create table row.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XRTableRow row = new XRTableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToString() == "Name")
                    {
                        XRTableCell cellName = new XRTableCell();
                        if (i == 0)
                        {
                            
                        cellName.Borders= ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom| DevExpress.XtraPrinting.BorderSide.Top)));
                        }
                        else
                        {
                            
                        cellName.Borders= ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom )));
                        }
                        cellName.Text =" "+(i+1).ToString()+". "+ dt.Rows[i][j].ToString()+":";
                        maxLengh = maxLengh > cellName.Text.Length ? maxLengh : cellName.Text.Length;
                        //cellName.Width = (maxLengh * 6) + 50;
                        cellName.Font = new Font(new FontFamily("Microsoft Sans Serif"), 6, FontStyle.Bold);
                        cellName.TextAlignment=TextAlignment.MiddleLeft;
                        row.Cells.Add(cellName);
                    }
                    else
                    {
                        XRTableCell cellValue = new XRTableCell();
                        if (i == 0)
                        {
                        cellValue.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right| DevExpress.XtraPrinting.BorderSide.Top)));
                        }
                        else
                        {
                            
                        cellValue.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right)));
                        }

                        cellValue.Multiline = true;
                        cellValue.Text = GetInfoByDataRow(dt.Rows[i], columnCount);
                        row.Cells.Add(cellValue);
                        break;
                    }

                }
                table.Rows.Add(row);
            }
            foreach (XRTableRow row in table)
            {
                row.Cells[0].Width = maxLengh*3+20;
                row.Cells[1].Width = table.Width - row.Cells[0].Width;
            }
            // Finish table initialization.
            //table.AdjustSize();
            table.EndInit();

            return table;
        }
        public string GetInfoByDataRow(DataRow dr,int columnCount)
        {
            bool isChecked=false;
            string s="";
            int agreementCount = 0;
            for (int i = 1; i < columnCount; i++)
            {
                if (i%2 == 1)
                {
                    if (ProcessGeneral.GetSafeBool(dr[i]))
                    {
                        isChecked = true;
                    }
                    else
                    {
                        isChecked = false;
                    }
                }
                else
                {
                    if (isChecked)
                    {
                        if (agreementCount > 0)
                        {
                            s = s + "\n";
                        }
                        s +="- "+ dr[i].ToString();
                        agreementCount++;
                    }
                }
            }
            return s;
        }
        private DataTable LoadDataAgreement()
        {
            DataTable dt = new DataTable();
            if (_primaryKey > 0)
            {
                dt = _inf.LoadAgreeMent_Save(_primaryKey);
            }
            else
            {
                dt = _inf.LoadAgreeMent();
            }
            var q1 = dt.AsEnumerable().GroupBy(p => new
            {
                PKParent = Convert.ToInt32(p["PKParent"]),
                Name = p.Field<String>("Name"),
            }).Select(t => new
            {
                t.Key.PKParent,
                t.Key.Name,
                DataItem = t.Select(s => new ChildItemAgreement
                {
                    Col = s.Field<String>("Values"),
                    Pk = Convert.ToInt32(s["ChildPK"]),
                    Sav = Convert.ToInt32(s["CNY021PK"]),
                    Chk = s.Field<bool>("IsChecked"),
                }).OrderBy(a=>a.Pk).ToList()
            }).ToList();

            Int32 maxRow = q1.Select(p => p.DataItem.Count).Max();

            DataTable dtT = new DataTable();
            dtT.Columns.Add("PKParent", typeof(Int32));
            dtT.Columns.Add("Name", typeof(String));
            for (int i = 0; i < maxRow; i++)
            {
                dtT.Columns.Add($"chk{i}", typeof(bool));
                dtT.Columns.Add($"Col{i}", typeof(String));
            }

            foreach (var item in q1)
            {
                DataRow dr = dtT.NewRow();
                dr["PKParent"] = item.PKParent;
                dr["Name"] = item.Name;

                List<ChildItemAgreement> l = item.DataItem;

                for (int i = 0; i < l.Count; i++)
                {
                    ChildItemAgreement c = l[i];
                    dr[$"chk{i}"] = c.Chk;
                    dr[$"Col{i}"] = c.Col;
                }
                dtT.Rows.Add(dr);
            }
            for (int i = 0; i < dtT.Columns.Count; i++)
            {
                if (dtT.Columns[i].ColumnName.Trim() == "PKParent")
                {
                    dtT.Columns.RemoveAt(i);
                }
            }
            return dtT;
        }
       
    }
}
