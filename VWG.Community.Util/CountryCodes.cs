using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration;

namespace VWG.Community.Util
{
    /// <summary>
    /// using CsvHelper: https://github.com/JoshClose/CsvHelper
    /// to prase country-codes.csv: https://github.com/datasets/country-codes/blob/master/data/country-codes.csv
    /// into object CountryCodes
    /// </summary>
    public class CountryCodes
    {
        public List<CountryCode> GetList()
        {
            List<CountryCode> result = null;

            var asm = Assembly.GetExecutingAssembly();

            using (var source = asm.GetManifestResourceStream("VWG.Community.Util.assets.country-codes.csv"))
            using (var reader = new StreamReader(source))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                source.Position = 0;

                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<CountryCodeMap>();

                result = csv.GetRecords<CountryCode>().ToList();
            }

            return result;
        }

        /// <summary>
        /// Find by Display Name where LevenshteinDistance.CalculateSimilarity > 0.6
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByDisplayName(String target)
        {
            CountryCode result = null;

            var list = GetList();
            var item = list.Where(x => LevenshteinDistance.CalculateSimilarity(x.CLDR_displayname.ToLower(), target.ToLower()) > 0.6).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by FIFA code
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByFIFA(String target)
        {
            CountryCode result = null;

            var list = GetList();
            var item = list.Where(x => x.FIFA.ToLower() == target.ToLower()).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by TLD code
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByTLD(String target)
        {
            CountryCode result = null;
            if (!target.Contains(".")) target = "." + target;
            var list = GetList();
            var item = list.Where(x => x.TLD.ToLower() == target.ToLower()).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by Dial code
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByDial(String target)
        {
            CountryCode result = null;
            var list = GetList();
            var item = list.Where(x => x.Dial.ToLower() == target.ToLower()).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by ISO3166-1 Alpha 3 code
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByISO3166_Alpha3(String target)
        {
            CountryCode result = null;
            var list = GetList();
            var item = list.Where(x => x.ISO3166_1_Alpha_3.ToLower() == target.ToLower()).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by FIPS code
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByFIPS(String target)
        {
            CountryCode result = null;
            var list = GetList();
            var item = list.Where(x => x.FIPS.ToLower() == target.ToLower()).FirstOrDefault();
            if (item != null) result = item;

            return result;
        }

        /// <summary>
        /// Find by All
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CountryCode FindByAll(String target)
        {
            CountryCode result = null;
            var list = GetList();
            var item = list
                .Where(x => x.Dial.ToLower() == target.ToLower() || x.FIPS.ToLower() == target || x.TLD.ToLower() == target ||
                x.FIFA.ToLower() == target || x.ISO3166_1_Alpha_3.ToLower()== target ||
                LevenshteinDistance.CalculateSimilarity(x.CLDR_displayname.ToLower(), target.ToLower()) > 0.6)
                .FirstOrDefault();
            if (item != null) result = item;

            return result;
        }
    }

    public class CountryCode
    {
        public string FIFA { get; set; }

        #region 1~10
        public string Dial { get; set; }
        public string ISO3166_1_Alpha_3 { get; set; }
        public string MARC { get; set; }
        public string is_independent { get; set; }
        public string ISO3166_1_numeric { get; set; }
        public string GAUL { get; set; }
        public string FIPS { get; set; }
        public string WMO { get; set; }
        public string ISO3166_1_Alpha_2 { get; set; }
        public string ITU { get; set; }
        #endregion

        #region 11~20
        public string IOC { get; set; }
        public string DS { get; set; }
        public string UNTERM_SpanishFormal { get; set; }
        public bool? GlobalCode { get; set; }
        public int? IntermediateRegionCode { get; set; }
        public string official_name_fr { get; set; }
        public string UNTERM_FrenchShort { get; set; }
        public string ISO4217_currency_name { get; set; }
        public string Developed_DevelopingCountries { get; set; }
        public string UNTERM_RussianFormal { get; set; }
        #endregion

        #region 21~30
        public string UNTERM_EnglishShort { get; set; }
        public string ISO4217_CurrencyAlphabeticCode { get; set; }
        public string SmallIslandDevelopingStates_SIDS { get; set; }
        public string UNTERM_SpanishShort { get; set; }
        public string ISO4217_CurrencyNumericCode { get; set; }
        public string UNTERM_ChineseFormal { get; set; }
        public string UNTERM_FrenchFormal { get; set; }
        public string UNTERM_RussianShort { get; set; }
        public string M49 { get; set; }
        public int? SubRegionCode { get; set; }
        #endregion

        #region 31~40
        public int? RegionCode { get; set; }
        public string official_name_ar { get; set; }
        public string ISO4217_CurrencyMinorUnit { get; set; }
        public string UNTERM_ArabicFormal { get; set; }
        public string UNTERM_ChineseShort { get; set; }
        public string LandLockedDevelopingCountries_LLDC { get; set; }
        public string IntermediateRegionName { get; set; }
        public string official_name_es { get; set; }
        public string UNTERM_EnglishFormal { get; set; }
        public string official_name_cn { get; set; }
        #endregion

        #region 41~50
        public string official_name_en { get; set; }
        public string ISO4217_CurrencyCountryName { get; set; }
        public string LeastDevelopedCountries_LDC { get; set; }
        public string RegionName { get; set; }
        public string UNTERM_ArabicShort { get; set; }
        public string SubRegionName { get; set; }
        public string official_name_ru { get; set; }
        public string GlobalName { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }
        #endregion

        public string TLD { get; set; }
        public string Languages { get; set; }
        public string GeonameID { get; set; }
        public string CLDR_displayname { get; set; }
        public string EDGAR { get; set; }
    }

    public sealed class CountryCodeMap : ClassMap<CountryCode>
    {
        CountryCodeMap()
        {
            Map(m => m.FIFA).Index(0);

            Map(m => m.Dial).Index(1);
            Map(m => m.ISO3166_1_Alpha_3).Index(2);
            Map(m => m.MARC).Index(3);
            Map(m => m.is_independent).Index(4);
            Map(m => m.ISO3166_1_numeric).Index(5);
            Map(m => m.GAUL).Index(6);
            Map(m => m.FIPS).Index(7);
            Map(m => m.WMO).Index(8);
            Map(m => m.ISO3166_1_Alpha_2).Index(9);
            Map(m => m.ITU).Index(10);

            Map(m => m.IOC).Index(11);
            Map(m => m.DS).Index(12);
            Map(m => m.UNTERM_SpanishFormal).Index(13);
            Map(m => m.GlobalCode).Index(14);
            Map(m => m.IntermediateRegionCode).Index(15);
            Map(m => m.official_name_fr).Index(16);
            Map(m => m.UNTERM_FrenchShort).Index(17);
            Map(m => m.ISO4217_currency_name).Index(18);
            Map(m => m.Developed_DevelopingCountries).Index(19);
            Map(m => m.UNTERM_RussianFormal).Index(20);

            Map(m => m.UNTERM_EnglishShort).Index(21);
            Map(m => m.ISO4217_CurrencyAlphabeticCode).Index(22);
            Map(m => m.SmallIslandDevelopingStates_SIDS).Index(23);
            Map(m => m.UNTERM_SpanishShort).Index(24);
            Map(m => m.ISO4217_CurrencyNumericCode).Index(25);
            Map(m => m.UNTERM_ChineseFormal).Index(26);
            Map(m => m.UNTERM_FrenchFormal).Index(27);
            Map(m => m.UNTERM_RussianShort).Index(28);
            Map(m => m.M49).Index(29);
            Map(m => m.SubRegionCode).Index(30);

            Map(m => m.RegionCode).Index(31);
            Map(m => m.official_name_ar).Index(32);
            Map(m => m.ISO4217_CurrencyMinorUnit).Index(33);
            Map(m => m.UNTERM_ArabicFormal).Index(34);
            Map(m => m.UNTERM_ChineseShort).Index(35);
            Map(m => m.LandLockedDevelopingCountries_LLDC).Index(36);
            Map(m => m.IntermediateRegionName).Index(37);
            Map(m => m.official_name_es).Index(38);
            Map(m => m.UNTERM_EnglishFormal).Index(39);
            Map(m => m.official_name_cn).Index(40);

            Map(m => m.official_name_en).Index(41);
            Map(m => m.ISO4217_CurrencyCountryName).Index(42);
            Map(m => m.LeastDevelopedCountries_LDC).Index(43);
            Map(m => m.RegionName).Index(44);
            Map(m => m.UNTERM_ArabicShort).Index(45);
            Map(m => m.SubRegionName).Index(46);
            Map(m => m.official_name_ru).Index(47);
            Map(m => m.GlobalName).Index(48);
            Map(m => m.Capital).Index(49);
            Map(m => m.Continent).Index(50);

            Map(m => m.TLD).Index(51);
            Map(m => m.Languages).Index(52);
            Map(m => m.GeonameID).Index(53);
            Map(m => m.CLDR_displayname).Index(54);
            Map(m => m.EDGAR).Index(55);
        }
    }
}
