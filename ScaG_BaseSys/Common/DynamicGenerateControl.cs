using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using System.Security;
using CNY_BaseSys.Class;
using CNY_BaseSys.Info;

namespace CNY_BaseSys.Common
{
    public static class DynamicGenerateControl
    {
        static int MarginLeft = 0;
        static GroupControl groupInfo;
        static List<DataFieldControl> List_ShowOnApp;
        static Boolean Checked = false;
        static int CountComlumn;
        public static List<DataFieldControl> GetListDataFieldControl(DataTable dtStructure)
        {
            List<DataFieldControl> list_dfc = new List<DataFieldControl>();

            for (int i = 0; i < dtStructure.Rows.Count; i++)
            {
                DataColumnCollection columns = dtStructure.Columns;
                DataFieldControl dfc = new DataFieldControl();
                dfc.dfc_id = i;
                dfc.dfc_name = dtStructure.Rows[i]["COLUMN_NAME"].ToString().Trim();
                if (columns.Contains("DATA_TYPE") == true)
                    dfc.data_type = dtStructure.Rows[i]["DATA_TYPE"].ToString().Trim().ToUpper();
                if (columns.Contains("DisplayName") == true)
                    dfc.dfc_displayname = dtStructure.Rows[i]["DisplayName"].ToString().Trim();
                dfc.Editable = ProcessGeneral.GetSafeBool(dtStructure.Rows[i]["Editable"]);
                if (columns.Contains("Nullable") == true)
                    dfc.Nullable = ProcessGeneral.GetSafeBool(dtStructure.Rows[i]["Nullable"]);

                if (columns.Contains("Reference") == true)
                    dfc.reference_column = dtStructure.Rows[i]["Reference"].ToString().Trim();
                else
                    dfc.reference_column = "";
                if (columns.Contains("AutoFillOnInsert") == true)
                    dfc.AutoFillOnInsert = dtStructure.Rows[i]["AutoFillOnInsert"].ToString();

                if (columns.Contains("TextLengthOnApplication") == true)
                    dfc.TextLengthOnApp = ProcessGeneral.GetSafeInt(dtStructure.Rows[i]["TextLengthOnApplication"]);
                else
                    dfc.TextLengthOnApp = 90;
                if (columns.Contains("TextLengthReferenceOnApplication") == true)
                    dfc.TextLengthReferenceOnApp = ProcessGeneral.GetSafeInt(dtStructure.Rows[i]["TextLengthReferenceOnApplication"]);
                else
                    dfc.TextLengthReferenceOnApp = 0;
                if (columns.Contains("TABLE_NAME") == true)
                    dfc.Table_name = ProcessGeneral.GetSafeString(dtStructure.Rows[i]["TABLE_NAME"]);
                else
                    dfc.Table_name = "";
                if (columns.Contains("ShowOnApplication") == true)
                    dfc.ShowOnApp = ProcessGeneral.GetSafeBool(dtStructure.Rows[i]["ShowOnApplication"]);
                else
                    dfc.ShowOnApp = true;
                if (columns.Contains("ShowOnApplicationAE") == true)
                    dfc.ShowOnAppAE = ProcessGeneral.GetSafeBool(dtStructure.Rows[i]["ShowOnApplicationAE"]);
                else
                    dfc.ShowOnAppAE = true;
                if (columns.Contains("CustomPosition") == true)
                    dfc.Position = ProcessGeneral.GetSafeInt(dtStructure.Rows[i]["CustomPosition"]);
                else
                    dfc.Position = 0;
                if (columns.Contains("Updateable") == true)
                    dfc.Updateable = ProcessGeneral.GetSafeBool(dtStructure.Rows[i]["Updateable"]);
                list_dfc.Add(dfc);
            }
            return list_dfc;
        }
        public static void AddControlsToDetailGroup(DataTable dtStructure, GroupControl _groupInfo, int ComlumnOnOneRow, List<DataFieldControl> _List_ShowOnApp)
        {
            #region Add to group control visible
            groupInfo = _groupInfo;
            List_ShowOnApp = _List_ShowOnApp.AsEnumerable().Where(p => p.ShowOnAppAE == true).ToList();
            CountComlumn = ComlumnOnOneRow;
            int RowCount = (List_ShowOnApp.Count / CountComlumn) + (List_ShowOnApp.Count % CountComlumn == 0 ? 0 : 1);
            int Cell = 0;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < CountComlumn; j++)
                {
                    if (Cell < List_ShowOnApp.Count)
                    {
                        List_ShowOnApp[Cell].y = groupInfo.Location.Y + 22 * (i + 1);
                        List_ShowOnApp[Cell].x = GetMarginLeft(dtStructure, j, CountComlumn);
                        AddControlItem(groupInfo, List_ShowOnApp[Cell], MaxLengthLabel(dtStructure, j, CountComlumn));
                        Cell++;
                    }
                }
            }
            groupInfo.Height = 22 * RowCount + 30;
            #endregion

            #region Add to group control unvisible
            List<DataFieldControl> ListUnvisible = new List<DataFieldControl>();
            ListUnvisible = _List_ShowOnApp.AsEnumerable().Where(p => p.ShowOnAppAE == false).ToList();
            foreach (DataFieldControl dfc in ListUnvisible)
            {
                AddControlItemUnvisible(groupInfo, dfc);
            }
            #endregion
        }

        public static void AddControlItem(GroupControl gc, DataFieldControl dfc, int LengthLabel)
        {
            Label lb = new Label();
            lb.Name = "lb" + dfc.dfc_id.ToString();
            lb.Text = dfc.dfc_displayname;
            lb.Location = new Point(dfc.x, dfc.y + 2);
            lb.Width = LengthLabel;
            gc.Controls.Add(lb);

            switch (dfc.data_type)
            {
                case "BIT":
                    ToggleSwitch toggle = new ToggleSwitch();
                    toggle.Name = "txt" + dfc.dfc_name;
                    toggle.Location = new Point(dfc.x + lb.Width, dfc.y);
                    toggle.Width = 90;
                    toggle.Properties.ReadOnly = true;
                    toggle.KeyDown += txt_KeyDown;
                    gc.Controls.Add(toggle);
                    break;
                case "DATETIME":
                    DateEdit date = new DateEdit();
                    date.Name = "txt" + dfc.dfc_name;
                    date.Location = new Point(dfc.x + lb.Width, dfc.y);
                    date.Width = 90;
                    date.Properties.ReadOnly = true;
                    date.KeyDown += txt_KeyDown;
                    gc.Controls.Add(date);
                    break;
                case "IMAGE":
                    PictureEdit date1 = new PictureEdit();
                    date1.Name = "txt" + dfc.dfc_name;
                    date1.Location = new Point(dfc.x + lb.Width, dfc.y);
                    date1.Width = 50;
                    date1.Height = 50;
                    date1.DoubleClick += new EventHandler(pic_DoubleClick);
                    date1.Properties.ReadOnly = true;
                    gc.Controls.Add(date1);
                    break;
                default:
                    TextEdit txt = new TextEdit();
                    txt.Name = "txt" + dfc.dfc_name;
                    txt.Location = new Point(dfc.x + lb.Width, dfc.y);
                    //txt.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    //txt.Properties.Appearance.ForeColor = Color.Black;
                    txt.Properties.ReadOnly = true;
                    //Check if type is number
                    string type = dfc.data_type;
                    string type1 = "";
                    if (type.IndexOf("(") >= 0)
                    {
                        type1 = type.Substring(0, type.IndexOf("("));
                    }
                    if (type == "FLOAT" || type == "INT" || type == "BIGINT")
                    {
                        txt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    }

                    else if (type1 == "NVARCHAR" || type1 == "VARCHAR" || type1 == "NCHAR")
                    {
                        string s = type.Substring(type.IndexOf("(") + 1, type.Length - 1 - type.IndexOf("(") - 1);
                        int MaxLength = ProcessGeneral.GetSafeInt(s);
                        txt.Properties.MaxLength = MaxLength;
                    }
                    if (dfc.reference_column == "")
                    {
                        txt.Width = dfc.TextLengthOnApp;
                        MarginLeft = txt.Location.X + txt.Width;
                    }
                    else
                    {
                        txt.Width = dfc.TextLengthReferenceOnApp;
                        TextEdit txtDesc = new TextEdit();
                        txtDesc.Name = "txt" + dfc.dfc_name + "Desc";
                        txtDesc.Location = new Point(dfc.x + lb.Width + txt.Width + 2, dfc.y);
                        txtDesc.Width = dfc.TextLengthOnApp - 2 - txt.Width;
                        //txtDesc.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        //txtDesc.Properties.Appearance.ForeColor = Color.Black;
                        txtDesc.Properties.ReadOnly = true;
                        txt.Leave += Txt_Leave;
                        gc.Controls.Add(txtDesc);

                    }

                    txt.KeyDown += txt_KeyDown;
                    gc.Controls.Add(txt);
                    break;
            }
        }
        public static void AddControlItemUnvisible(GroupControl gc, DataFieldControl dfc)
        {
            Control txt;
            switch (dfc.data_type)
            {

                case "BIT":
                    txt = new ToggleSwitch();
                    break;
                case "DATETIME":
                    txt = new DateEdit();

                    break;
                case "IMAGE":
                    txt = new PictureEdit();

                    break;
                default:
                    txt = new TextEdit();

                    break;
            }
            txt.Name = "txt" + dfc.dfc_name;
            txt.Location = new Point(0, 0);
            txt.Visible = false;
            gc.Controls.Add(txt);
        }
        static void pic_DoubleClick(object sender, EventArgs e)
        {
            PictureEdit picbox = sender as PictureEdit;


            var opf = new OpenFileDialog()
            {
                Title = @"Open File Image",
                Filter = @"Image File (.JPG,.PNG)|*.jpg;*.png",
                RestoreDirectory = true,
                Multiselect = true,
            }; if (opf.ShowDialog() != DialogResult.OK)
                return;
            foreach (String file in opf.FileNames)
            {
                try
                {
                    Image loadedImage = Image.FromFile(file);
                    picbox.Image = loadedImage;

                }
                catch (SecurityException ex)
                {
                    // The user lacks appropriate permissions to read files, discover paths, etc.
                    MessageBox.Show(@"Security error. Please contact your administrator for details.\n\n" +
                        @"Error message: " + ex.Message + "\n\n" +
                        @"Details (send to Support):\n\n" + ex.StackTrace
                    );
                }
                catch (Exception ex)
                {
                    // Could not load the image - probably related to Windows file system permissions.
                    MessageBox.Show(@"Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                        + @". You may not have permission to read the file, or " +
                        @"it may be corrupt.\n\nReported error: " + ex.Message);
                }
            }
        }

        private static void Txt_Leave(object sender, EventArgs e)
        {
            if (Checked)
                return;
            Checked = true;

            TextEdit txt = sender as TextEdit;
            TextEdit txtDesc = new TextEdit();
            if (txt.Text.Trim() == "")
                return;
            foreach (DataFieldControl dfc1 in List_ShowOnApp)
            {
                if ("txt" + dfc1.dfc_name == txt.Name)
                {
                    string[] Reference = dfc1.reference_column.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    string ColumnReference = Reference[0];
                    string TableReferenceName = Reference[1];
                    string ColumnReferenceName = Reference[2];
                    TextEdit control1 = groupInfo.Controls.Find(txt.Name + "Desc", true).FirstOrDefault() as TextEdit;
                    Inf_Progress inf = new Inf_Progress();
                    string SQL = "SELECT " + ColumnReference + "," + ColumnReferenceName + " FROM " + TableReferenceName;
                    SQL += " Where " + ColumnReference + "='" + txt.Text + "'";
                    DataTable dt = inf.TblExcuteSqlText(SQL);
                    if (dt.Rows.Count <= 0)
                    {
                        XtraMessageBox.Show("Not found " + txt.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        txt.Focus();

                        return;
                    }
                    else
                    {
                        control1.Text = ProcessGeneral.GetSafeString(dt.Rows[0][ColumnReferenceName]);
                    }
                }
            }
        }

        public static int GetMarginLeft(DataTable dtStructure, int ColumnIndex, int CountComlumn)
        {
            int Margin = 3;
            for (int i = 0; i < ColumnIndex; i++)
            {
                int LengthLabel = MaxLengthLabel(dtStructure, i, CountComlumn);
                int LengthTextBox = MaxLengthTextBox(dtStructure, i, CountComlumn);
                Margin += 22 + LengthLabel + LengthTextBox;
            }
            return Margin;
        }
        public static int MaxLengthLabel(DataTable dtStructure, int ColumnIndex, int CountComlumn)
        {
            int Max = 0;
            if (ColumnIndex > dtStructure.Rows.Count)
            {
                return Max;
            }

            for (int i = ColumnIndex; i < dtStructure.Rows.Count; i += CountComlumn)
            {
                string DisplayName = dtStructure.Rows[i]["DisplayName"].ToString();
                if (DisplayName.Length > Max)
                {
                    Max = DisplayName.Length;
                }
            }
            return Max * 5 + 15;
        }
        public static int MaxLengthTextBox(DataTable dtStructure, int ColumnIndex, int CountComlumn)
        {
            int Max = 0;
            DataColumnCollection columns = dtStructure.Columns;
            if (columns.Contains("TextLengthOnApplication") == true)
            {
                for (int i = ColumnIndex; i < dtStructure.Rows.Count; i += CountComlumn)
                {
                    int LengthOnApplication = ProcessGeneral.GetSafeInt(dtStructure.Rows[i]["TextLengthOnApplication"]);
                    if (LengthOnApplication > Max)
                    {
                        Max = LengthOnApplication;
                    }
                }
            }
            else
                return 90;
            return Max;
        }
        static void txt_KeyDown(object sender, KeyEventArgs e)
        {
            Checked = false;
            TextEdit txt = sender as TextEdit;

            #region Press F4
            if (e.KeyCode == Keys.F4)
            {
                TextEdit txtDesc = new TextEdit();
                foreach (DataFieldControl dfc1 in List_ShowOnApp)
                {
                    if ("txt" + dfc1.dfc_name == txt.Name)
                    {
                        if (string.IsNullOrEmpty(dfc1.reference_column))
                            return;
                        string[] Reference = dfc1.reference_column.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                        string ColumnReference = Reference[0];
                        string TableReferenceName = Reference[1];
                        string ColumnReferenceName = Reference[2];
                        TextEdit control1 = groupInfo.Controls.Find(txt.Name + "Desc", true).FirstOrDefault() as TextEdit;
                        Inf_Progress inf = new Inf_Progress();
                        if (control1 != null)
                        {
                            string SQL = "SELECT DISTINCT " + ColumnReference + "," + ColumnReferenceName + " FROM " + TableReferenceName;
                            if (Reference.Length > 3)
                            {
                                string Condition = Reference[3];
                                SQL += " WHERE " + Condition;
                            }
                            var lG = new List<GridViewTransferDataColumnInit>
                            {

                            };
                            var f = new FrmTransferData
                            {
                                DtSource = inf.TblExcuteSqlText(SQL),
                                ListGvColFormat = lG,
                                MinimizeBox = false,
                                MaximizeBox = false,
                                FormBorderStyle = FormBorderStyle.FixedSingle,
                                Size = new Size(400, 300),
                                StartPosition = FormStartPosition.CenterScreen,
                                WindowState = FormWindowState.Normal,
                                Text = ColumnReference,
                                StrFilter = txt.Text,
                                IsShowAutoFilterRow = true,
                                IsShowFindPanel = false,
                                IsShowFooter = false,
                            };

                            f.OnTransferData += (s1, e1) =>
                            {
                                List<DataRow> lDr = e1.ReturnRowsSelected;
                                txt.Text = ProcessGeneral.GetStringPkDataTransferForm(lDr, ColumnReference, ",", true);
                                control1.Text = ProcessGeneral.GetStringPkDataTransferForm(lDr, ColumnReferenceName, ",", true);
                            };
                            f.ShowDialog();
                        }
                    }
                }
            }
            #endregion

            #region Enter
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                List<DataFieldControl> List_ShowOnAppInput = List_ShowOnApp.AsEnumerable().Where(p => p.dfc_name.Length > 4).ToList();
                List_ShowOnAppInput = List_ShowOnAppInput.AsEnumerable().Where(p => p.dfc_name.Substring(p.dfc_name.Length - 4, 4) != "Desc").ToList();

                for (int i = 0; i < List_ShowOnApp.Count; i++)
                {
                    if (txt.Name == "txt" + List_ShowOnApp[i].dfc_name)
                    {
                        try
                        {
                            Control input = GetUnderLineControl(List_ShowOnApp, i);
                            input.Focus();
                        }
                        catch
                        {
                        }

                    }
                }
            }
            #endregion

            #region Esc
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                List<DataFieldControl> List_ShowOnAppInput = List_ShowOnApp.AsEnumerable().Where(p => p.dfc_name.Substring(p.dfc_name.Length - 4, 4) != "Desc" && p.Editable == true).ToList();
                for (int i = 0; i < List_ShowOnAppInput.Count; i++)
                {
                    if (txt.Name == "txt" + List_ShowOnAppInput[i].dfc_name)
                    {
                        try
                        {
                            TextEdit input = groupInfo.Controls.Find("txt" + List_ShowOnAppInput[i - CountComlumn].dfc_name, true).FirstOrDefault() as TextEdit;
                            input.Focus();
                        }
                        catch
                        {
                        }

                    }
                }
            }
            #endregion


        }
        public static Control GetUnderLineControl(List<DataFieldControl> List_ShowOnApp, int i)
        {
            if (i + CountComlumn > List_ShowOnApp.Count - 1)
            {
                i = (i + CountComlumn) % CountComlumn + 1;
                return GetUnderLineControl(List_ShowOnApp, i - CountComlumn);
            }

            if (List_ShowOnApp[i + CountComlumn].Editable)
            {
                Control input = groupInfo.Controls.Find("txt" + List_ShowOnApp[i + CountComlumn].dfc_name, true).FirstOrDefault() as Control;
                return input;
            }
            else
            {
                return GetUnderLineControl(List_ShowOnApp, i + CountComlumn);
            }
        }
        public static void ReadOnlyControl(GroupControl _groupInfo, List<DataFieldControl> _List_ShowOnApp, Boolean Enable)
        {
            foreach (DataFieldControl dfc in _List_ShowOnApp)
            {
                if (dfc.Editable == true)
                {
                    if (dfc.data_type == "BIT")
                    {
                        //CheckBox input1 = _groupInfo.Controls.Find("txt" + dfc.dfc_name, true).FirstOrDefault() as CheckBox;
                        //input1.Enabled = Enable;
                    }
                    else
                    {
                        //Finding control to enable
                        TextEdit input = _groupInfo.Controls.Find("txt" + dfc.dfc_name, true).FirstOrDefault() as TextEdit;
                        input.Properties.ReadOnly = Enable;
                        //input.Enabled = !Enable;
                    }
                }
            }
        }
        public static void FillDataControlOnInsert(GroupControl _groupInfo, List<DataFieldControl> _List_ShowOnApp)
        {
            foreach (DataFieldControl dfc in _List_ShowOnApp)
            {
                if (dfc.AutoFillOnInsert != "")
                {
                    TextEdit input = _groupInfo.Controls.Find("txt" + dfc.dfc_name, true).FirstOrDefault() as TextEdit;
                    input.Text = dfc.AutoFillOnInsert;
                }

            }
        }
        public static void FillDataControlOnUpdate(GroupControl _groupInfo, List<DataFieldControl> _List_ShowOnApp, DataTable dt)
        {
            foreach (DataFieldControl dfc1 in _List_ShowOnApp)
            {

                Control control1 = _groupInfo.Controls.Find("txt" + dfc1.dfc_name, true).FirstOrDefault() as Control;
                if (control1 != null)
                {
                    string dataType = dfc1.data_type;
                    FillValueToControl(control1, dataType, dt.Rows[0][dfc1.dfc_name]);
                    if (dfc1.reference_column != "")
                    {
                        string[] reference = dfc1.reference_column.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                        string columnReference = reference[0];
                        string tableReferenceName = reference[1];
                        string columnReferenceName = reference[2];

                        string sql = "SELECT DISTINCT " + columnReference + "," + columnReferenceName + " FROM " + tableReferenceName + " where "
                            + columnReference + " =  " + "'" + (dfc1.dfc_name == "CNYMF012PK" ? ProcessGeneral.GetCodeInString(dt.Rows[0][dfc1.dfc_name].ToString().Trim()) : dt.Rows[0][dfc1.dfc_name].ToString().Trim()) + "'";
                        Inf_Progress inf = new Inf_Progress();
                        var tba = inf.TblExcuteSqlText(sql);

                        Control control2 = _groupInfo.Controls.Find("txt" + dfc1.dfc_name + "Desc", true).FirstOrDefault() as Control;

                        if (control2 != null)
                        {
                            try
                            {

                                FillValueToControl(control2, "nvarchar",
                                    tba.Rows.Count > 0 ? tba.Rows[0][columnReferenceName] : "");
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
        }
        public static void FillValueToControl(Control control1, string DataType, object Value)
        {
            try
            {

                switch (DataType)
                {
                    case "INT":
                        control1.Text = ProcessGeneral.GetSafeString(Value);
                        break;
                    case "BIT":
                        string value = ProcessGeneral.GetSafeString(Value);
                        ToggleSwitch tg = control1 as ToggleSwitch;
                        tg.IsOn = value == "True" ? true : false;
                        break;
                    case "DATETIME":
                        DateEdit date = control1 as DateEdit;
                        date.EditValue = ProcessGeneral.GetSafeDatetime(Value);
                        ProcessGeneral.SetDateEditFormat(date, @"dd\/MM\/yyyy", false, true, DefaultBoolean.Default);
                        break;
                    case "IMAGE":
                        object photoValue = ProcessGeneral.GetSafeString(Value);
                        PictureEdit pic = control1 as PictureEdit;

                        if (!Convert.IsDBNull(photoValue) && !String.IsNullOrEmpty(photoValue.ToString()))
                        {
                            pic.Image = ProcessGeneral.ConvertByteArrayToImage((byte[])Value);
                            //set image property of the picture box by creating a image from stream 

                            pic.Refresh(); //refresh picture box
                        }
                        else
                        {
                            pic.Image = null;
                        }
                        break;
                    default:
                        if (control1.Name == "txtCNYMF012PK")
                        {
                            control1.Text = ProcessGeneral.GetCodeInString(Value.ToString());
                        }
                        else
                        {
                            control1.Text = ProcessGeneral.GetSafeString(Value);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public static Boolean CheckInput(GroupControl _groupInfo, List<DataFieldControl> _List_ShowOnApp)
        {
            foreach (DataFieldControl dfc in _List_ShowOnApp)
            {
                if (dfc.Nullable == false)
                {
                    //Finding control to check null
                    TextEdit input = _groupInfo.Controls.Find("txt" + dfc.dfc_name, true).FirstOrDefault() as TextEdit;
                    if (input != null)
                    {
                        if (string.IsNullOrEmpty(input.Text))
                        {
                            XtraMessageBox.Show(dfc.dfc_displayname + " is not null!", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            input.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void SetValueFromControl(GroupControl _groupInfo, List<DataFieldControl> _List_ShowOnApp)
        {
            foreach (DataFieldControl dfc in _List_ShowOnApp)
            {
                //Finding control to get value
                Control input = _groupInfo.Controls.Find("txt" + dfc.dfc_name, true).FirstOrDefault() as Control;
                if (input != null)
                {
                    switch (dfc.data_type)
                    {
                        case "int":
                            TextEdit text = input as TextEdit;
                            dfc.value = ProcessGeneral.GetSafeInt(text.EditValue);
                            break;
                        case "datetime":
                            DateEdit date = input as DateEdit;
                            dfc.value = ProcessGeneral.GetSafeDatetime(date.EditValue);
                            break;
                        case "bit":
                            ToggleSwitch tg = input as ToggleSwitch;
                            dfc.value = tg.IsOn = true ? true : false;
                            break;
                        default:
                            TextEdit text1 = input as TextEdit;
                            dfc.value = ProcessGeneral.GetSafeString(text1.EditValue);
                            break;
                    }
                }
            }
        }

        public static void SetValueFromControl(GridControl gc, int RowID, List<DataFieldControl> _List)
        {
            DataTable dt = gc.DataSource as DataTable;
            foreach (DataFieldControl dfc in _List)
            {
                //Finding control to get value
                DataColumnCollection columns = dt.Columns;

                if (columns.Contains(dfc.dfc_name))
                {
                    switch (dfc.data_type)
                    {
                        case "int":
                            dfc.value = ProcessGeneral.GetSafeInt(dt.Rows[RowID][dfc.dfc_name]);
                            break;
                        case "datetime":
                            dfc.value = ProcessGeneral.GetSafeDatetime(dt.Rows[RowID][dfc.dfc_name]);
                            break;
                        case "bit":
                            dfc.value = ProcessGeneral.GetSafeInt(dt.Rows[RowID][dfc.dfc_name]) == 1 ? true : false;
                            break;
                        default:
                            dfc.value = ProcessGeneral.GetSafeString(dt.Rows[RowID][dfc.dfc_name]);
                            break;
                    }
                }
            }
        }

        #region Code Definition
        public static void SetPropertiesTextBox(DataTable dt)
        {

            TextEdit control1 = groupInfo.Controls.Find("txtCNY001", true).FirstOrDefault() as TextEdit;
            if (control1 != null && dt.Rows.Count > 0)
            {
                control1.Properties.MaxLength = ProcessGeneral.GetSafeInt(dt.Rows[0]["CharLength"]);
            }
        }
        public static void SetPropertiesTextBox(DataTable dt, string txtName)
        {

            TextEdit txt = groupInfo.Controls.Find(txtName, true).FirstOrDefault() as TextEdit;
            if (txt == null)
                return;
            List<string> RegEx = new List<string>();
            string RegEx1 = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Boolean IsNumberic = ProcessGeneral.GetSafeBool(dt.Rows[i]["IsNumeric"]);
                Boolean Active = ProcessGeneral.GetSafeBool(dt.Rows[i]["Active"]);
                int CharLength = ProcessGeneral.GetSafeInt(dt.Rows[i]["CharLength"]);
                int InputType = ProcessGeneral.GetSafeInt(dt.Rows[i]["InputType"]);
                if (!Active)
                {
                    continue;
                }
                switch (InputType)
                {
                    case 0:
                        if (IsNumberic)
                        {
                            RegEx.Add("[0-9]{" + CharLength + "}");

                        }
                        else
                        {
                            RegEx.Add("[^aeiou]{" + CharLength + "}");
                        }
                        RegEx1 += RegEx[i];
                        break;
                    case 2:
                        if (IsNumberic)
                        {
                            RegEx.Add("[0-9]{" + CharLength + "}");
                        }
                        else
                        {
                            RegEx.Add("[^aeiou]{" + CharLength + "}");
                        }
                        RegEx1 += RegEx[i];
                        break;
                }
            }
            txt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            txt.Properties.Mask.EditMask = RegEx1;
        }

        #endregion
    }
}
