using FluentValidation;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;

namespace MetaAuth.Metamask.Validation;

public static class EthereumRules
{
    public static IRuleBuilderInitial<T, string>? IsUri<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) => {
            if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                context.AddFailure("Invalid Uri");
        }) as IRuleBuilderInitial<T, string>;
    }
    
    public static IRuleBuilderInitial<T, string>? IsEthereumAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) => {
            if (!value.IsValidEthereumAddressHexFormat())
                context.AddFailure("Invalid Ethereum Address");
        }) as IRuleBuilderInitial<T, string>;
    }
    
    public static IRuleBuilderInitial<T, string>? IsHex<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().Custom((value, context) => {
            if (!value.HasHexPrefix()) context.AddFailure("The value needs to be prefixed with 0x");
            if (!value.IsHex()) context.AddFailure("This is not a valid Hexadecimal");
        }) as IRuleBuilderInitial<T, string>;
    }
}