using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public enum SmtpResponseCodes
    {
        SystemStatus = 211,
        Help = 214,
        Ready = 220,
        ClosingChannel = 221,
        AuthenticationSuccessfull = 235,
        RequestCompleted = 250,
        UserNotLocalOk = 251,
        StartInput = 354,
        ServiceNotAvailable = 421,
        MailBoxUnavailable = 450,
        RequestAborted = 451,
        InsufficientStorage = 452,
        Error = 500,
        SyntaxError = 501,
        CommandNotImplemented = 502,
        BadSequence = 503,
        CommandParameterNotImplemented = 504,
        MailNotAccepted = 521,
        AuthenticationFailed = 535,
        NotImplemented = 550,
        UserNotLocalBad = 551,
        ExceededStorage = 552,
        MailBoxNameNotValid = 553,
        TransactionFailed = 554
    }
    public enum SaveCloseType
    {
        
        None,
        ButtonCloseClick,
        FormMainClose

    }
    public enum ActionDialog
    {
        Yes,
        No,
        None

    }



    public enum RowEditType
    {
        Empty,
        Released,
        Locked

    }


    public enum CtrlLoadSourceType
    {
        Init,
        Load,
        Normal,
    }
    public enum BomMHLoadSourceType
    {
        InitTree,
        AddFunc,
        EditFunc,
        SourceChange,
    }
    public enum WizardButtonType
    {
        Load,
        Back,
        Next,
        Finish
    }

    public enum DocumentTypeRichText
    {
        Normal,
        Rtf,
        Html,
    }
    public enum SuppRefSet
    {
        NotSet,
        SubSet,
        MainSet,
    }

    public enum TreeListShowPopupType
    {
        EmptyCell,
        InCell,
    }

    public enum WorkingAt
    {
        PRODUCTION,
        TEST
    }
    public enum PasteOptionEmpty
    {
        RemoveAllEmpty,
        NotRemoveEmpty,
        RemoveEmptyLast,
    }


    public enum ReferhDataOption
    {
        None,
        View,
        Edit,
        Add
    }


    

    public enum DataStatus
    {
        Unchange,
        Update,
        Insert,
    }

    public enum DataAttType
    {
        Number,
        Boolean,
        Datetime,
        String,

    }


    public enum TreeInsertPosition
    {
        Befor,
        After,
        Last,
    }

}
