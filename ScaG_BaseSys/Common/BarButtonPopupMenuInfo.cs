using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CNY_BaseSys.Common
{
    public class BarButtonPopupMenuInfo
    {
        public Int32 Index { get; set; }
        public String Name { get; set; }
        public String Caption { get; set; }
        public Int32 ImageIndex { get; set; }
    }
    public class PopupMenuRibbonItemInfo
    {
        private string _itemName = "";
        public string ItemName
        {
            get { return this._itemName; }
            set { _itemName = value; }
        }
        private string _itemCaption = "";
        public string ItemCaption
        {
            get { return this._itemCaption; }
            set { _itemCaption = value; }
        }
        private Image _itemImage = null;
        public Image ItemImage
        {
            get { return this._itemImage; }
            set
            {
                _itemImage = value;
            }
        }
        private bool _beginGroup = false;
        public bool BeginGroup
        {
            get { return this._beginGroup; }
            set { _beginGroup = value; }
        }







        public PopupMenuRibbonItemInfo()
        {

        }

        public PopupMenuRibbonItemInfo(string itemName, string itemCaption, bool beginGroup, Image itemImage)
        {
            _itemName = itemName;
            _itemCaption = itemCaption;
            _beginGroup = beginGroup;
            _itemImage = itemImage;
        }
    }
}
