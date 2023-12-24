
sealed class LoginEvent {}

final class ChangeTab extends LoginEvent {
  final int tabIndex;

  ChangeTab({required this.tabIndex});
}

final class EnableLoginBtn extends LoginEvent {
  final bool isLoginBtnEnabled;

  EnableLoginBtn({required this.isLoginBtnEnabled});
}

final class ObscurePasswordText extends LoginEvent {}

final class GetCode extends LoginEvent {
  final bool isGettingCode;
  GetCode({required this.isGettingCode});
}