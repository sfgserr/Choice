part of 'set_new_password_bloc.dart';

class SetNewPasswordState {
  final bool isMainBtnEnabled;
  final bool isObscurePassword;
  final bool isObscureConfirmPassword;
  final String firstPassword;
  final String secondPassword;

  SetNewPasswordState({
    required this.isObscurePassword,
    required this.isObscureConfirmPassword,
    required this.isMainBtnEnabled,
    required this.firstPassword,
    required this.secondPassword,
  });
}

class SetNewPasswordInitial extends SetNewPasswordState {
  SetNewPasswordInitial()
      : super(
          isMainBtnEnabled: false,
          isObscurePassword: true,
          isObscureConfirmPassword: true,
          firstPassword: '',
          secondPassword: '',
        );
}
