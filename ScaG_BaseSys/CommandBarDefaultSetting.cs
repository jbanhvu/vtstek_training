using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CNY_BaseSys
{
    public static class CommandBarDefaultSetting
    {
        #region "Set Default Image"
        public static readonly Image ImageButtonReject = CNY_BaseSys.Properties.Resources.System_Reject_32x32_new;
        public static readonly Image ImageButtonConfirm = CNY_BaseSys.Properties.Resources.System_Confirm_32x32_new;


        public static readonly Image ImageButtonAdd = CNY_BaseSys.Properties.Resources.System_Add_32x32;
        public static readonly Image ImageButtonEdit = CNY_BaseSys.Properties.Resources.System_Edit_32x32;
        public static readonly Image ImageButtonDelete = CNY_BaseSys.Properties.Resources.System_Delete_32x32_New;
        public static readonly Image ImageButtonSave = CNY_BaseSys.Properties.Resources.System_Save_32x32;
        public static readonly Image ImageButtonRevision = CNY_BaseSys.Properties.Resources.System_Revision_32x32;
        public static readonly Image ImageButtonRefresh = CNY_BaseSys.Properties.Resources.System_Refresh_32x32;
        public static readonly Image ImageButtonRangeSize = CNY_BaseSys.Properties.Resources.System_RangeSize_32x32;
        public static readonly Image ImageButtonPrint = CNY_BaseSys.Properties.Resources.System_Print_32x32;
        public static readonly Image ImageButtonPrintDown = CNY_BaseSys.Properties.Resources.printernetwork_32x32;
        public static readonly Image ImageButtonGenerate = CNY_BaseSys.Properties.Resources.System_Generate_32x32;
        public static readonly Image ImageButtonFind = CNY_BaseSys.Properties.Resources.System_Find_32x32;
        public static readonly Image ImageButtonExpand = CNY_BaseSys.Properties.Resources.System_Expand_32x32;
        public static readonly Image ImageButtonCopy = CNY_BaseSys.Properties.Resources.System_Copy_32x32;
        public static readonly Image ImageButtonBreakDown = CNY_BaseSys.Properties.Resources.System_BreakDown_32x32;
        public static readonly Image ImageButtonCancel = CNY_BaseSys.Properties.Resources.System_Cancel_32x32;
        public static readonly Image ImageButtonCheck = CNY_BaseSys.Properties.Resources.System_Check_32x32;
        public static readonly Image ImageButtonClose = CNY_BaseSys.Properties.Resources.System_Close_32x32;
        public static readonly Image ImageButtonCollosepand = CNY_BaseSys.Properties.Resources.System_Collosepand_32x32;
        public static readonly Image ImageButtonCombine = CNY_BaseSys.Properties.Resources.System_Combine_32x32;

        public static readonly Image ImageButtonFunction = CNY_BaseSys.Properties.Resources.inserttableofequations_32x32;
        public static readonly Image ImageButtonFunctionDown = CNY_BaseSys.Properties.Resources.inserttableofequations_16x16;
        #endregion

        #region "Set Default Text"
        public const string CaptionButtonConfirmDefaultCommamd = "Confirm";
        public const string CaptionButtonRejectDefaultCommamd = "Reject";

        public const string CaptionButtonAddDefaultCommamd = "New";
        public const string CaptionButtonEditDefaultCommamd = "Edit";
        public const string CaptionButtonDeleteDefaultCommamd = "Delete";
        public const string CaptionButtonSaveDefaultCommamd = "Save";
        public const string CaptionButtonRevisionDefaultCommamd = "Revision";
        public const string CaptionButtonRefreshDefaultCommamd = "Refresh";
        public const string CaptionButtonRangeSizeDefaultCommamd = "RangeSize";
        public const string CaptionButtonPrintDefaultCommamd = "Print";
        public const string CaptionButtonPrintDownDefaultCommamd = "Print";
        public const string CaptionButtonGenerateDefaultCommamd = "Generate";
        public const string CaptionButtonFindDefaultCommamd = "Find";
        public const string CaptionButtonExpandDefaultCommamd = "Expand";
        public const string CaptionButtonCopyDefaultCommamd = "Copy";
        public const string CaptionButtonBreakDownDefaultCommamd = "BreakDown";
        public const string CaptionButtonCancelDefaultCommamd = "Cancel";
        public const string CaptionButtonCheckDefaultCommamd = "Check";
        public const string CaptionButtonCloseDefaultCommamd = "Close";
        public const string CaptionButtonCollosepandDefaultCommamd = "Collapse";
        public const string CaptionButtonCombineDefaultCommamd = "Combine";
        public const string CaptionButtonFunctionDefaultCommamd = "Fn";

        public const string CaptionButtonbbiGFN1_F1 = "bbiGFN1_F1";
        public const string CaptionButtonbbiGFN1_F2 = "bbiGFN1_F2";
        public const string CaptionButtonbbiGFN1_F3 = "bbiGFN1_F3";
        public const string CaptionButtonbbiGFN1_F4 = "bbiGFN1_F4";
        public const string CaptionButtonbbiGFN1_F5 = "bbiGFN1_F5";
        public const string CaptionButtonbbiGFN1_F6 = "bbiGFN1_F6";
        //-------------------------------------------------------------------------------------------------
        public const string CaptionButtonbbiGFN2_F1 = "bbiGFN2_F1";
        public const string CaptionButtonbbiGFN2_F2 = "bbiGFN2_F2";
        public const string CaptionButtonbbiGFN2_F3 = "bbiGFN2_F3";
        public const string CaptionButtonbbiGFN2_F4 = "bbiGFN2_F4";
        public const string CaptionButtonbbiGFN2_F5 = "bbiGFN2_F5";
        public const string CaptionButtonbbiGFN2_F6 = "bbiGFN2_F6";
        //-------------------------------------------------------------------------------------------------
        public const string CaptionButtonbbiGFN3_F1 = "bbiGFN3_F1";
        public const string CaptionButtonbbiGFN3_F2 = "bbiGFN3_F2";
        public const string CaptionButtonbbiGFN3_F3 = "bbiGFN3_F3";
        public const string CaptionButtonbbiGFN3_F4 = "bbiGFN3_F4";
        public const string CaptionButtonbbiGFN3_F5 = "bbiGFN3_F5";
        public const string CaptionButtonbbiGFN3_F6 = "bbiGFN3_F6";
        //-------------------------------------------------------------------------------------------------
        public const string CaptionButtonbbiGFN4_F1 = "bbiGFN4_F1";
        public const string CaptionButtonbbiGFN4_F2 = "bbiGFN4_F2";
        public const string CaptionButtonbbiGFN4_F3 = "bbiGFN4_F3";
        public const string CaptionButtonbbiGFN4_F4 = "bbiGFN4_F4";
        public const string CaptionButtonbbiGFN4_F5 = "bbiGFN4_F5";
        public const string CaptionButtonbbiGFN4_F6 = "bbiGFN4_F6";
        //-------------------------------------------------------------------------------------------------
        public const string CaptionButtonbbiGFN5_F1 = "bbiGFN5_F1";
        public const string CaptionButtonbbiGFN5_F2 = "bbiGFN5_F2";
        public const string CaptionButtonbbiGFN5_F3 = "bbiGFN5_F3";
        public const string CaptionButtonbbiGFN5_F4 = "bbiGFN5_F4";
        public const string CaptionButtonbbiGFN5_F5 = "bbiGFN5_F5";
        public const string CaptionButtonbbiGFN5_F6 = "bbiGFN5_F6";
        //-------------------------------------------------------------------------------------------------
        public const string CaptionButtonbbiFN_1 = "bbiFN_1";
        public const string CaptionButtonbbiFN_2 = "bbiFN_2";
        public const string CaptionButtonbbiFN_3 = "bbiFN_3";
        public const string CaptionButtonbbiFN_4 = "bbiFN_4";
        public const string CaptionButtonbbiFN_5 = "bbiFN_5";
        //-------------------------------------------------------------------------------------------------
        #endregion


        #region "Allow Default Button"
        public const Boolean AllowButtonAddDefaultCommamd = true;
        public const Boolean AllowButtonEditDefaultCommamd = true;
        public const Boolean AllowButtonDeleteDefaultCommamd = true;
        public const Boolean AllowButtonSaveDefaultCommamd = true;
        public const Boolean AllowButtonRevisionDefaultCommamd = true;
        public const Boolean AllowButtonRefreshDefaultCommamd = true;
        public const Boolean AllowButtonRangeSizeDefaultCommamd = true;
        public const Boolean AllowButtonPrintDefaultCommamd = true;
        public const Boolean AllowButtonPrintDownDefaultCommamd = false;
        public const Boolean AllowButtonGenerateDefaultCommamd = true;
        public const Boolean AllowButtonFindDefaultCommamd = true;
        public const Boolean AllowButtonExpandDefaultCommamd = false;
        public const Boolean AllowButtonCopyDefaultCommamd = true;
        public const Boolean AllowButtonBreakDownDefaultCommamd = true;
        public const Boolean AllowButtonCancelDefaultCommamd = true;
        public const Boolean AllowButtonCheckDefaultCommamd = true;
        public const Boolean AllowButtonCloseDefaultCommamd = true;
        public const Boolean AllowButtonCollosepandDefaultCommamd = false;
        public const Boolean AllowButtonCombineDefaultCommamd = true;
        public const Boolean AllowButtonFunctionDefaultCommamd = false;

        #endregion

        #region "Enable Default Button"

        public const Boolean EnableButtonAddDefaultCommamd = true;
        public const Boolean EnableButtonEditDefaultCommamd = true;
        public const Boolean EnableButtonDeleteDefaultCommamd = true;
        public const Boolean EnableButtonSaveDefaultCommamd = false;
        public const Boolean EnableButtonRevisionDefaultCommamd = true;
        public const Boolean EnableButtonRefreshDefaultCommamd = true;
        public const Boolean EnableButtonRangeSizeDefaultCommamd = true;
        public const Boolean EnableButtonPrintDefaultCommamd = true;
        public const Boolean EnableButtonPrintDownDefaultCommamd = true;
        public const Boolean EnableButtonGenerateDefaultCommamd = true;
        public const Boolean EnableButtonFindDefaultCommamd = true;
        public const Boolean EnableButtonExpandDefaultCommamd = false;
        public const Boolean EnableButtonCopyDefaultCommamd = true;
        public const Boolean EnableButtonBreakDownDefaultCommamd = true;
        public const Boolean EnableButtonCancelDefaultCommamd = false;
        public const Boolean EnableButtonCheckDefaultCommamd = true;
        public const Boolean EnableButtonCloseDefaultCommamd = true;
        public const Boolean EnableButtonCollosepandDefaultCommamd = false;
        public const Boolean EnableButtonCombineDefaultCommamd = true;
        public const Boolean EnableButtonFunctionDefaultCommamd = true;

        #endregion




        #region "Tooltip Default Button"

        public const string HintButtonAddDefaultCommamd = "Ctrl+Shift+A";
        public const string HintButtonEditDefaultCommamd = "Ctrl+Shift+E";
        public const string HintButtonDeleteDefaultCommamd = "Ctrl+Shift+D";
        public const string HintButtonSaveDefaultCommamd = "Ctrl+Shift+S";
        public const string HintButtonRevisionDefaultCommamd = "Ctrl+Shift+R";
        public const string HintButtonRefreshDefaultCommamd = "Ctrl+Shift+F5";
        public const string HintButtonRangeSizeDefaultCommamd = "Ctrl+Shift+K";
        public const string HintButtonPrintDefaultCommamd = "Ctrl+Shift+P";
        public const string HintButtonPrintDownDefaultCommamd = "";
        public const string HintButtonGenerateDefaultCommamd = "Ctrl+Shift+G";
        public const string HintButtonFindDefaultCommamd = "Ctrl+Shift+F";
        public const string HintButtonExpandDefaultCommamd = "Ctrl+Shift+L";
        public const string HintButtonCopyDefaultCommamd = "Ctrl+Shift+C";
        public const string HintButtonBreakDownDefaultCommamd = "Ctrl+Shift+B";
        public const string HintButtonCancelDefaultCommamd = "Ctrl+Shift+X";
        public const string HintButtonCheckDefaultCommamd = "";
        public const string HintButtonCloseDefaultCommamd = "Ctrl+Shift+Q";
        public const string HintButtonCollosepandDefaultCommamd = "Ctrl+Shift+M";
        public const string HintButtonCombineDefaultCommamd = "Ctrl+Shift+H";
        public const string HintButtonFunctionDefaultCommamd = "";

        #endregion
    }


}
