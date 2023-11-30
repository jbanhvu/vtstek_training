using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CNY_Report.Common
{
  public static  class NumberInWord
    {
        public static string So_chu(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";
            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";
            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";
            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();
            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai
            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";
            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);
                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";
            return lso_chu.ToString().Trim();
        }
        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }

            return Ktach;
        }
        private static string Donvi(string so)
        {
            string Kdonvi = "";
            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";
            return Kdonvi;
        }
        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }
        

        public static String changeToWords(String numb)
        {
            String val = "";
            String wholeNo = numb;
            String points = "";
            String andStr = "";
            String pointStr = "";
            String endStr = "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and cents";
                        // just to separate whole numbers from points
                        pointStr = translateWholeNumber(points);
                            //translateCents(points);
                    }
                    else
                    {
                        andStr = "only";
                    }
                }
                else
                {
                    andStr = "only";
                }
                val = String.Format("{0} {1} {2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);

            }
            catch
            {

            }
            return val;
        }
        private static String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;
                //tests for 0XX
                bool isDone = false;
                //test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))
                if (dblAmt > 0)
                {
                    //test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");

                    int numDigits = number.Length;
                    int pos = 0;
                    //store digit grouping
                    String place = "";
                    //digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1:
                            //ones' range
                            word = ones(number);
                            isDone = true;
                            break; // TODO: might not be correct. Was : Exit Select
                        case 2:
                            //tens' range
                            word = tens(number);
                            isDone = true;
                            break; // TODO: might not be correct. Was : Exit Select
                        case 3:
                            //hundreds' range
                            if(number.Substring(0,1)=="0")
                            {
                                pos = (numDigits % 3) + 1;
                                place = "";
                            }
                            else
                            {
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            }
                            break; // TODO: might not be correct. Was : Exit Select
                        //thousands' range
                        case 4:
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break; // TODO: might not be correct. Was : Exit Select
                        //millions' range
                        case 7:
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break; // TODO: might not be correct. Was : Exit Select
                        case 10:
                            //Billions's range
                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break; // TODO: might not be correct. Was : Exit Select

                        default:
                            //add extra case options for anything above Billion...
                            isDone = true;
                            break; // TODO: might not be correct. Was : Exit Select

                    }
                    if (!isDone)
                    {
                        //if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero)
                        {
                          if(!word.Contains("and"))
                            {
                                word = "and " + word.Trim();
                            }
                        }
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim()))
                    {
                        word = "";
                    }
                }

            }
            catch
            {
               
            }
            return word.Trim();
        }
        private static String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "Ten";
                    break; // TODO: might not be correct. Was : Exit Select        \
                case 11:
                    name = "Eleven";
                    break; // TODO: might not be correct. Was : Exit Select
                case 12:
                    name = "Twelve";
                    break; // TODO: might not be correct. Was : Exit Select
                case 13:
                    name = "Thirteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 14:
                    name = "Fourteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 15:
                    name = "Fifteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 16:
                    name = "Sixteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 17:
                    name = "Seventeen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 18:
                    name = "Eighteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 19:
                    name = "Nineteen";
                    break; // TODO: might not be correct. Was : Exit Select
                case 20:
                    name = "Twenty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 30:
                    name = "Thirty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 40:
                    name = "Fourty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 50:
                    name = "Fifty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 60:
                    name = "Sixty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 70:
                    name = "Seventy";
                    break; // TODO: might not be correct. Was : Exit Select
                case 80:
                    name = "Eighty";
                    break; // TODO: might not be correct. Was : Exit Select
                case 90:
                    name = "Ninety";
                    break; // TODO: might not be correct. Was : Exit Select
                default:
                    if (digt > 0)
                    {
                        name = (tens(digit.Substring(0, 1) + "0") + " ") + ones(digit.Substring(1));
                    }
                    break; // TODO: might not be correct. Was : Exit Select
            }
            return name;
        }
        private static String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    name = "One";
                    break; // TODO: might not be correct. Was : Exit Select
                case 2:
                    name = "Two";
                    break; // TODO: might not be correct. Was : Exit Select
                case 3:
                    name = "Three";
                    break; // TODO: might not be correct. Was : Exit Select
                case 4:
                    name = "Four";
                    break; // TODO: might not be correct. Was : Exit Select
                case 5:
                    name = "Five";
                    break; // TODO: might not be correct. Was : Exit Select
                case 6:
                    name = "Six";
                    break; // TODO: might not be correct. Was : Exit Select
                case 7:
                    name = "Seven";
                    break; // TODO: might not be correct. Was : Exit Select
                case 8:
                    name = "Eight";
                    break; // TODO: might not be correct. Was : Exit Select
                case 9:
                    name = "Nine";
                    break; // TODO: might not be correct. Was : Exit Select

            }
            return name;
        }
        private static String translateCents(String cents)
        {
            String cts = "";
            String digit = "";
            String engOne = "";
            for (int i = 0; i <= cents.Length - 1; i++)
            {
                digit = cents[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cts += " " + engOne;
            }
            return cts;
        }

        #region "Enlisgh"
        public static string NumberToWordsEnglish(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWordsEnglish(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWordsEnglish(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWordsEnglish(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWordsEnglish(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        #endregion


        #region "Doc Tieng Viet"
        public static string PhanCach(string number,string sCurrency)
        {
            var sb = "";
            string DVT = "";
            var split = number.Trim().Split('.').ToList();
            switch (sCurrency)
            {
                case "USD":
                    for (var i = 0; i < split.Count; i++)
                    {
                        if (i > 0)
                            sb += " đô la Mỹ;";
                            sb += ChuyenSo(split[i]);
                        
                    }
                    DVT = " cent";
                    sb += DVT;
                    break;
                case "VND":
                    for (var i = 0; i < split.Count; i++)
                    {
                        if (i > 0)
                            sb += " lẻ ";
                        sb += ChuyenSo(split[i]);
                    }
                     DVT = " đồng";
                    sb += DVT;
                    break;
            }

    
      
            //string DVT = sCurrency == "USD" ? " đô la Mỹ" : " đồng";
            //sb += DVT;

            return Regex.Replace(sb.Substring(0, 1).ToUpper() + sb.Substring(1).ToLower(), @"\s+", " ");
        }
        private static string ChuyenSo(string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            var length = number.Length;
            number += "ss";
            var doc = new StringBuilder();
            var rd = 0;

            var i = 0;
            while (i < length)
            {
                //So chu so o hang dang duyet
                var n = (length - i + 2) % 3 + 1;

                //Kiem tra so 0
                var found = 0;
                int j;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] == '0') continue;
                    found = 1;
                    break;
                }

                //Duyet n chu so
                int k;
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        var ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3)
                                    doc.Append(cs[0]);
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0')
                                        doc.Append("lẻ");
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                switch (n - j)
                                {
                                    case 3:
                                        doc.Append(cs[1]);
                                        break;
                                    case 2:
                                        doc.Append("mười");
                                        ddv = 0;
                                        break;
                                    case 1:
                                        k = (i + j == 0) ? 0 : i + j - 1;
                                        doc.Append((number[k] != '1' && number[k] != '0') ? "mốt" : cs[1]);
                                        break;
                                }
                                break;
                            case '5':
                                if( n - j==1)
                                {
                                    doc.Append("lăm");
                                }
                                else
                                {

                                    doc.Append((i + j == length - 1) ? "lăm" : cs[5]);
                                }
                              
                                break;
                            default:
                                doc.Append(cs[number[i + j] - 48]);
                                break;
                        }

                        doc.Append(" ");

                        //Doc don vi nho
                        if (ddv == 1)
                            doc.Append(dv[n - j - 1] + " ");
                    }
                }


                //Doc don vi lon
                if (length - i - n > 0)
                {
                    if ((length - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (length - i - n) / 9; k++)
                                doc.Append("tỉ ");
                        rd = 0;
                    }
                    else
                        if (found != 0) doc.Append(dv[((length - i - n + 1) % 9) / 3 + 2] + " ");
                }

                i += n;
            }

            return (length == 1) && (number[0] == '0' || number[0] == '5') ? cs[number[0] - 48] : doc.ToString();
        }
        #endregion
    }
}
