class ForgotPasswordState {
  final bool isEnabledMainBtn;

  ForgotPasswordState({
    this.isEnabledMainBtn = false,
  });
}

class ForgotPasswordInitial extends ForgotPasswordState {
  ForgotPasswordInitial() : super(isEnabledMainBtn: false);
}
