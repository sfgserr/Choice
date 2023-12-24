class LoginState {
  final int currentTabIndex;
  final bool isLoginBtnEnabled;
  final bool isGettingCode;
  final bool isObscurePassword;

  LoginState({
    required this.isObscurePassword,
    required this.isGettingCode,
    required this.currentTabIndex,
    required this.isLoginBtnEnabled,
  });
}

class LoginInitial extends LoginState {
  LoginInitial()
      : super(
          currentTabIndex: 0,
          isLoginBtnEnabled: false,
          isGettingCode: false,
          isObscurePassword: true,
        );
}
