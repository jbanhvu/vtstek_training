using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Text;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Text.Internal;
using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;

namespace CNY_BaseSys
{
    public class LinkLabelControl : LabelControl
    {
        internal const string StartLinkTag = "<link=", EndLinkTag = "</link>";
        List<LinkBlock> links;
        LinkBlock pressedLink;

        public LinkLabelControl()
            : base()
        {
            links = new List<LinkBlock>();
        }

        internal List<LinkBlock> Links
        {
            get { return links; }
        }

        internal LinkBlock PressedLink
        {
            get { return pressedLink; }
            set
            {
                if (pressedLink != value)
                {
                    pressedLink = value;
                    Refresh();
                }
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (AllowHtmlString)
                    UpdateLinks(value);
                base.Text = value;
            }
        }

        void UpdateLinks(string text)
        {
            links.Clear();
            ParseText(text);
        }

        private void ParseText(string text)
        {
            int startLinkTagIndex = text.IndexOf(StartLinkTag);
            if (startLinkTagIndex == -1) return;
            int endLinkTagIndex = text.IndexOf(EndLinkTag);
            if (endLinkTagIndex == -1) return;
            int index = startLinkTagIndex + StartLinkTag.Length;
            int startBracketIndex = text.IndexOf("<", index);
            int endBracketIndex = text.IndexOf(">", index);
            if (startBracketIndex < endBracketIndex) return;
            string link = text.Substring(index, endBracketIndex - index);
            string linkText = text.Substring(endBracketIndex + 1, endLinkTagIndex - endBracketIndex - 1);
            CreateNewLink(link, linkText, false);
            ParseText(text.Substring(endLinkTagIndex + EndLinkTag.Length));
        }

        private void CreateNewLink(string link, string linkText, bool isVisitedLink)
        {
            LinkBlock bl = new LinkBlock(linkText, link, isVisitedLink);
            links.Add(bl);
        }

        protected override DevExpress.XtraEditors.ViewInfo.BaseStyleControlViewInfo CreateViewInfo()
        {
            return new LinkLabelControlViewInfo(this);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (AllowHtmlString)
                SetCursor(e);
        }

        private void SetCursor(MouseEventArgs e)
        {
            if (GetLink(e) != null)
                Cursor.Current = Cursors.Hand;
            else
                Cursor.Current = Cursors.Default;
        }

        private LinkBlock GetLink(MouseEventArgs e)
        {
            string text = GetLinkText(e);
            if (!text.Equals(string.Empty))
                foreach (LinkBlock bl in Links)
                    if (bl.LinkText == text)
                        return bl;
            return null;
        }

        private string GetLinkText(MouseEventArgs e)
        {
            PropertyInfo pr = ViewInfo.StringInfo.GetType().GetProperty("Blocks", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            List<StringBlock> blocks = ViewInfo.StringInfo.Blocks;
            pr = ViewInfo.StringInfo.GetType().GetProperty("BlocksBounds", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            List<Rectangle> blocksBounds = ViewInfo.StringInfo.BlocksBounds;
            for (int i = 0; i < blocksBounds.Count; i++)
                if (blocksBounds[i].Contains(e.Location))
                    return blocks[i].Text;
            return string.Empty;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (AllowHtmlString)
            {
                LinkBlock bl = GetLink(e);
                if (bl != null)
                    PressedLink = bl;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (PressedLink != null)
            {
                OpenLink();
                PressedLink.IsVisitedLink = true;
                PressedLink = null;
            }
        }

        private void OpenLink()
        {
            try
            {
                Process.Start(PressedLink.Link.Trim());
            }
            catch (Exception exc)
            {
                XtraMessageBox.Show(exc.Message);
            }
        }


        public override Size GetPreferredSize(Size proposedSize)
        {
            LabelAutoSizeMode mode = RealAutoSizeMode;
            if (mode == LabelAutoSizeMode.Horizontal)
            {
                if (proposedSize.Width == 1)
                    proposedSize.Width = 0;
                if (proposedSize.Height == 1)
                    proposedSize.Height = 0;
            }
            else if (mode == LabelAutoSizeMode.Vertical)
            {
                if (proposedSize.Width == int.MaxValue ||
                    proposedSize.Width == 1)
                    proposedSize.Width = Width;
            }
            ViewInfo.UpdatePaintAppearance();
            Size size = ViewInfo.CalcContentSizeByTextSize(ViewInfo.CalcTextSize(ViewInfo.DisplayText, true, mode, proposedSize.Width));
            size.Width += Padding.Left + Padding.Right;
            size.Height += Padding.Top + Padding.Bottom;
            size = ConstrainByMinMax(size);
            if (mode == LabelAutoSizeMode.None) return new Size(proposedSize.Width, size.Height);
            if (mode == LabelAutoSizeMode.Horizontal) return size;
            if (mode == LabelAutoSizeMode.Vertical) return new Size(proposedSize.Width, size.Height);
            return base.GetPreferredSize(size);
        }

        Size ConstrainByMinMax(Size size)
        {
            if (MaximumSize.Width > 0)
                size.Width = Math.Min(MaximumSize.Width, size.Width);
            if (MaximumSize.Height > 0)
                size.Height = Math.Min(MaximumSize.Height, size.Height);
            size.Width = Math.Max(MinimumSize.Width, size.Width);
            size.Height = Math.Max(MinimumSize.Height, size.Height);
            return size;
        }

        protected new LinkLabelControlViewInfo ViewInfo { get { return base.ViewInfo as LinkLabelControlViewInfo; } }
    }

    public class LinkLabelControlViewInfo : LabelControlViewInfo
    {
        public LinkLabelControlViewInfo(BaseStyleControl control) : base(control) { }

        public override string DisplayText
        {
            get
            {
                string text = base.DisplayText;
                if (OwnerControl.Links != null)
                    foreach (LinkBlock bl in OwnerControl.Links)
                    {
                        if (bl.Equals(OwnerControl.PressedLink))
                            text = text.Replace(LinkLabelControl.StartLinkTag + bl.Link + ">", "<u><color=red>");
                        else if (bl.IsVisitedLink)
                            text = text.Replace(LinkLabelControl.StartLinkTag + bl.Link + ">", "<u><color=128,0,128>");
                        else
                            text = text.Replace(LinkLabelControl.StartLinkTag + bl.Link + ">", "<u><color=blue>");
                        text = text.Replace(LinkLabelControl.EndLinkTag, "</color></u>");
                    }
                return text;
            }
        }

        public new LinkLabelControl OwnerControl
        {
            get { return base.OwnerControl as LinkLabelControl; }
        }

        public new Size CalcTextSize(string Text, bool useHotkeyPrefix, LabelAutoSizeMode mode, int predWidth)
        {
            return base.CalcTextSize(Text, useHotkeyPrefix, mode, predWidth);
        }

        protected internal new StringInfo StringInfo {
            get { return base.StringInfo; }
            set { base.StringInfo = value; }
        }
    }

    class LinkBlock
    {
        string linkText, link;
        bool isVisitedLink;

        public LinkBlock(string linkText, string link, bool isVisitedLink)
        {
            this.linkText = linkText;
            this.link = link;
            this.isVisitedLink = isVisitedLink;
        }

        public string LinkText
        {
            get { return linkText; }
        }

        public string Link
        {
            get { return link; }
        }

        public bool IsVisitedLink
        {
            get { return isVisitedLink; }
            set
            {
                if (isVisitedLink != value)
                    isVisitedLink = value;
            }
        }
    }
}
