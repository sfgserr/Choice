import 'package:form_field_validator/form_field_validator.dart';

import 'strings.dart';

class AppValidators {
  static String? Function(String?)? get emailValidator => MultiValidator([
        RequiredValidator(errorText: AppStrings.emailIsRequiredText),
        EmailValidator(errorText: AppStrings.incorrectEmailText),

      ]);

  static String? Function(String?)? get resetPasswordCodeValidator =>
      MultiValidator([
        RequiredValidator(errorText: AppStrings.codeIsRequiredText),
        LengthRangeValidator(min: 6, max: 6, errorText: AppStrings.incorrectCodeCountText(6)),

        // TODO: compare this code with server
      ]);

  static String? Function(String?)? get smsCodeValidator => MultiValidator([
        RequiredValidator(errorText: AppStrings.codeIsRequiredText),
        LengthRangeValidator(min: 4, max: 4, errorText: AppStrings.incorrectCodeCountText(4)),

        // TODO: compare this code with server
      ]);

  static String? Function(String?)? get passwordValidator => MultiValidator([
        RequiredValidator(errorText: AppStrings.passwordIsRequiredText),
        MinLengthValidator(8, errorText: AppStrings.passwordMinLengthText),
        MaxLengthValidator(16, errorText: AppStrings.passwordMaxLengthText),

        // TODO: add some regExp
      ]);

  static String? Function(String?)? get newPasswordValidator => MultiValidator([
        RequiredValidator(errorText: AppStrings.passwordIsRequiredText),
        MinLengthValidator(8, errorText: AppStrings.passwordMinLengthText),
        MaxLengthValidator(16, errorText: AppStrings.passwordMaxLengthText),

        // TODO: add some regExp
      ]);

  static String? Function(String?)? get confirmPasswordValidator =>
      MultiValidator([
        RequiredValidator(errorText: AppStrings.passwordIsRequiredText),
        MinLengthValidator(8, errorText: AppStrings.passwordMinLengthText),
        MaxLengthValidator(16, errorText: AppStrings.passwordMaxLengthText),
        // TODO: match it with the first one
        // TODO: add some regExp
      ]);

  static String? Function(String?)? get phoneValidator => MultiValidator([
        RequiredValidator(errorText: AppStrings.phoneIsRequiredText),
        PatternValidator('^[(+7)8]*', errorText: AppStrings.incorrectPhoneText),
        LengthRangeValidator(min: 10, max: 10, errorText: AppStrings.phoneIncorrectLengthText),

        // TODO: add some regExp
      ]);
}
