using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO Class that is used as return type for most of the CountryService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; } 
    }

    public static class CountryExtensions()
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {

        }
    }
}
