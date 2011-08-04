using System;
using JetBrains.Annotations;

namespace DocxConverter.Utilities
{
  public static class ArgumentUtility
  {
    [AssertionMethod]
    public static T CheckNotNull<T> ([InvokerParameterName] string argumentName, [AssertionCondition (AssertionConditionType.IS_NOT_NULL)] T actualValue)
    {
      // ReSharper disable CompareNonConstrainedGenericWithNull
      if (actualValue == null)
        // ReSharper restore CompareNonConstrainedGenericWithNull
        throw new ArgumentNullException (argumentName);

      return actualValue;
    }
  }
}