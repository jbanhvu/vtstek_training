using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using System.Drawing;

namespace CNY_BaseSys.Common
{
    public class UserLookUpEdit : LookUpEdit
    {

        static UserLookUpEdit()
        {
            RepositoryItemUserLookUpEdit.Register();
        }

        public UserLookUpEdit()
        {
        }

        public override string EditorTypeName { get { return "UserLookUpEdit"; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemUserLookUpEdit Properties
        {
            get { return base.Properties as RepositoryItemUserLookUpEdit; }
        }
        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new UserPopupLookUpEditForm(this);
        }
    }

    [UserRepositoryItem("Register")]
    public class RepositoryItemUserLookUpEdit : RepositoryItemLookUpEdit
    {
        static RepositoryItemUserLookUpEdit()
        {
            Register();
        }
        public RepositoryItemUserLookUpEdit() { }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo("UserLookUpEdit", typeof(UserLookUpEdit),
                typeof(RepositoryItemUserLookUpEdit), typeof(DevExpress.XtraEditors.ViewInfo.LookUpEditViewInfo),
                new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true));
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string EditorTypeName { get { return "UserLookUpEdit"; } }
    }

    public class UserPopupLookUpEditForm : PopupLookUpEditForm
    {
      
        public UserPopupLookUpEditForm(LookUpEdit owner)
            : base(owner)
        {
            fCloseButtonStyle = BlobCloseButtonStyle.None;
 
        }
    }
}
