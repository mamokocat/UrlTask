
namespace TinyUrl.Common.Constants
{
    public static class ValidationErrors
    {
        public const string DuplicatePath = "UrlDuplicated";
        public const string HasConsecutiveSymbols = "UrlNoConsecutiveSymbols";
        public const string UrlNotExist = "UrlNotExist";
        public const string PathIsTooShort = "PathIsTooShort";
        public const string PathNoAlphaNumbericSubsequences = "PathNoAlphaNumbericSubsequences";
        public const string PathNoDigits = "PathNoDigits";
        public const string PathNoLetters = "PathNoLetters";
    }
}
