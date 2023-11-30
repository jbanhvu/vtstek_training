using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraEditors.Popup;

namespace CNY_BaseSys.Common
{

    #region "Search Multicolumn"



    //The attribute that points to the registration method
    [UserRepositoryItem("RegisterCustomSearchLookUpEditEdit")]
    public class RepositoryItemCustomSearchLookUpEditEdit : RepositoryItemSearchLookUpEdit
    {

        //The static constructor that calls the registration method
        static RepositoryItemCustomSearchLookUpEditEdit() { RegisterCustomSearchLookUpEditEdit(); }

        //Initialize new properties
        public RepositoryItemCustomSearchLookUpEditEdit()
        {

        }

        //The unique name for the custom editor
        public const string CustomEditName = "CustomSearchLookUpEditEdit";

        //Return the unique name
        public override string EditorTypeName { get { return CustomEditName; } }

        //Register the editor
        public static void RegisterCustomSearchLookUpEditEdit()
        {
            //Icon representing the editor within a container editor's Designer
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(CustomSearchLookUpEditEdit), typeof(RepositoryItemCustomSearchLookUpEditEdit),
                typeof(SearchLookUpEditBaseViewInfo), new ButtonEditPainter(), true, null));
        }
        static readonly object _updateDisplayFilter = new object();

        public SearchEditLookUpPopup LookUpPopupForm { get { return LookUpPopup; } }

        public event UpdateDisplayFilterHandler UpdateDisplayFilter
        {
            add { this.Events.AddHandler(_updateDisplayFilter, value); }
            remove { this.Events.RemoveHandler(_updateDisplayFilter, value); }
        }

        protected internal virtual void RaiseUpdateDisplayFilter(DisplayFilterEventArgs e)
        {
            UpdateDisplayFilterHandler handler = (UpdateDisplayFilterHandler)Events[_updateDisplayFilter];
            if (handler != null) handler(GetEventSender(), e);
        }

        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            RepositoryItemCustomSearchLookUpEditEdit source = item as RepositoryItemCustomSearchLookUpEditEdit;
            Events.AddHandler(_updateDisplayFilter, source.Events[_updateDisplayFilter]);
        }
    }


    public class CustomSearchLookUpEditEdit : SearchLookUpEdit
    {

        //The static constructor that calls the registration method
        static CustomSearchLookUpEditEdit() { RepositoryItemCustomSearchLookUpEditEdit.RegisterCustomSearchLookUpEditEdit(); }

        //Initialize the new instance
        public CustomSearchLookUpEditEdit()
        {
            //...
            Properties.LookUpPopupForm.FindTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(FindTextBox_KeyPress);
        }

        void FindTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = Properties.View;
                if (gridView.RowCount > 0)
                {
                    EditValue = gridView.GetRowCellValue(0, Properties.ValueMember);
                    gridView.FocusedRowHandle = 0;
                    ClosePopup();
                }
            }
        }

        //Return the unique name
        public override string EditorTypeName
        {
            get
            {
                return
                    RepositoryItemCustomSearchLookUpEditEdit.CustomEditName;
            }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomSearchLookUpEditEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEditEdit; }
        }

        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new CustomPopupSearchLookUpEditEditForm(this);
        }

        public event UpdateDisplayFilterHandler UpdateDisplayFilter
        {
            add { this.Properties.UpdateDisplayFilter += value; }
            remove { this.Properties.UpdateDisplayFilter -= value; }
        }

    }

  



    public class CustomPopupSearchLookUpEditEditForm : PopupSearchLookUpEditForm
    {
        public CustomPopupSearchLookUpEditEditForm(SearchLookUpEdit edit) : base(edit) { }

        protected override void UpdateDisplayFilter(string displayFilter)
        {
            DisplayFilterEventArgs args = new DisplayFilterEventArgs(displayFilter);
            Properties.RaiseUpdateDisplayFilter(args);
            base.UpdateDisplayFilter(args.FilterText);
        }

        public new RepositoryItemCustomSearchLookUpEditEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEditEdit; }
        }
    }
    #endregion


    #region "Search By Text"

    public class CustomPopupSearchLookUpEditForm : PopupSearchLookUpEditForm
    {
        public CustomPopupSearchLookUpEditForm(SearchLookUpEdit edit) : base(edit) { }

        protected override void UpdateDisplayFilter(string displayFilter)
        {
            DisplayFilterEventArgs args = new DisplayFilterEventArgs(displayFilter);
            Properties.RaiseUpdateDisplayFilter(args);
            base.UpdateDisplayFilter(args.FilterText);
        }

        public new RepositoryItemCustomSearchLookUpEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEdit; }
        }
    }

    public class CustomSearchLookUpEdit : SearchLookUpEdit
    {
        static CustomSearchLookUpEdit() { RepositoryItemCustomSearchLookUpEdit.RegisterCustomEdit(); }

        public CustomSearchLookUpEdit() { }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemCustomSearchLookUpEdit.CustomEditName;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomSearchLookUpEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEdit; }
        }

        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new CustomPopupSearchLookUpEditForm(this);
        }

        public event UpdateDisplayFilterHandler UpdateDisplayFilter
        {
            add { this.Properties.UpdateDisplayFilter += value; }
            remove { this.Properties.UpdateDisplayFilter -= value; }
        }
    }

    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemCustomSearchLookUpEdit : RepositoryItemSearchLookUpEdit
    {
        static readonly object _updateDisplayFilter = new object();

        static RepositoryItemCustomSearchLookUpEdit() { RegisterCustomEdit(); }

        public RepositoryItemCustomSearchLookUpEdit() { }

        public const string CustomEditName = "CustomSearchLookUpEdit";

        public override string EditorTypeName { get { return CustomEditName; } }

        public static void RegisterCustomEdit()
        {
            Image img = null;
            try
            {
                img = (Bitmap)Bitmap.FromStream(Assembly.GetExecutingAssembly().
                  GetManifestResourceStream("DevExpress.CustomEditors.CustomEdit.bmp"));
            }
            catch
            {
            }
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
              typeof(CustomSearchLookUpEdit), typeof(RepositoryItemCustomSearchLookUpEdit),
              typeof(SearchLookUpEditBaseViewInfo), new ButtonEditPainter(), true, img));
        }

        public event UpdateDisplayFilterHandler UpdateDisplayFilter
        {
            add { this.Events.AddHandler(_updateDisplayFilter, value); }
            remove { this.Events.RemoveHandler(_updateDisplayFilter, value); }
        }

        protected internal virtual void RaiseUpdateDisplayFilter(DisplayFilterEventArgs e)
        {
            UpdateDisplayFilterHandler handler = (UpdateDisplayFilterHandler)Events[_updateDisplayFilter];
            if (handler != null) handler(GetEventSender(), e);
        }

        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            RepositoryItemCustomSearchLookUpEdit source = item as RepositoryItemCustomSearchLookUpEdit;
            Events.AddHandler(_updateDisplayFilter, source.Events[_updateDisplayFilter]);
        }
    }
    #endregion

}
