using Moodio.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moodio.Common.Tests
{
    public class JwtUtilsTests
    {
        private const string tokenValid = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Imh1Tjk1SXZQZmVocTM0R3pCRFoxR1hHaXJuTSIsImtpZCI6Imh1Tjk1SXZQZmVocTM0R3pCRFoxR1hHaXJuTSJ9.eyJhdWQiOiJhcGk6Ly90cnVseWhwY2RldiIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzNmMjNhYWM2LWVlM2YtNDVjYi1iZDYwLWZlMTRmNzc4YTQ2Ny8iLCJpYXQiOjE1OTc1Mjg3NzEsIm5iZiI6MTU5NzUyODc3MSwiZXhwIjoxNTk3NTMyNjcxLCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOFFBQUFBcjFjaEh1bFlNejhyQkFick16djhtRG1SQ1RNVVh1aXlDcjFkVUN3YnZQdmsvS0doL1BCTko2bEdXOXlOS0ZUUzlwMnduZldjMUF6WU9EQTdoS0lXQnV6N0loZHdwdXZTYUFZVUJKRTgyT2M9IiwiYW1yIjpbInB3ZCIsInJzYSIsIm1mYSJdLCJhcHBpZCI6IjNjMzAzZDEwLTY0YWYtNDFmYi05N2Q4LTRkMzlkY2IwYTFlZCIsImFwcGlkYWNyIjoiMCIsImRldmljZWlkIjoiZjg2YmI0ZGItOTZmZS00ZWExLWE5ZDUtMzA0YjVmODUxYzM2IiwiZmFtaWx5X25hbWUiOiJTd2VobGkiLCJnaXZlbl9uYW1lIjoiTWFobW91ZCIsImlwYWRkciI6IjIxNy4xMzguMTU3LjE1OCIsIm5hbWUiOiJNYWhtb3VkIFN3ZWhsaSIsIm9pZCI6IjYxMGE1N2ZjLTVjNjktNGYyOC1hMWQ4LWQ5NTYxODhiZDE5NSIsInNjcCI6ImFsbCIsInN1YiI6Im92VGVnTk15UDRydzZEZG1LdTUtS3d4aTNLSXRKejR4YmhqZWhmVF90SE0iLCJ0aWQiOiIzZjIzYWFjNi1lZTNmLTQ1Y2ItYmQ2MC1mZTE0Zjc3OGE0NjciLCJ1bmlxdWVfbmFtZSI6Im1haG1vdWRAbW9vZGlvLmNvLnVrIiwidXBuIjoibWFobW91ZEBtb29kaW8uY28udWsiLCJ1dGkiOiJQZGdLbUpDNjdVS0tBN2tEUE9vU0FBIiwidmVyIjoiMS4wIn0.MgzkWomQSxEn5NkRdpFNG8emxShtg_or04p75RieIh-mCZ_Z-DB1_VHsTd7_iociGIM6Jb0793cSPSKbKZWOMQ2__7qb7RwYg6xZRrCqM5cMYujL8YgKybcJDeWFzwDbPVvuE5GffEKYwD-h6TGbrF1waOc-Qcd_f4nn0Bp4wgXeJ-VEAv-71sgE8zmD5Lws4Y2rueEzkza9nP5HTpq4LnxjWIzYtasArL8uOpNwgbZzdwK4JbwkIbvPqogK5HEcUJ02bt_Zxq28M1FIpdgKsddE1sa8zIitZIsZ6IZspRC34qmylpeyg_sM1i4_UkYLEG_hLdhcM-uNvVEEzxiPNw";
        private const string tokeninvalid = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Imh1Tjk1SXZQZmVocTM0R3pCRFoxR1hHaXJuTSIsImtpZCI6Imh1Tjk1SXZQZmVocTM0R3pCRFoxR1hHaXJuTSJ9.eyJhdWQiOiJhcGk6Ly90cnVseWhwY2RldiIsImlzcyI6Imh0dHBzOi";

        [Test]
        public void GivenCorrectTokenFormatValidatesTrue() 
        {

            //arrange
            var res = JwtUtils.ValidateToken(tokenValid);

            //assert
            Assert.IsTrue(res);
        }

        [Test]
        public void GivenIncorrectTokenFormatValidatesFalse()
        {
            //act and arrange
            var res1 = JwtUtils.ValidateToken(tokeninvalid);
            var res2 = JwtUtils.ValidateToken("");
            var res3 = JwtUtils.ValidateToken(null);


            //assert
            Assert.IsFalse(res1);
            Assert.IsFalse(res2);
            Assert.IsFalse(res3);
        }

        [Test]
        public void GetsSectionBody()
        {
            //act and arrange
            var body = JwtUtils.ExtractTokenBody(tokenValid);

            Assert.IsNotNull(body);

            Assert.IsTrue(body.TryGetProperty("aud", out var aud));
            Assert.IsTrue(body.TryGetProperty("iss", out var iss));
            Assert.IsTrue(body.TryGetProperty("iat", out var iat));

            Assert.AreEqual("api://trulyhpcdev", aud.GetString());
            Assert.AreEqual("https://sts.windows.net/3f23aac6-ee3f-45cb-bd60-fe14f778a467/", iss.GetString());
            Assert.IsTrue(iat.TryGetInt32(out var iatInt));
            Assert.AreEqual(1597528771, iatInt);
        }

    }
}
