using System;

namespace MaTrack.Shared.Helpers
{
    public static class GlobalHelpers
    {
        public static DateTime GetDate(string date)
        {
            DateTime d = default;
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var txtDoBArray = date.Split('/');
                    var dob = new DateTime(int.Parse(txtDoBArray[0]), int.Parse(txtDoBArray[1]), int.Parse(txtDoBArray[2]));
                    d = dob;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Date of Birth is not correct. More:{ex.Message}");
                }
            }
            return d;
        }
    }
}
