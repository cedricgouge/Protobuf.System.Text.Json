using System.Text.Json;
using System.Text.Json.Protobuf.Tests;
using System.Text.Json.Serialization;
using Protobuf.System.Text.Json.Tests.Utils;
using SmartAnalyzers.ApprovalTestsExtensions;
using Xunit;

namespace Protobuf.System.Text.Json.Tests
{
    public class DefaultAndNullableHandlingForEnums
    {
        [Fact]
        public void Should_Remove_DefaultEnumValues()
        {
            var msg = new MessageWithEnum2
            {
                EnumField = TestEnum2.Unknown,
                BoolField = true,
                DoubleField = 1.0
            };

            var jsonSerializerOptions = TestHelper.CreateJsonSerializerOptions(conf => conf.UseStringProtoEnumValueNames = true);
            
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

            // Act
            var serialized = JsonSerializer.Serialize(msg, jsonSerializerOptions);

            // Assert
            var approver = new ExplicitApprover();
            approver.VerifyJson(serialized);
        }
    }
}
