part of 'set_new_password_bloc.dart';

class SetNewPasswordState {
  final bool isMainBtnEnabled;
  final bool isObscurePassword;
  final bool isObscureConfirmPassword;
  final String firstPassword;

  SetNewPasswordState({
    required this.isObscurePassword,
    required this.isObscureConfirmPassword,
    required this.isMainBtnEnabled,
    required this.firstPassword,
  });
}

class SetNewPasswordInitial extends SetNewPasswordState {
  SetNewPasswordInitial()
      : super(
          isMainBtnEnabled: false,
          isObscurePassword: true,
          isObscureConfirmPassword: true,
          firstPassword: '',
        );
}
