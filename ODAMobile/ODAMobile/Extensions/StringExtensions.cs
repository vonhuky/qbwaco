﻿using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Xamarin.Forms.Extensions
{
    public static class StringExtensions
    {
        public static bool IsDateTime(this string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToUnSign(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static bool UnSignContains(this string a, string b)
        {
            return a.ToUnSign().ToUpper().Contains(b.ToUnSign().ToUpper());
        }
        public static string ToBase64(this string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string NumericFormat(this string input)
        {
            string result = input;
            if (string.IsNullOrWhiteSpace(result))
                result = "0";
            else if (result.StartsWith("0") && result.Length > 1 && !result.Contains(",") && !result.Contains("."))
                result = result.Remove(result.IndexOf('0'), 1);
            else if (result.StartsWith(","))
                result = result.Remove(result.IndexOf(','), 1);
            else if (result.StartsWith("."))
                result = result.Remove(result.IndexOf('.'), 1);
            else if (Convert.ToDecimal(result) >= 1000 && !result.EndsWith("."))
                result = Convert.ToDecimal(result).ToString("###,###,###,###,##0.##");
            else if (Convert.ToDecimal(result) < 1000 && result.Contains(",") && !result.EndsWith("."))
                result = result.Remove(result.IndexOf(','), 1);
            return result;
        }

        public static bool EmailValidate(this string input)
        {
            try
            {
                Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                return regex.IsMatch(input.Trim());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}