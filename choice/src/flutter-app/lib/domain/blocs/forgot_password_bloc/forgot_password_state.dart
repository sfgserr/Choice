class ForgotPasswordState {
  final bool isEnabledMainBtn;

  ForgotPasswordState({
    required this.isEnabledMainBtn,
  });
}

class ForgotPasswordInitial extends ForgotPasswordState {
  ForgotPasswordInitial() : super(isEnabledMainBtn: false);
}
