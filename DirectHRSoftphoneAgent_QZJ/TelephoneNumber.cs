using System.Linq;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.TelephoneNumber
    /// Class Created: 2011/11/29 11:28
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class TelephoneNumber
    {
        private const int MinTelephoneNumberLength = 7;//MUST large than 5
        private const int LocalTelephoneLengthMin = 7;
        private const int LocalTelephoneLengthMax = 8;
        private const int CellphoneNumberLengthMin = 11;
        private const int CellphoneNumberLengthMax = 11;
        private const char CellphoneNumberBegins = '1';
        private const char CellphoneNumberSecondNeverBe = '0';
        private const int RemoveCountyCodeLengthMin = 10;

        public string OriginalNumber { get; set; }

        public string FixedNumber { get; set; }

        public string AreaCode { get; set; }

        public string CityName { get; set; }

        public TelephoneNumberType TelephoneType { get; set; }

        public DHRSoftphone.SoftphoneAgent_QZJ.AgentDatabaseProvider.AgentCityItem Location { get; set; }

        public TelephoneNumber(string telephoneNumber)
        {
            OriginalNumber = telephoneNumber;
            AnalyzeNumber();
        }

        public TelephoneNumberType AnalyzeNumber(string telephone)
        {
            OriginalNumber = telephone;
            return AnalyzeNumber();
        }

        public TelephoneNumberType AnalyzeNumber()
        {
            //FixedNumber = OriginalNumber;

            //Here is the begin
            FixedNumber = OriginalNumber.Replace(" ", string.Empty).Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
            //Remove the 86 if telephone number length large or euq 13
            if (FixedNumber.Length >= RemoveCountyCodeLengthMin)
            {
                if (FixedNumber.Substring(0, 2) == "86")
                    FixedNumber = FixedNumber.Substring(2);
            }

            //This program was design for Webpage click to dial, so NO service number (such as 110, 119, 10000) allowed.
            if (FixedNumber.Length < MinTelephoneNumberLength)
                return TelephoneNumberType.Invalid;

            while (FixedNumber.Length > 5 && FixedNumber[0] == '0')
            {
                //Remove this 0
                FixedNumber = FixedNumber.Substring(1);
            }

            if (FixedNumber.Length <= 5)
                return TelephoneNumberType.Invalid;

            if (FixedNumber[0] == CellphoneNumberBegins && FixedNumber[1] != CellphoneNumberSecondNeverBe)
            {
                //Should be cellphone number, but length check still needed.
                if (FixedNumber.Length >= CellphoneNumberLengthMin && FixedNumber.Length <= CellphoneNumberLengthMax)
                {
                    //Cellphone number.
                    var location = AgentDatabaseProvider.AllData.Where(a => a.CellphoneNumber == FixedNumber.Substring(0, 7));
                    if (location.Count() != 0)
                    {
                        //Found cellphone number location
                        Location = location.Single();
                        AreaCode = Location.AreaCode;
                        CityName = Location.City;
                        TelephoneType = TelephoneNumberType.Cellphone;
                        return TelephoneNumberType.Cellphone;
                    }
                }
            }

            if (FixedNumber.Length >= LocalTelephoneLengthMin && FixedNumber.Length <= LocalTelephoneLengthMax)
            {
                //Should be local number or something
                return TelephoneNumberType.FixedTelephone;
            }

            //Check areacode in telephone
            string areacode1 = string.Format("0{0}", FixedNumber.Substring(0, 2));
            string areacode2 = string.Format("0{0}", FixedNumber.Substring(0, 3));
            var areaCode = AgentDatabaseProvider.AllData.Where(a => a.AreaCode == areacode1 || a.AreaCode == areacode2);
            if (areaCode.Count() > 0)
            {
                //areaCode has been found!
                Location = areaCode.First();

                AreaCode = Location.AreaCode;
                CityName = Location.City;
                FixedNumber = FixedNumber.Substring(AreaCode.Length - 1);
                TelephoneType = TelephoneNumberType.FixedTelephone;
                return TelephoneNumberType.FixedTelephone;
            }

            return TelephoneNumberType.Unknown;
        }

        /// <summary>
        /// Makes the finally number.
        /// </summary>
        /// <param name="currentAreaCode">The current area code.</param>
        /// <returns></returns>
        public string MakeFinallyNumber(string currentAreaCode)
        {
            if (AreaCode != null && currentAreaCode != AreaCode)
            {
                switch (TelephoneType)
                {
                    case TelephoneNumberType.Cellphone:
                        return string.Format("0{0}", FixedNumber);
                    case TelephoneNumberType.FixedTelephone:
                        return string.Format("{0}{1}", AreaCode, FixedNumber);
                    default:
                        return FixedNumber;
                }
            }
            else
                return FixedNumber;
        }

        public enum TelephoneNumberType
        {
            Cellphone,
            FixedTelephone,
            Service400,
            Service800,
            Abroad,
            Unknown,
            Invalid
        }
    }
}