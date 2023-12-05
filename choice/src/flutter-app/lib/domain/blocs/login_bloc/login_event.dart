
abstract class LoginEvent {}

class ChangeTab extends LoginEvent {
  final int tabIndex;

  ChangeTab({required this.tabIndex});
}

class EnableLoginBtn extends LoginEvent {
  final bool isLoginBtnEnabled;

  EnableLoginBtn({required this.isLoginBtnEnabled});
}

class GetCode extends LoginEvent {}