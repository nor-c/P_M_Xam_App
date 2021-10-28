using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace P_M_Xam_App.Models
{
    public class ApiToken
    {

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("authId")]
        public string AuthId { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("expiryDate")]
        public DateTime ExpiryDate { get; set; }
    }
}
