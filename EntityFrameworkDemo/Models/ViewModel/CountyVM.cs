using System;

namespace EntityFrameworkDemo.Models.ViewModel
{
    public class CountyVM
    {
        public Guid Id { get; set; }

        // County Language
        public Guid?  LanguageId { get; set; }
        public string Language { get; set; }
        public string Name       { get; set; }

        // Country
        public Guid   CountryId   { get; set; }

        // 用來顯示 View Label 用，不需要讀值
        public string CountryName { get; set; }
    }
}