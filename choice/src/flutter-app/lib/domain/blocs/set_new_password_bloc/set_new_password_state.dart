class SetNewPasswordState {
  final bool isMainBtnEnabled;
  final bool isObscurePassword;
  final bool isObscureConfirmPassword;

  SetNewPasswordState({
    required this.isObscurePassword,
    required this.isObscureConfirmPassword,
    required this.isMainBtnEnabled,
  });
}

class SetNewPasswordInitial extends SetNewPasswordState {
  SetNewPasswordInitial()
      : super(
          isMainBtnEnabled: false,
          isObscurePassword: true,
          isObscureConfirmPassword: true,
        );
}
