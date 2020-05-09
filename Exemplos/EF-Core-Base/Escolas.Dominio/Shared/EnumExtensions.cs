using CSharpFunctionalExtensions;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Converter uma string para um enum especificado
        /// </summary>
        /// <typeparam name="T">Tipo do enum desejado para conversão</typeparam>
        /// <param name="valor">Valor a ser convertido para o enum</param>
        /// <returns>Enum conforme string informada</returns>
        public static Result<T> ToEnum<T>(this string valor) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(valor))
                return Result.Failure<T>($"O parâmentro valor(string) está vazio ou nulo");
            else if (!Enum.TryParse(valor, true, out T resultado))
                return Result.Failure<T>($@"O valor ""{valor}"" não é reconhecido para enum ({resultado.GetType().Name})");
            else
                return Result.Ok(resultado);
        }
    }
}
