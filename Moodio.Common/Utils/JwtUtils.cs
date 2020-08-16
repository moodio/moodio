using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Moodio.Utils
{
    public static class JwtUtils
    {
        public static JsonElement ExtractTokenBody(string token)
        {
            if (!ValidateToken(token))
                throw new ArgumentException("Invalid token");
            
            // get body section
            var sectionBase64 = PadSection(token.Split('.')[1]);

            //convert from base64
            var section = Convert.FromBase64String(sectionBase64);

            //return parsed as json
            return JsonDocument.Parse(section).RootElement;
        }

        /// <summary>
        /// check if a token is correctly formatted
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public  static bool ValidateToken(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
                return false;

            //check sections
            var parts = token.Split('.');
            if(parts.Length != 3){
                return false;
            }

            return true;


        }

        private static string PadSection(string tokenSection)
        {
            var rem = tokenSection.Length % 4;
            if(rem != 0)
            {
                tokenSection += new string('=', 4 - rem);
            }
            return tokenSection;
        }
    }
}
