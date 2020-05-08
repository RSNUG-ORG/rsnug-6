namespace System
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime dataNascimento, DateTime quando)
        {
            if (quando.Month < dataNascimento.Month ||
                    quando.Month == dataNascimento.Month &&
                    quando.Day < dataNascimento.Day)            
                return quando.Year - dataNascimento.Year - 1;                        
            return quando.Year - dataNascimento.Year;
        }
    }
}
