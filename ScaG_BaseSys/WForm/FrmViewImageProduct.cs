using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;

namespace CNY_BaseSys.WForm
{
    public partial class FrmViewImageProduct : DevExpress.XtraEditors.XtraForm
    {
        #region "Property & File"
        WaitDialogForm _dlg;
        private readonly Inf_General _inf = new Inf_General();


        private readonly Int64 _pkCode;





        #endregion



        #region "Contructor & Event"
        public FrmViewImageProduct(Int64 pkCode)
        {
            InitializeComponent();
            _pkCode = pkCode;
            picProduct.Properties.ShowMenu = false;
            picProduct.Properties.SizeMode = PictureSizeMode.Squeeze;
            InitTileView();
            gcImage.DataSource = TableImageTempData();
            this.Load += FrmViewImageProduct_Load;
        }

        private void FrmViewImageProduct_Load(object sender, EventArgs e)
        {
            LoadGridImage();
        }



        #endregion



        #region "Process Tile View"


        private void InitTileView()
        {
            // gcImage.UseEmbeddedNavigator = true;
            //gcImage.EmbeddedNavigator.Buttons.Next.Visible = true;
            //gcImage.EmbeddedNavigator.Buttons.Prev.Visible = true;

            tvImage.OptionsImageLoad.AnimationType = ImageContentAnimationType.SegmentedFade;
            tvImage.OptionsImageLoad.AsyncLoad = true;
            tvImage.OptionsImageLoad.CacheThumbnails = false;
            tvImage.ColumnSet.BackgroundImageColumn = tvImage.Columns[0];
            tvImage.OptionsImageLoad.LoadThumbnailImagesFromDataSource = false;
            tvImage.OptionsImageLoad.DesiredThumbnailSize = new Size(35, 35);


            tvImage.CustomColumnDisplayText += tvImage_CustomColumnDisplayText;
            tvImage.GetThumbnailImage += tvImage_GetThumbnailImage;

            tvImage.FocusedRowChanged += tvImage_FocusedRowChanged;
            tvImage.FocusedColumnChanged += tvImage_FocusedColumnChanged;
        }

        private DataTable TableImageTempData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(Int64));
            dt.Columns.Add("ImageThumb", typeof(byte[]));

            //for (int i = 1; i <= 5; i++)
            //{

            //    DataRow dr = dt.NewRow(); dr["Id"] = i;
            //    Image img = Image.FromFile(_path + string.Format(@"\Images\{0}.jpg", i));
            //    byte[] byteImg = ConvertImageToByteArray(img, ImageFormat.Jpeg);
            //    dr["ImageThumb"] = byteImg;
            //    dt.Rows.Add(dr);
            //}
            return dt;
        }
        private void DisplayImageOnPictureBox(TileView tView, int rhLoad = -1)
        {
            if (tView.RowCount <= 0)
            {
                picProduct.Image = null;
                return;
            }
            int rHnew = rhLoad == -1 ? tView.FocusedRowHandle : rhLoad;

            Byte[] byteImg = tView.GetRowCellValue(rHnew, "ImageThumb") as byte[];
            if (byteImg == null)
            {
                picProduct.Image = null;
            }
            else
            {
                var memStream = new MemoryStream(byteImg); //Creating an image from the stream 
                Image thumbImg = Image.FromStream(memStream);
                picProduct.Image = thumbImg;
            }
        }


        private void tvImage_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            TileView tV = sender as TileView;
            if (tV == null) return;
            DisplayImageOnPictureBox(tV);
        }

        private void tvImage_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            TileView tV = sender as TileView;
            if (tV == null) return;
            DisplayImageOnPictureBox(tV);
        }






        private void tvImage_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            e.DisplayText = "";
        }



        private void tvImage_GetThumbnailImage(object sender, DevExpress.Utils.ThumbnailImageEventArgs e)
        {
            TileView tV = sender as TileView;
            if (tV == null) return;
            int rH = tV.GetRowHandle(e.DataSourceIndex);
            DataRow dr = tV.GetDataRow(rH);
            Byte[] byteImg = dr["ImageThumb"] as byte[];
            if (byteImg == null) return;
            var memStream = new MemoryStream(byteImg);
            Image thumbImg = Image.FromStream(memStream);
            e.ThumbnailImage = e.CreateThumbnailImage(thumbImg, new Size(109, 109));


        }


        #endregion



        #region "Load Data"



        public void LoadGridImage()
        {

            _dlg = new WaitDialogForm();

            DataTable dt = gcImage.DataSource as DataTable;
            if (dt == null) return;
            dt.Rows.Clear();
            dt.AcceptChanges();



            DataTable dtImage = _inf.RMCode_LoadImage(_pkCode);

            bool isAddImage = false;
            foreach (DataRow drImage in dtImage.Rows)
            {
                isAddImage = true;
                DataRow dr = dt.NewRow();
                dr["Id"] = drImage["PK"];
                dr["ImageThumb"] = drImage["ImageCode"];
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();


            if (isAddImage)
            {
                tvImage.SelectRow(0);
                tvImage.FocusedRowHandle = 0;
                DisplayImageOnPictureBox(tvImage);
            }

            _dlg.Close();
        }


        #endregion






    }
}
