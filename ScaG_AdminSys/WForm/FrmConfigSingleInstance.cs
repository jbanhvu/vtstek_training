using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_AdminSys.WForm
{
	public partial class FrmConfigSingleInstance : FrmBase
	{
		#region "Property"
		private WaitDialogForm _dlg;
		#endregion

		#region "Contructor"
		public FrmConfigSingleInstance()
		{
			InitializeComponent();
			this.Load += FrmConfigSingleInstance_Load;
			GridViewMacCustomInit();
		}

	   
		#endregion

		#region "Process Gridview"
		private void GridViewMacCustomInit()
		{
			var chkeditCom = new RepositoryItemCheckEdit() { AutoHeight = true };
			gcMac.UseEmbeddedNavigator = true;

			gcMac.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			gcMac.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			gcMac.EmbeddedNavigator.Buttons.Append.Visible = false;
			gcMac.EmbeddedNavigator.Buttons.Edit.Visible = false;
			gcMac.EmbeddedNavigator.Buttons.Remove.Visible = false;

			
			//   gvCom.OptionsBehavior.AutoPopulateColumns = false;
			gvMac.OptionsBehavior.Editable = true;
			gvMac.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
			gvMac.OptionsCustomization.AllowColumnMoving = false;
			gvMac.OptionsCustomization.AllowQuickHideColumns = true;
			gvMac.OptionsCustomization.AllowSort = true;
			gvMac.OptionsCustomization.AllowFilter = true;

			//     gvUpd.OptionsHint.ShowCellHints = true;

			gvMac.OptionsView.ShowGroupPanel = false;
			gvMac.OptionsView.ShowIndicator = true;
			gvMac.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
			gvMac.OptionsView.ShowVerticalLines = DefaultBoolean.True;
			gvMac.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
			gvMac.OptionsView.ShowAutoFilterRow = false;
			gvMac.OptionsView.AllowCellMerge = false;
			gvMac.HorzScrollVisibility = ScrollVisibility.Auto;
			gvMac.OptionsView.ColumnAutoWidth = false;

			//gvCom.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

			gvMac.OptionsNavigation.AutoFocusNewRow = true;
			gvMac.OptionsNavigation.UseTabKey = true;

			gvMac.FocusRectStyle = DrawFocusRectStyle.CellFocus;
			gvMac.OptionsSelection.MultiSelect = true;
			gvMac.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
			gvMac.OptionsSelection.EnableAppearanceFocusedRow = true;
			gvMac.OptionsSelection.EnableAppearanceFocusedCell = true;

			gvMac.OptionsView.EnableAppearanceEvenRow = true;
			gvMac.OptionsView.EnableAppearanceOddRow = true;

			gvMac.OptionsView.ShowFooter = true;

			gvMac.OptionsHint.ShowCellHints = false;

			//   gvCom.RowHeight = 25;

			gvMac.OptionsFind.AllowFindPanel = true;
			//gvCom.OptionsFind.AlwaysVisible = true;//==>false==>gvCom.OptionsFind.ShowCloseButton = true;
			gvMac.OptionsFind.AlwaysVisible = false;
			gvMac.OptionsFind.ShowCloseButton = true;
			gvMac.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvMac)
            {
                IsPerFormEvent = true,
            };

			gvMac.OptionsPrint.AutoWidth = false;


			var gridColumn3 = new GridColumn();
			gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn3.Caption = @"ID";
			gridColumn3.FieldName = "ID";
			gridColumn3.Name = "ID";       
			gridColumn3.VisibleIndex =0;
			gvMac.Columns.Add(gridColumn3);



			var gridColumn2 = new GridColumn();
			gridColumn2.AppearanceCell.Options.UseTextOptions = true;
			gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
			gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
			gridColumn2.Caption = @"Machine Name";
			gridColumn2.FieldName = "Computer";
			gridColumn2.Name = "Computer";
			gridColumn2.Visible = true;
			gridColumn2.VisibleIndex = 1;
			gridColumn2.SummaryItem.SummaryType = SummaryItemType.Count;
			gridColumn2.SummaryItem.DisplayFormat = @"Total : {0:N0} (item)";
			gvMac.Columns.Add(gridColumn2);



		  


			var gridColumnS = new GridColumn();
			gridColumnS.AppearanceHeader.Options.UseTextOptions = true;
			gridColumnS.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumnS.AppearanceCell.Options.UseTextOptions = true;
			gridColumnS.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumnS.Caption = @"Single Instancee";
			gridColumnS.FieldName = "SingleInstance";
			gridColumnS.Name = "SingleInstance";
			gridColumnS.Visible = true;
			gridColumnS.VisibleIndex = 3;
			gridColumnS.Width = 20;
			gridColumnS.ColumnEdit = chkeditCom;
			gridColumnS.OptionsColumn.AllowSort = DefaultBoolean.False;
			gvMac.Columns.Add(gridColumnS);


			gcMac.ProcessGridKey += gcMac_ProcessGridKey;
			gvMac.RowCountChanged += gvMac_RowCountChanged;
			gvMac.CustomDrawRowIndicator += gvMac_CustomDrawRowIndicator;
			gvMac.RowStyle += gvMac_RowStyle;
			gvMac.CustomDrawCell += gvMac_CustomDrawCell;
			gvMac.RowCellStyle += gvMac_RowCellStyle;
			gvMac.CustomDrawFooter += gvMac_CustomDrawFooter;
			gvMac.CustomDrawFooterCell += gvMac_CustomDrawFooterCell;
			gvMac.CellValueChanged += gvMac_CellValueChanged;
            gvMac.ShowingEditor += gvMac_ShowingEditor;
			ProcessGeneral.HideVisibleColumnsGridView(gvMac, false, "ID");
			gcMac.ForceInitialize();
		  
		}

       

	    private DataTable TableTemplateXml()
		{
			var dtXml = new DataTable();
			dtXml.Columns.Add("ID", typeof(int));
			dtXml.Columns.Add("Computer", typeof(string));
			dtXml.Columns.Add("SingleInstance", typeof(bool));
			return dtXml;
		}
		private void LoadDataGridMachine()
		{
			DataTable dtXml;
			XElement xelement = XElement.Load(DeclareSystem.SysPathConfigExe);
			var query = from p in xelement.Descendants("Machine")	                      
						  let id = int.Parse(p.Attribute("id").Value)
						  select new 
						  { 
							  ID = id,
							  Computer = p.Element("Computer").Value.Trim(),
							  SingleInstance = Convert.ToBoolean(p.Element("SingleInstance").Value)
						  };
			dtXml = query.Count() > 0 ? ProcessGeneral.ConvertToDataTable(query) : TableTemplateXml();
			gcMac.DataSource = dtXml;
			BestFitColumnsGvMac();
		}
		

		private void BestFitColumnsGvMac()
		{
			gvMac.BestFitColumns();
			gvMac.Columns["Computer"].Width += 200;
		}
        private void UpdateXMLFile(int id, string fieldName, object value)
        {
            XDocument documentUpd = XDocument.Load(DeclareSystem.SysPathConfigExe);
            XElement xUpd = (from c in documentUpd.Root.Descendants("Machine")
                              where c.Attribute("id").Value == id.ToString()
                              select c).Single();
            xUpd.SetElementValue(fieldName, value);
             documentUpd.Nodes().OfType<XComment>().Single().Value = string.Format("Config Single Instance ({0:dd/MM/yyyy hh:mm:ss tt})", DateTime.Now);
             documentUpd.Save(DeclareSystem.SysPathConfigExe);
        }
        private int GetMaxIdXmlFile(XDocument document)
        {
            try
            {
                return document.Descendants("Machine").Max(x => (int)x.Attribute("id"));
            }
            catch
            {
                return 0;
            }
        }
		private void gvMac_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
		    var gv = sender as GridView;
            switch (e.Column.FieldName)
            {
                case "Computer":
                    {
                        UpdateXMLFile(ProcessGeneral.GetSafeInt(gv.GetRowCellValue(e.RowHandle,"ID")), "Computer",e.Value.ToString().Trim());
                        BestFitColumnsGvMac();
                    }
                    break;
                case "SingleInstance":
                    {
                        UpdateXMLFile(ProcessGeneral.GetSafeInt(gv.GetRowCellValue(e.RowHandle, "ID")), "SingleInstance",Convert.ToBoolean(e.Value));
                    }
                    break;
            }
            
		
		}
        private void gvMac_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = !PerUpd;
        }
		private void gvMac_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_AdminSys.Properties.Resources.Computer_icon;
			e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
			e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
			e.Handled = true;
		}

		private void gvMac_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var gv = sender as GridView;
			switch (e.Column.FieldName)
			{
				case "SingleInstance":
					if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "SingleInstance")))
					{
						e.Appearance.BackColor = Color.BlanchedAlmond;
						e.Appearance.BackColor2 = Color.Azure;
					}
					break;

			}
	   
		}

		private void gvMac_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
		{
			var rect = new Rectangle(e.Bounds.Location, new Size(100, 25));
			Brush brush = new LinearGradientBrush(rect, Color.FromArgb(209, 227, 241), Color.Azure, 90);
			e.Graphics.FillRectangle(brush, e.Bounds);
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
			//Prevent default painting
			e.Handled = true;
		}
		private void gvMac_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
		{
			if (e.Column.FieldName != "Computer") return;
			if(e.Bounds.Width>0 && e.Bounds.Height>0)
			{
				Brush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(100, Color.Blue), Color.FromArgb(0, 255, 128, 0), LinearGradientMode.Vertical);
				e.Graphics.FillRectangle(brush, e.Bounds);
			}
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.SunkenOuter);
			e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
			e.Appearance.ForeColor = Color.Red;
			e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
			e.Graphics.DrawString(@"   " + e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
			e.Handled = true;
		}

       

		private void gcMac_ProcessGridKey(object sender, KeyEventArgs e)
		{
			var gc = sender as GridControl;
			var gv = gc.FocusedView as GridView;
			if (e.KeyData == Keys.Insert)
			{
                if (!PerIns) return;
                XDocument documentIns = XDocument.Load(DeclareSystem.SysPathConfigExe);
                int maxId = GetMaxIdXmlFile(documentIns);
                documentIns.Element("ConfigInstance").Add(
                 new XElement("Machine", new XAttribute("id", maxId+1),
                       new XElement("Computer", ""),
                        new XElement("SingleInstance", true)
                        )

                );
                documentIns.Save(DeclareSystem.SysPathConfigExe);
                gv.AddNewRow();
                int newRowHandle = gv.FocusedRowHandle;
                gv.SetRowCellValue(newRowHandle, "ID", maxId + 1);
                gv.SetRowCellValue(newRowHandle, "Computer", "");
                gv.SetRowCellValue(newRowHandle, "SingleInstance", true);
                gv.UpdateCurrentRow();
			}
			if (e.KeyData == Keys.Escape)
			{
                if (!PerDel) return;
				if (gv.SelectedRowsCount == 0) return;
				XDocument documentDel = XDocument.Load(DeclareSystem.SysPathConfigExe);
				for (int i = 0; i < gv.SelectedRowsCount; i++)
				{
                    documentDel.Root.Elements().Where(t => t.Attribute("id").Value.Equals(ProcessGeneral.GetSafeString(gv.GetRowCellValue(gv.GetSelectedRows()[i], "ID")))).
						Select(t => t).Single().Remove();
				}

                documentDel.Save(DeclareSystem.SysPathConfigExe);
				gv.DeleteSelectedRows();
				((DataTable)gcMac.DataSource).AcceptChanges();
			}
			if (e.KeyCode == Keys.F5)
			{
				ProcessGeneral.UnCheckSelectedInGridView(gv, "SingleInstance");
			}
		}


		private void gvMac_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			var gv = sender as GridView;
			if (!gv.IsRowSelected(e.RowHandle)) return;
			e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
			e.HighPriority = true;
			e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
			e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
			e.Appearance.GradientMode = LinearGradientMode.Horizontal;
		}

		private void gvMac_RowCountChanged(object sender, EventArgs e)
		{
			var gv = sender as GridView;
			//  if (!gv.GridControl.IsHandleCreated) return;
			Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
			SizeF size = gr.MeasureString((gv.RowCount + 1).ToString(), gv.PaintAppearance.Row.GetFont());
			gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
			//GridPainter.Indicator.ImageSize.Width 
		}

		private void gvMac_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			var gv = sender as GridView;
			if (!e.Info.IsRowIndicator) return;
			if (!gv.IsDataRow(e.RowHandle)) return;
			e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			e.Info.DisplayText = (e.RowHandle + 1).ToString();
			e.Info.ImageIndex = -1;
		}



		#endregion

		#region "Form Event"
	 
		private void FrmConfigSingleInstance_Load(object sender, EventArgs e)
		{
			AllowAdd = true;
			AllowEdit = false;
			AllowDelete = false;
			AllowCancel = false;
			AllowPrint = false;
			AllowGenerate = false;
			AllowBreakDown = false;
			AllowRevision = false;
			AllowRangeSize = false;
			AllowCopyObject = false;
			AllowCheck = false;
			AllowCombine = false;
			AllowFind = false;
			AllowSave = false;
			AllowAdd = PerIns;
			if (!File.Exists(DeclareSystem.SysPathConfigExe))
			{
				try
				{
					var dirEntry = new DirectoryEntry("LDAP://" + DeclareSystem.SysDomainName, DeclareSystem.SysUserInDomain, DeclareSystem.SysPasswordOfUserInDomain, AuthenticationTypes.Secure);
					var dirSearch = new DirectorySearcher(dirEntry) { Filter = "(objectClass=Computer)" };
					DataTable dtXml = TableTemplateXml();
					int i = 0;
					foreach (SearchResult sr in dirSearch.FindAll())
					{
						dtXml.Rows.Add(i + 1, sr.GetDirectoryEntry().Name.Substring(3).Trim(), true);
						i++;
					}
					var documentIsDomain = new XDocument(
										   new XDeclaration("1.0", "utf-8", "yes"),
										   new XComment("Config Single Instance"),
										   new XElement("ConfigInstance",
										   from p in dtXml.AsEnumerable()
										//   orderby p.Field<string>("Computer") 
										   select new XElement("Machine", 
												  new XAttribute("id", p.Field<int>("ID")),
												  new XElement("Computer", p.Field<string>("Computer")),
												  new XElement("SingleInstance", p.Field<bool>("SingleInstance"))
												  )
					));
					documentIsDomain.Save(DeclareSystem.SysPathConfigExe);
					
				}
				catch 
				{

					var document = new XDocument(
								   new XDeclaration("1.0", "utf-8", "yes"),
								   new XComment("Config Single Instance"),
								   new XElement("ConfigInstance",
								   new XElement("Machine", new XAttribute("id", 1),
								   new XElement("Computer", Environment.MachineName.Trim()),
								   new XElement("SingleInstance", true)
						  )));
					document.Save(DeclareSystem.SysPathConfigExe);
				}
			 
			}
			LoadDataGridMachine();

		}
		#endregion

		#region "Overide Button Click Event"
		/// <summary>
		/// Perform when click Refresh button
		/// </summary>
		protected override void PerformRefresh()
		{
		    _dlg = new WaitDialogForm("");
		    FrmConfigSingleInstance_Load(null, null);
		    _dlg.Close();

        }

        /// <summary>
        /// Perform when click Add button
        /// </summary>
        protected override void PerformAdd()
        {
            DialogResult kq = XtraMessageBox.Show("Do you want to geting list machine form domain???", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No) return;
            _dlg = new WaitDialogForm("");
            var dt = new DataTable();
            dt.Columns.Add("Computer", typeof(string));
            var dirEntry = new DirectoryEntry("LDAP://" + DeclareSystem.SysDomainName, DeclareSystem.SysUserInDomain, DeclareSystem.SysPasswordOfUserInDomain, AuthenticationTypes.Secure);
            var dirSearch = new DirectorySearcher(dirEntry) { Filter = "(objectClass=Computer)" };
            foreach (SearchResult sr in dirSearch.FindAll())
            {
                dt.Rows.Add(sr.GetDirectoryEntry().Name.Substring(3).Trim());
            }
            XDocument document = XDocument.Load(DeclareSystem.SysPathConfigExe);
            XElement xelement = XElement.Load(DeclareSystem.SysPathConfigExe);
    
            var queryXml = from p in xelement.Descendants("Machine")
                           select new
                                      {
                                          Computer = p.Element("Computer").Value.ToUpper().Trim()
                                      };

            //get computer name form domain not in xml file
            IEnumerable<DataRow> queryFinal = from c in dt.AsEnumerable()
                                              join b in queryXml
                                              on c.Field<string>("Computer").ToUpper().Trim() equals b.Computer into j
                                              from x in j.DefaultIfEmpty()
                                              where x == null
                                              select c;

            int maxId = GetMaxIdXmlFile(document);
            foreach (var dr in queryFinal)
            {
                maxId++;
                document.Element("ConfigInstance").Add(
            new XElement("Machine", new XAttribute("id", maxId),
                  new XElement("Computer", dr["Computer"].ToString()),
                   new XElement("SingleInstance", true)
                   ));
            }
            document.Save(DeclareSystem.SysPathConfigExe);
            LoadDataGridMachine();
            _dlg.Close();
            XtraMessageBox.Show("Insert Successful List Machine Form Domain into database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
		#endregion
	}
}
