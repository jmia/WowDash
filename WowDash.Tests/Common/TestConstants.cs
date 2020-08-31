using System;
using System.Collections.Generic;
using System.Text;

namespace WowDash.Tests.Common
{
    public static class TestConstants
    {
        public const string AllOnesGuidLiteral = "11111111-1111-1111-1111-111111111111";
        public const string AllTwosGuidLiteral = "22222222-2222-2222-2222-222222222222";
        public const string AllThreesGuidLiteral = "33333333-3333-3333-3333-333333333333";
        public const string AllFoursGuidLiteral = "44444444-4444-4444-4444-444444444444";
        public const string AllFivesGuidLiteral = "55555555-5555-5555-5555-555555555555";
        public static Guid AllOnesGuid = Guid.Parse(AllOnesGuidLiteral);
        public static Guid AllTwosGuid = Guid.Parse(AllTwosGuidLiteral);
        public static Guid AllThreesGuid = Guid.Parse(AllThreesGuidLiteral);
        public static Guid AllFoursGuid = Guid.Parse(AllFoursGuidLiteral);
        public static Guid AllFivesGuid = Guid.Parse(AllFivesGuidLiteral);
    }
}
