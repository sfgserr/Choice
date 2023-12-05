class LoginState {
  final int currentTabIndex;
  final bool isLoginBtnEnabled;
  final bool isGettingCode;

  LoginState({
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
        );
}
